using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SiriusStore : MonoBehaviour
{
    [Serializable]
    public class Item
    {
        public string Name;

        public string GoogleID;
        public string AppleID;
        public string AmazonID;
        public string SamsungID;
        public string MicrosoftID;

        public bool Managed;
    }

    public static SiriusStore instance;

    public string GooglePrivateKey = "";
    public Item[] Items = new Item[0];
    public bool AutoRestore = true;

    void Awake()
    {
        if(instance != null)
        {
            Debug.LogError("More than one SiriusStore instances are not allowed.");
            Destroy(this);
            return;
        }

        instance = this;

        _Initialize();
    }

    void OnDestroy()
    {
        _Terminate();
    }

    public static void Purchase(string name)
    {
        if(Application.platform == RuntimePlatform.WindowsEditor)
		{
            _FireOnPurchaseComplete(name);
			return;
        }

        for(int i = 0; i < instance.Items.Length; i++)
        {
            if(instance.Items[i].Name == name)
            {
                instance._Purchase(instance.Items[i]);
                return;
            }
        }

        Debug.LogError("Unknown purchase: " + name);
    }

    public static event Action<string> OnPurchaseComplete;

    public static bool CanPurchase()
    {
        return(instance._CanPurchase());
    }

    public static void Restore()
    {
        instance._Restore();
    }

    private static void _FireOnPurchaseComplete(string item)
    {
        if(OnPurchaseComplete != null)
        {
            OnPurchaseComplete(item);
        }
    }

    #if UNITY_ANDROID && ANDROID_GOOGLE

        bool _googleBillingSupported;

        private void _Initialize()
        {
            _googleBillingSupported = false;

            GoogleIABManager.billingSupportedEvent += _GoogleBillingSupported;
            GoogleIABManager.billingNotSupportedEvent += _GoogleBillingNotSupported;
            GoogleIABManager.purchaseSucceededEvent += _GooglePurchaseSuccess;
            GoogleIABManager.purchaseFailedEvent += _GooglePurchaseFail;
            GoogleIABManager.queryInventorySucceededEvent += _GoogleInventorySuccess;
            GoogleIABManager.queryInventoryFailedEvent += _GoogleInventoryFail;
            GoogleIABManager.consumePurchaseSucceededEvent += _GoogleConsumeSuccess;
            GoogleIABManager.consumePurchaseFailedEvent += _GoogleConsumeFail;

            GoogleIAB.enableLogging(true);

            GoogleIAB.init(GooglePrivateKey);

            if(AutoRestore)
            {
                _Restore();
            }
        }

        private void _Terminate()
        {
            GoogleIAB.unbindService();
        }

        private void _Purchase(Item item)
        {
            GoogleIAB.purchaseProduct(item.GoogleID);
        }

        private bool _CanPurchase()
        {
            return(_googleBillingSupported);
        }

        private void _Restore()
        {
            List<string> skus = new List<string>();

            for(int i = 0; i < Items.Length; i++)
            {
                if(Items[i].Managed)
                {
                    skus.Add(Items[i].Name);
                }
            }

            GoogleIAB.queryInventory(skus.ToArray());
        }

        private void _GoogleBillingSupported()
        {
            _googleBillingSupported = true;
        }

        private void _GoogleBillingNotSupported(string error)
        {
            _googleBillingSupported = false;
            Debug.LogError(error);
        }

        private void _GooglePurchaseSuccess(GooglePurchase purchase)
        {
            for(int i = 0; i < Items.Length; i++)
            {
                if(Items[i].GoogleID == purchase.productId)
                {
                    if(!Items[i].Managed)
                    {
                        GoogleIAB.consumeProduct(purchase.productId);
                    }

                    _FireOnPurchaseComplete(Items[i].Name);
                    return;
                }
            }
        }

        private void _GooglePurchaseFail(string error)
        {
            Debug.LogError(error);
        }

        private void _GoogleInventorySuccess(List<GooglePurchase> purchases, List<GoogleSkuInfo> infos)
        {
            for(int i = 0; i < purchases.Count; i++)
            {
                _GooglePurchaseSuccess(purchases[i]);
            }
        }

        private void _GoogleInventoryFail(string error)
        {
            Debug.LogError(error);
        }

        private void _GoogleConsumeSuccess(GooglePurchase purchase)
        {
            // ...
        }

        private void _GoogleConsumeFail(string error)
        {
            Debug.LogError(error);
        }

    #elif UNITY_ANDROID && ANDROID_AMAZON

        private string _amazonUserID;
        private bool _amazonAvailable;

        private void _Initialize()
        {
            AmazonIAPManager.onGetUserIdResponseEvent += _AmazonUserIDResponse;
            AmazonIAPManager.onSdkAvailableEvent += _AmazonAvailable;
            AmazonIAPManager.itemDataRequestFinishedEvent += _AmazonItemDataSucess;
            AmazonIAPManager.itemDataRequestFailedEvent += _AmazonItemDataFail;
            AmazonIAPManager.purchaseSuccessfulEvent += _AmazonPurchaseSuccess;
            AmazonIAPManager.purchaseFailedEvent += _AmazonPurchaseFail;
            AmazonIAPManager.purchaseUpdatesRequestSuccessfulEvent += _AmazonUpdateSuccess;
            AmazonIAPManager.purchaseUpdatesRequestFailedEvent += _AmazonUpdateFail;

            AmazonIAP.initiateGetUserIdRequest();
            //AmazonIAP.initiateItemDataRequest();
        }

        private void _Terminate()
        {
            // ...
        }

        private void _Purchase(Item item)
        {
            AmazonIAP.initiatePurchaseRequest(item.AmazonID);
        }

        private bool _CanPurchase()
        {
            return(_amazonAvailable);
        }

        void _AmazonUserIDResponse(string userID)
        {
            _amazonUserID = userID;
        }

        void _AmazonAvailable(bool available)
        {
            _amazonAvailable = available;
        }

        void _AmazonItemDataSucess(List<string> unavailSKUs, List<AmazonItem> availItems)
        {
            // ...
        }

        void _AmazonItemDataFail()
        {
            Debug.LogError("Item data request failed.");
        }

        void _AmazonPurchaseSuccess(AmazonReceipt receipt)
        {
            for(int i = 0; i < Items.Length; i++)
            {
                if(Items[i].AmazonID == receipt.sku)
                {
                    _FireOnPurchaseComplete(receipt.sku);
                    return;
                }
            }

            Debug.LogError("Unknown purchase completed.");
        }

        void _AmazonPurchaseFail(string error)
        {
            Debug.LogError(error);
        }

        void _AmazonUpdateSuccess(List<string> items, List<AmazonReceipt> receipts)
        {
            for(int i = 0; i < receipts.Count; i++)
            {
                _AmazonPurchaseSuccess(receipts[i]);
            }
        }

        void _AmazonUpdateFail()
        {
            Debug.LogError("Failed to restore amazon purchases.");
        }

    #elif UNITY_ANDROID && ANDROID_SAMSUNG

        private void _Initialize()
        {
            
        }

        private void _Terminate()
        {
            
        }

        private void _Purchase()
        {
            
        }

        private bool _CanPurchase()
        {
            return(false);
        }

    #elif UNITY_IPHONE

        private void _Initialize()
        {
            StoreKitManager.autoConfirmTransactions = true;

            StoreKitManager.productListReceivedEvent += _AppleProductListSuccess;
            StoreKitManager.productListRequestFailedEvent += _AppleProductListFail;
            StoreKitManager.productPurchaseAwaitingConfirmationEvent += _ApplePurchaseAwaitingConfirm;
            StoreKitManager.purchaseSuccessfulEvent += _ApplePurchaseSuccess;
            StoreKitManager.purchaseCancelledEvent += _ApplePurchaseCancel;
	        StoreKitManager.purchaseFailedEvent += _ApplePurchaseFail;
            StoreKitManager.restoreTransactionsFinishedEvent += _AppleRestoreSuccess;
            StoreKitManager.restoreTransactionsFailedEvent += _AppleRestoreFail;
            StoreKitManager.paymentQueueUpdatedDownloadsEvent += _AppleUpdatedDownloads;

            //StoreKitBinding.requestProductData(new string[]);
        }

        private void _Terminate()
        {
            // ...
        }

        private void _Purchase(Item item)
        {
            StoreKitBinding.purchaseProduct(item.AppleID, 1);
        }

        private bool _CanPurchase()
        {
            return(StoreKitBinding.canMakePayments());
        }

        private void _Restore()
        {
            StoreKitBinding.restoreCompletedTransactions();
        }

        void _AppleProductListSuccess(List<StoreKitProduct> products)
        {
            Debug.LogError("We do not request the products' data. This event should never get raised.");
        }

        void _AppleProductListFail(string error)
        {
            Debug.LogError(error);
        }

        void _ApplePurchaseAwaitingConfirm(StoreKitTransaction transaction)
        {
            Debug.LogError("Purchases should be auto-confirmed. This event should never get raised.");
        }

        void _ApplePurchaseSuccess(StoreKitTransaction transaction)
        {
            for(int i = 0; i < Items.Length; i++)
            {
                if(Items[i].AppleID == transaction.productIdentifier)
                {
                    _FireOnPurchaseComplete(transaction.productIdentifier);
                    return;
                }
            }

            Debug.LogError("Unknown purchase completed.");
        }

        void _ApplePurchaseCancel(string error)
        {
            Debug.LogError(error);
        }

        void _ApplePurchaseFail(string error)
        {
            Debug.LogError(error);
        }

        void _AppleRestoreSuccess()
        {
            // ...
        }

        void _AppleRestoreFail(string error)
        {
            Debug.LogError(error);
        }

        void _AppleUpdatedDownloads(List<StoreKitDownload> downloads)
        {
            Debug.LogError("Downloads are not supported. This event should never get raised.");
        }

	#else

		private void _Initialize()
		{

		}

		private void _Terminate()
		{

		}

		private void _Purchase(Item item)
		{

		}

		private bool _CanPurchase()
		{
			return(true);
		}

		private void _Restore()
		{

		}

    #endif
};