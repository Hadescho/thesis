using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Game : MonoBehaviour {
	//General
	public static GameObject background;
	public static GameObject cursor;
	public static GameObject Script;
	//Local
	public static float timer; //Used to count total plsytime
	public static List<GameObject> allProjectiles;
	public static bool timeshiftBool;
	public static bool warBannerBool;
	public static bool energryDrainBool;
	public static bool towerSpawnUpdaterBool;
	public static bool blockTowerSpawn;
	public static GameObject warBannerMarker;
	
	public static float[] towersRange = new float[]
	{
//		/*Tower1Behaviour.config.range , Tower2Behaviour.config.range , Tower3Behaviour.config.range ,*/ AuraTowerBehaviour.config.range, CannonBehaviour.config.range
	};
	
	public static class Consts
	{
		public const float TimeshiftUpdateProofLife = 0.2f;
	}
	
	public static class SpellToolbar
	{
		public static GameObject spellToolbar;
		public static GameObject button;
		public static bool shown;
	}
	public static class NeutralButtons
	{
		public static GameObject pauseButton;
	}
	
	public static class TowerToolbar
	{
		public static GameObject towerToolbar;
		public static GameObject button;
		public static bool shown;
	}
	//End of general
	
	private static GameObject _lastTowerHit;
	 		
	public static class Towers
	{
		public static class Damage
		{
			//Damage
			public static float Tower1 = 60;
			public static float Tower2 = 7;
			public static float Tower3 = 200;
			public static float DropTower = 20;
			
		}
		
		public static class Upgrades
		{
			public static class Damage
			{
				//Damage Updates
				public static float Tower1 = 20;
				public static float Tower2 = 3;
				public static float Tower3 = 50;
				
				//Costs
				public static float Tower1cost = 100;
				public static float Tower2cost = 50;
				public static float Tower3cost = 500;
			}
			
		 
		}
	}
	
	public class LastRosette
	{
		public static GameObject Center;
		public static GameObject Sell;
		public static GameObject Upgrade1;
		public static GameObject Upgrade2;
		public static GameObject Upgrade3;
		public static bool shown;
		
		public static void Hide()
		{
			GameObject.Destroy(Center);
			GameObject.Destroy(Upgrade1);
			GameObject.Destroy(Upgrade2);
			GameObject.Destroy(Upgrade3);
			GameObject.Destroy(Sell);
			shown = false;
		}
		
		public static void Setter(GameObject center)
		{
			if (center.name != "Center")
			{
				Debug.LogError("You gave me wrong GameObject");
				return;
			}
			else
			{
				Center = center;
				shown = true;
				BuyTowerBox.BlockTowerSpawn();
				for(int i = 0; i < center.GetChildCount(); i++)
				{
					GameObject temp = center.GetChild(i);
					switch (temp.name)
					{
					case "Upgrade1":
						Upgrade1 = temp;
						Upgrade1.SetActive(true);
						//temp.FindChild("Text").GetComponent<exSpriteFont>().text = "Add Damage";
						break;
					case "Upgrade2":
						Upgrade2 = temp;
						Upgrade2.SetActive(true);
						//temp.FindChild("Text").GetComponent<exSpriteFont>().text = "Upgrade To Blue";
						break;
					case "Upgrade3":
						Upgrade3 = temp;
						Upgrade3.SetActive(true);
						break;
					case "Sell":
						Sell = temp;
						Sell.SetActive(true);
						temp.FindChild("Text").GetComponent<exSpriteFont>().text = "Sell";
						break;
					default:
						Debug.LogError("Something with LastRosette went terribly wrong");
						break;
					}
				}
				TowerBehaviour beh = center.GetComponent<RosetteBehaviour>().owner.GetComponent<TowerBehaviour>();
				
				//Debug.Log (beh.GetType().GetField("config").FieldType.GetField("Upgrade1").GetValue(beh.GetType ().GetField("config").GetValue(null)));
				
				if (beh.type.Upgrade1 == "" || !SiriusPrefs.B[beh.type.Upgrade1 + "unlocked"])
				{
					Upgrade1.SetActive(false);
				}
				else
				{
					Debug.Log (beh.type.Upgrade1);
					Upgrade1.FindChild("Text").SetText(beh.type.Upgrade1 + "unlocked");
				}
				if (beh.type.Upgrade2 == "" || !SiriusPrefs.B[beh.type.Upgrade2 + "unlocked"])
				{
					Upgrade2.SetActive(false);
				}
				else
				{
					Upgrade2.FindChild("Text").SetText(beh.type.Upgrade2);
				}
				if (beh.type.Upgrade3 == "" || !SiriusPrefs.B[beh.type.Upgrade3 + "unlocked"])
				{
					Upgrade3.SetActive(false);
				}
				else
				{
					Upgrade3.FindChild("Text").SetText(beh.type.Upgrade3);
				}
//				if (beh.GetType().GetField("type").FieldType.GetField("Upgrade1").GetValue(beh.GetType ().GetField("config").GetValue(null)) == "")
//				{
//					Upgrade1.SetActive(false);
//					//Debug.Log ("Vliza v if na upgrade 1" + beh.GetType().GetField("config").FieldType.GetField("Upgrade1").GetValue(beh.GetType ().GetField("config").GetValue(null)).ToString());
//				}
//				else
//				{
//					//Debug.Log ("Vliza v else na upgrade 1" + beh.GetType().GetField("config").FieldType.GetField("Upgrade1").GetValue(beh.GetType ().GetField("config").GetValue(null)).ToString());
//					Upgrade1.FindChild("Text").GetComponent<exSpriteFont>().text = (string) beh.GetType().GetField("config").FieldType.GetField("Upgrade1").GetValue(beh.GetType ().GetField("config").GetValue(null));
//				}
//				if (beh.GetType().GetField("config").FieldType.GetField("Upgrade2").GetValue(beh.GetType ().GetField("config").GetValue(null)) == "")
//				{
//					//Debug.Log ("Vliza v if na upgrade 2" + beh.GetType().GetField("config").FieldType.GetField("Upgrade2").GetValue(beh.GetType ().GetField("config").GetValue(null)).ToString());
//					Upgrade2.SetActive(false);
//				}
//				else
//				{
//					//Debug.Log ("Vliza v else na upgrade 2" + beh.GetType().GetField("config").FieldType.GetField("Upgrade2").GetValue(beh.GetType ().GetField("config").GetValue(null)).ToString());
//					Upgrade2.FindChild("Text").GetComponent<exSpriteFont>().text = (string) beh.GetType().GetField("config").FieldType.GetField("Upgrade2").GetValue(beh.GetType ().GetField("config").GetValue(null));
//				}
//				if (beh.GetType().GetField("config").FieldType.GetField("Upgrade3").GetValue(beh.GetType ().GetField("config").GetValue(null)) == "")
//				{
//					//Debug.Log ("Vliza v if na upgrade 3" + beh.GetType().GetField("config").FieldType.GetField("Upgrade3").GetValue(beh.GetType ().GetField("config").GetValue(null)).ToString());
//					Upgrade3.SetActive(false);
//				}
//				else
//				{
//					//Debug.Log ("Vliza v else na upgrade 3" + beh.GetType().GetField("config").FieldType.GetField("Upgrade3").GetValue(beh.GetType ().GetField("config").GetValue(null)).ToString());
//					Upgrade3.FindChild("Text").GetComponent<exSpriteFont>().text = (string) beh.GetType().GetField("config").FieldType.GetField("Upgrade3").GetValue(beh.GetType ().GetField("config").GetValue(null));
//				}
				//TODO: Do it the new way. 
				
			}
		}
	}
	
	public static class Cursors
	{
		public static GameObject TowerRangeCursor;
	}
	
	public static class Prototypes
	{

		
		public static class Hearts
		{
			public static GameObject HeartStack;
			public static GameObject Heart1;
			public static GameObject HeartNum;
		}
		
		public static class Extras
		{
			//towerRangeCursor		
			public static GameObject TowerRangeCursor;
			
			//spellbox
			public static GameObject SpellToolbar;
			
			//airstrike
			public static GameObject AirPlanel;
			public static GameObject Bomb;
			public static GameObject AirStrike;
			public static GameObject AirStrikeCursor;
			public static GameObject AirStrikeStroke;
			
			//orbital strike
			public static GameObject OrbitalStrike;
			public static GameObject OrbitalStrikeIcon;
			
			//artillery strike
			public static GameObject ArilleryIcon;
			public static GameObject ArtilleryStrikeCursor;
			public static GameObject Explosion;
			
			//EMM / WarBanner /TImeshift
			public static GameObject WarBanner;
			public static GameObject WarBannerIcon;
			public static GameObject EMPicon;
			public static GameObject Timeshift;
			
			//AirDrop
			public static GameObject Parachute;
			public static GameObject MachineGun;
			public static GameObject MachineGunMuzzle;
			public static GameObject AirDropIcon;
			
			//Other shits
			public static GameObject FireAoE;
			public static GameObject EnergyDrain;
			public static GameObject EnergyDrainIcon;
			
			//spells
			public static GameObject SpellsParent;
			
			
		}
		
		public static class Towers
		{
			
			public static GameObject Tower1;
			public static GameObject Tower2;
			public static GameObject Tower3;
			public static GameObject Tower4;
			public static GameObject Tower5;
			public static GameObject Tower6;
			public static GameObject CannonTower;
			public static GameObject AuraTower;
			public static GameObject ShockTower;
			public static GameObject AirDropTower;
			
			public static GameObject[] TowerArr;
		}
		
		public static class Enemies
		{
			public static GameObject[] EnemyArr;
		}
		
		public static class HealthBar
		{
			public static GameObject healthBar;
			public static GameObject Filler;
			public static GameObject Frame;
		}
		
		public static class Projectiles
		{
			public static GameObject Tower1shot;
			public static GameObject Tower2shot;
			public static GameObject Tower3shot;
			public static GameObject CannonTowerShot;
		}
		
		public static class Rosette
		{
			public static GameObject Center;
			public static GameObject Sell;
			public static GameObject Upgrade1;
			public static GameObject Upgrade2;
			public static GameObject Upgrade3;
		}
		
		public static class HUD
		{
			public static GameObject EnergyFrame;
			public static GameObject HudFrame;
			public static GameObject Money;
			public static GameObject MoneyIcon;
			public static GameObject Energy;
		}
		public static class PopUp
		{
			public static GameObject win_pop;
			public static GameObject lose_pop;
			public static GameObject pause_pop;
			public static GameObject wave;
		}
		public static class Counters
		{
			public static int enemy_count;
			public static int [] enemie_counter;
			public static float earned_money;			
		}
		
		public static class FloatingTexts
		{
			public static FloatingText CashSpent;
			public static FloatingText Cash;
			public static FloatingText Energy;
			public static FloatingText Lives;
			public static FloatingText Damage;
		}
		
		public static class NewTowers
		{
			public static GameObject MachineGun;
			public static GameObject UtilityTower;
			public static GameObject Cannon;
		}
		
//		public static class NewTowers
//		{
//			public static GameObject AntiAirTurret;
//			public static GameObject AuraTower;
//			public static GameObject Cannon;
//			public static GameObject Cannon2;
//			public static GameObject CryoRocketPlatform;
//			public static GameObject CryoRocketPlatform2;
//			public static GameObject CryoRocketPlatform3;
//			public static GameObject DamageBooster;
//			public static GameObject DamageBooster2;
//			public static GameObject DamageBooster3;
//			public static GameObject DualFlakCannon;
//			public static GameObject FlakCannon;
//			public static GameObject FusionPlant;
//			public static GameObject FusionPlant2;
//			public static GameObject FusionPlant3;
//			public static GameObject HellfireCannon;
//			public static GameObject HellfireCannon2;
//			public static GameObject HellfireCannon3;
//			public static GameObject LightningTower;
//			public static GameObject LightningTower2;
//			public static GameObject LightningTower3;
//			public static GameObject Machinegun;
//			public static GameObject Machinegun2;
//			public static GameObject Machinegun3;
//			public static 
//		}
	}
	
	void Awake()
	{
		Application.LoadLevelAdditive(MapNodeBehaviour.LevelName);
		// what: SiriusPrefs.S["LastLoadedLevel"] = gameObject.name;
		
		timer = 0;
		blockTowerSpawn=true;
		
		//background = GameObject.Find("/Background");
		//Debug.Log(background);
		cursor = GameObject.Find("/Cursors/MainCursor");
		Script = GameObject.Find("/Script");
		
		TowerToolbar.towerToolbar = GameObject.Find("/Toolbars/TowerToolbar");
		TowerToolbar.button = GameObject.Find("/Toolbars/TowerToolbar/Button");
		TowerToolbar.shown = true;
		SpellToolbar.spellToolbar = GameObject.Find("/Toolbars/SpellToolbar");
		SpellToolbar.button = GameObject.Find("/Toolbars/SpellToolbar/Button");
		SpellToolbar.shown = true;
		Prototypes.Extras.SpellsParent=GameObject.Find("/Toolbars/Spells");
		//leaving the Actual spells
		//SpellToolbar.spellToolbar.FindChild("Slot0").AddComponent<exSprite>().SetSprite("CastFrameClicked");
		//SpellToolbar.spellToolbar.FindChild("Slot1").AddComponent<exSprite>().SetSprite("CastFrameClicked");
		//SpellToolbar.spellToolbar.FindChild("Slot2").AddComponent<exSprite>().SetSprite("CastFrameClicked");
		
		int k=0;
		for (int i = 0; i < Prototypes.Extras.SpellsParent.transform.childCount; i++) 
		{
			//if(SpellSelectionBehaviour.selectedSpells.Contains(Prototypes.Extras.SpellsParent.GetChild(i).name))
			//{
			if(SiriusPrefs.S["Spell1"]==Prototypes.Extras.SpellsParent.GetChild(i).name || SiriusPrefs.S["Spell2"]==Prototypes.Extras.SpellsParent.GetChild(i).name || SiriusPrefs.S["Spell0"]==Prototypes.Extras.SpellsParent.GetChild(i).name)
			{
				string temp ="Slot"+SiriusPrefs.I[Prototypes.Extras.SpellsParent.GetChild(i).name].ToString();
				//string temp1 = Prototypes.Extras.SpellsParent.GetChild(i).name;
				GameObject tempObject;
				Vector3 tempVec = SpellToolbar.spellToolbar.FindChild(temp).transform.position;
				tempVec.z=-4.0f;
				
				tempObject=Prototypes.Extras.SpellsParent.GetChild(i).Clone();
				tempObject.SetParent(SpellToolbar.spellToolbar);
				tempObject.transform.position=tempVec;

				Destroy(SpellToolbar.spellToolbar.FindChild(temp));
				k++;
			}
		}
		
		
		Prototypes.Towers.Tower1 = GameObject.Find("/Prototypes/Towers/Tower 1");
		Prototypes.Towers.Tower2 = GameObject.Find("/Prototypes/Towers/Tower 2");
		Prototypes.Towers.Tower3 = GameObject.Find("/Prototypes/Towers/Tower 3");
		Prototypes.Towers.Tower4 = GameObject.Find("/Prototypes/Towers/Tower 4");
		Prototypes.Towers.Tower5 = GameObject.Find("/Prototypes/Towers/Tower 5");
		Prototypes.Towers.Tower6 = GameObject.Find("/Prototypes/Towers/Tower 6");
		Prototypes.Towers.AuraTower = GameObject.Find ("/Prototypes/Towers/AuraTower ");
		Prototypes.Towers.CannonTower = GameObject.Find("/Prototypes/Towers/Cannon Tower ");
		Prototypes.Towers.ShockTower = GameObject.Find("/Prototypes/Towers/Tower 3").Clone ();
		Prototypes.Towers.ShockTower.GetComponent<exSprite>().color= new Color(0.686f,0.172f,0.364f,1.0f);
		Prototypes.Towers.AirDropTower = GameObject.Find ("/Prototypes/Extras/AirDrop/MachineGun");
		
		Prototypes.Enemies.EnemyArr = new GameObject[6];
		
		Prototypes.Enemies.EnemyArr[0] = GameObject.Find("/Prototypes/Enemies/Enemy 1");
		Prototypes.Enemies.EnemyArr[1] = GameObject.Find("/Prototypes/Enemies/Enemy 2");
		Prototypes.Enemies.EnemyArr[2] = GameObject.Find("/Prototypes/Enemies/Enemy 3");
		Prototypes.Enemies.EnemyArr[3] = GameObject.Find("/Prototypes/Enemies/Enemy 4");
		Prototypes.Enemies.EnemyArr[4] = GameObject.Find("/Prototypes/Enemies/Enemy 5");
		Prototypes.Enemies.EnemyArr[5] = GameObject.Find("/Prototypes/Enemies/Enemy 6");
		
		Prototypes.HealthBar.healthBar = GameObject.Find("/Prototypes/HealthBar");
		Prototypes.HealthBar.Filler    = GameObject.Find("/Prototypes/HealthBar/Filler");
		Prototypes.HealthBar.Frame     = GameObject.Find("/Prototypes/HealthBar/Frame");
		
		Prototypes.Projectiles.Tower1shot = GameObject.Find("/Prototypes/TowerShots/Tower1shot");
		Prototypes.Projectiles.Tower2shot = GameObject.Find("/Prototypes/TowerShots/Tower2shot");
		Prototypes.Projectiles.Tower3shot = GameObject.Find("/Prototypes/TowerShots/Tower3shot");
		Prototypes.Projectiles.CannonTowerShot = GameObject.Find("/Prototypes/TowerShots/CannonTowerShot");
		
		NeutralButtons.pauseButton= GameObject.Find("/Toolbars/PauseButton");
		
		Prototypes.Rosette.Center =   GameObject.Find("/Prototypes/Rosette/Center");
		Prototypes.Rosette.Sell = 	  GameObject.Find("/Prototypes/Rosette/Center/Sell");
		Prototypes.Rosette.Upgrade1 = GameObject.Find("Prototypes/Rosette/Center/Upgrade1");
		Prototypes.Rosette.Upgrade2 = GameObject.Find("Prototypes/Rosette/Center/Upgrade2");
		Prototypes.Rosette.Upgrade3 = GameObject.Find("Prototypes/Rosette/Center/Upgrade3");
		
		Prototypes.HUD.EnergyFrame = GameObject.Find("/Hud/EnergyFrame");
		Prototypes.HUD.HudFrame    = GameObject.Find("/Hud/HudFrame");
		Prototypes.HUD.Money 	   = GameObject.Find("/Hud/MoneyNum");
		Prototypes.HUD.MoneyIcon   = GameObject.Find("/Hud/Money");
		Prototypes.HUD.Energy	   = GameObject.Find("/Hud/EnergyNum");
		
		Prototypes.PopUp.win_pop = GameObject.Find("/PopUp/Win");
		Prototypes.PopUp.lose_pop = GameObject.Find("/PopUp/Defeat");
		Prototypes.PopUp.wave = GameObject.Find("/PopUp/Wave");
		Prototypes.PopUp.pause_pop = GameObject.Find("/PopUp/Pause");
		
		Prototypes.Counters .enemy_count =0;
		Prototypes.Counters.earned_money=0;
		Prototypes.Counters.enemie_counter= new int [6];
		
		Prototypes.Hearts.HeartStack = GameObject.Find("/Hud/HeartStack");
		Prototypes.Hearts.Heart1 = GameObject.Find ("/Hud/HeartStack/Heart1");
		Prototypes.Hearts.HeartNum = GameObject.Find ("/Hud/HeartStack/HeartNum1");
		
		//Prototypes.Extras.TowerRangeCursor=GameObject.Find("/Prototypes/Towers/TowerRange");
		
		Prototypes.Extras.SpellToolbar=GameObject.Find("/Toolbars/SpellToolbar");
		
		Prototypes.Extras.AirPlanel = GameObject.Find("/Prototypes/Extras/AirStrike/Plane");
		Prototypes.Extras.Bomb = GameObject.Find("/Prototypes/Extras/AirStrike/Bombs" + "");
		Prototypes.Extras.AirStrike = GameObject.Find("/Toolbars/Spells/AirStrike");
		Prototypes.Extras.AirStrikeCursor = GameObject.Find("/Cursors/AirStrikeCursor/Head");
		Prototypes.Extras.AirStrikeStroke=GameObject.Find("/Cursors/AirStrikeCursor/Stroke");
		
		Prototypes.Extras.OrbitalStrike = GameObject.Find("/Prototypes/Extras/OrbitalStrike/Laser");
		Prototypes.Extras.OrbitalStrikeIcon = GameObject.Find("/Toolbars/Spells/OrbitalStrike");
		
		Prototypes.Extras.ArilleryIcon = GameObject.Find("/Toolbars/Spells/ArtileryStrike");
		Prototypes.Extras.ArtilleryStrikeCursor=GameObject.Find("/Cursors/ArtilleryStrikeCursor");
		Prototypes.Extras.Explosion = GameObject.Find("/Prototypes/Explosion");
		
		Prototypes.Extras.WarBanner = GameObject.Find("/Prototypes/Extras/Banner/Banner");
		Prototypes.Extras.WarBannerIcon = GameObject.Find("/Toolbars/Spells/Banner");
		
		Prototypes.Extras.EMPicon = GameObject.Find("/Toolbars/Spells/EMP");
		Prototypes.Extras.Timeshift = GameObject.Find("/Toolbars/Spells/Timeshift");
		
		
		Prototypes.Extras.Parachute = GameObject.Find("/Prototypes/Extras/AirDrop/Parachute");
		Prototypes.Extras.MachineGun = GameObject.Find("/Prototypes/Extras/AirDrop/MachineGun");
		Prototypes.Extras.MachineGunMuzzle = GameObject.Find("/Prototypes/Extras/AirDrop/MachineGun/MachineGunMuzzle");
		Prototypes.Extras.AirDropIcon = GameObject.Find("/Toolbars/Spells/AirDrop");
		
		Prototypes.Extras.FireAoE = GameObject.Find ("/Prototypes/Extras/Fire");
		Prototypes.Extras.EnergyDrain = new GameObject();
		Prototypes.Extras.EnergyDrainIcon=GameObject.Find("/Toolbars/Spells/EnergyDrain");
				
		Prototypes.FloatingTexts.Cash = FloatingText.Find("/Prototypes/FloatingTexts/Cash");
		Prototypes.FloatingTexts.CashSpent = FloatingText.Find("/Prototypes/FloatingTexts/CashSpent");
		Prototypes.FloatingTexts.Energy = FloatingText.Find("/Prototypes/FloatingTexts/Energy");
		Prototypes.FloatingTexts.Lives = FloatingText.Find("/Prototypes/FloatingTexts/Lifes");
		Prototypes.FloatingTexts.Damage = FloatingText.Find("/Prototypes/FloatingTexts/Damage");
		
		Prototypes.NewTowers.Cannon = GameObjectExt.Find ("/Prototypes/NewTowers/Cannon");
		Prototypes.NewTowers.MachineGun = GameObjectExt.Find ("/Prototypes/NewTowers/Machinegun");
		Prototypes.NewTowers.UtilityTower = GameObjectExt.Find ("/Prototypes/NewTowers/Utility Tower");
		
		
		Cursors.TowerRangeCursor=GameObject.Find("/Prototypes/Towers/TowerRange");
		//Deactivate Prototypes
		//GameObject.Find("/Prototypes").SetActive(false);
		
		//Add Prices bellow tower icons
		GameObject.Find("/Toolbars/TowerToolbar/ButtonTower1/Tower1price").GetComponent<exSpriteFont>().text = Currency.Prices.Tower1Price.ToString();
		GameObject.Find("/Toolbars/TowerToolbar/ButtonTower2/Tower2price").GetComponent<exSpriteFont>().text = Currency.Prices.Tower2Price.ToString();
		GameObject.Find("/Toolbars/TowerToolbar/ButtonTower3/Tower3price").GetComponent<exSpriteFont>().text = Currency.Prices.Tower3Price.ToString();
		GameObject.Find("/Toolbars/TowerToolbar/ButtonAuraTower/AuraTowerPrice").GetComponent<exSpriteFont>().text = Currency.Prices.AuraTowerPrice.ToString();
		//Debug
//		Debug.Log (Stats.GetTower1Built());
//		Debug.Log (Stats.GetTower2Built());
//		Debug.Log (Stats.GetTower3Built());
//		Debug.Log("Total time:" + Stats.GetTotalTime());
//		Debug.Log("Total currency: " + Stats.GetTotalCurrency());
		
		allProjectiles = new List<GameObject>();
	}
	
	void Start () 
	{
		towerSpawnUpdaterBool = true;
		//Requisition.Recieve(133700);
		background = GameObject.Find("/Background");
		
		//GameObject.Find("/Cursors").SetActive(true);
		GameObject.Find("/Prototypes").SetActive(false);
		Prototypes.PopUp.win_pop.SetActive(false);
		Prototypes.PopUp.lose_pop.SetActive(false);
		Prototypes.PopUp.pause_pop.SetActive(false);
		Prototypes.PopUp.wave.SetActive(false);
		warBannerMarker = GameObject.Find("/Cursors/ArtilleryStrikeCursor").Clone();
		
	}
	
	void Update () 
	{
		timer+= Time.deltaTime;
		if (towerSpawnUpdaterBool)
		{
			TowerSpawn.Updater();
		}
		
		if(Input.GetMouseButton(0))
		{
			Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
			RaycastHit hit;
			
			if(!Physics.Raycast(ray, out hit))
			{
				return;		
			}
			else
			{
				GameObject hitObj = hit.transform.gameObject;
				if (hitObj.name == "Upgrade1")
				{
					hitObj.GetComponent<exSprite>().SetSprite("RedRosettePressed");
				}
				else if (LastRosette.shown)
				{
					LastRosette.Upgrade1.GetComponent<exSprite>().SetSprite("RedRosette");
				}
				if (hitObj.name == "Upgrade2")
				{
					hitObj.GetComponent<exSprite>().SetSprite("BlueRosettePressed");
				}
				else if (LastRosette.shown)
				{
					LastRosette.Upgrade2.GetComponent<exSprite>().SetSprite("BlueRosette");
				}
				if (hitObj.name == "Upgrade3")
				{
					hitObj.GetComponent<exSprite>().SetSprite("PurpleRosettePressed");
				}
				else if (LastRosette.shown)
				{
					LastRosette.Upgrade3.GetComponent<exSprite>().SetSprite("PurpleRosette");
				}
				if (hitObj.name == "Sell")
				{
					hitObj.GetComponent<exSprite>().SetSprite("YellowRosettePressed");
				}
				else if (LastRosette.shown)
				{
					LastRosette.Sell.GetComponent<exSprite>().SetSprite("YellowRosette");
				}
				
				if (hitObj.name == "Background" && warBannerBool)
				{
					if(Warbanner.banner == null)
					{
						Warbanner.banner=Game.Prototypes.Extras.WarBanner.Clone();
					}
					Warbanner.MarkSetter();
					Warbanner.banner.transform.position = hit.point;
				}
				else
				{
					warBannerMarker.transform.position = new Vector3(0,0,10);
				}
				
			}
		}
		if(Input.GetMouseButtonUp(0))
		{
			Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
			RaycastHit hit;
			
			if(!Physics.Raycast(ray, out hit))
			{
				return;		
			}
			else
			{
				GameObject hitObj = hit.transform.gameObject;
				
				if (warBannerBool)
				{
					
					Warbanner.Cast(hit.point);
					Warbanner.GiveDamage();
					warBannerBool = false;
					
				}
				
				if (hitObj.name == "Banner")
				{
					TowerSpawn.tempCursor.SetActive(false);
					BuyTowerBox.BlockTowerSpawn();
					warBannerBool = true;
					Game.SpellToolbar.spellToolbar.FindChild(Game.Prototypes.Extras.WarBannerIcon.name).SetSprite(Game.Prototypes.Extras.WarBannerIcon.name+"Clicked");
					RosetteBehaviour.Block();
				}
				
			}
		}
		if(Input.GetMouseButtonDown(0))
		{
			Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
			RaycastHit hit;
			
			if(!Physics.Raycast(ray, out hit))
			{
				return;		
			}
			else
			{
				
				GameObject hitObj = hit.transform.gameObject;
//				if (hitObj.name.Contains("Tower ") && hitObj != TowerSpawn.alphaTower && hitObj != TowerSpawn.alphaTower  )
//				{
//					hitObj.GetComponent<TowerBehaviour>().ShowRosette();
//					_lastTowerHit = hitObj;
//				}
				if (hitObj.name == "Button")
				{	
					TowerSpawn.tempCursor.SetActive(false);
					BuyTowerBox.BlockTowerSpawn();
					TowerSpawn.cursor.SetActive(false);
					Destroy(TowerSpawn.lastLowAlphaSprite);
					if(TowerSpawn.alphaTower!=null)
					{
						TowerSpawn.alphaTower.SetActive(false);
					}
					if(hitObj.GetParent().name == "SpellToolbar")
					{
						AnimateSpellToolbar();
					}
					if(hitObj.GetParent().name == "TowerToolbar")
					{
						AnimateTowerToolbar();	
					}
				}
				
				if (hitObj.GetComponent<TowerBehaviour>() != null)
				{
					hitObj.GetComponent<TowerBehaviour>().ShowRosette();
					_lastTowerHit = hitObj;
				}
				/*if (timeshiftBool)
				{

					towerSpawnUpdaterBool = false;
					Debug.Log (hit.point);
					Timeshift.Cast(hit.point);
					timeshiftBool = false;
					
				}
				*/
				
				/*
				if (warBannerBool)
				{
					
					Warbanner.Cast(hit.point);
					warBannerBool = false;
					
				}
				
				*/
				/*
				if(energryDrainBool)
				{
					EnergyDrain.Cast(hit.point);
					energryDrainBool = false;
				}
				*/
				/*if (hitObj.name == "Timeshift" )
				{
					Prototypes.Extras.Timeshift.SetSprite("TimeShiftClicked");
					Debug.Log("vliza v Timeshift if");
					timeshiftBool = true;
					blockTowerSpawn = true;
				}*/
				
				/*
				if (hitObj.name == "Banner")
				{
					warBannerBool = true;
					Game.SpellToolbar.spellToolbar.FindChild(Game.Prototypes.Extras.WarBannerIcon.name).SetSprite("bannerCLicked");
					RosetteBehaviour.Block();
				}
				
				if (hitObj.GetComponent<TowerBehaviour>() != null)
				{
					hitObj.GetComponent<TowerBehaviour>().ShowRosette();
					_lastTowerHit = hitObj;
				}
				*/
				
				/*
				if (hitObj.name == "EnergyDrain")
				{
					Debug.Log ("vliza v Banner if");
					energryDrainBool = true;
				}
				*/
				
			}
		}
		else if(Input.GetMouseButtonUp(0))
		{
			Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
			RaycastHit hit;
			
			if(!Physics.Raycast(ray, out hit))
			{
				return;		
			}
			else
			{
				TowerBehaviour beh = null;
				GameObject hitObj = hit.transform.gameObject; 
				if (LastRosette.shown)
				{
					beh = LastRosette.Center.GetComponent<RosetteBehaviour>().owner.GetComponent<TowerBehaviour>();
				}
				
				switch(hitObj.name)
				{
				case "Sell":
					_lastTowerHit.GetComponent<TowerBehaviour>().Sell();
					RosetteBehaviour.instance.DestroyRosette();
					break;
				case "Upgrade1":
					

					_lastTowerHit.GetComponent<TowerBehaviour>().UpgradeTo(beh.type.Upgrade1);
					RosetteBehaviour.instance.DestroyRosette();
					break;
				case "Upgrade2":

					_lastTowerHit.GetComponent<TowerBehaviour>().UpgradeTo(beh.type.Upgrade2);
					RosetteBehaviour.instance.DestroyRosette();
					break;
					
				case "Upgrade3":
					_lastTowerHit.GetComponent<TowerBehaviour>().UpgradeTo(beh.type.Upgrade3);
					RosetteBehaviour.instance.DestroyRosette();
					break;
				case "NextLevel":
					Main.Load("Campaign");
					break;
				case "RestartButton":
					//Enemy.path.Clear();
					EnemySpawn._wave=0;
					EnemySpawn._creep=0;
					EnemySpawn.currCreep=0;
					//Path.points.Clear();
					SpawnHearts.spawnHearts();
					HeartStack.lifes=HeartStack.config.Maxlifes;
					Main.Load("Game");
					
					break;
				case "ResumeButton":
					//TowerSpawn.tempCursor.SetActive(true);
					TowerSpawn.popUpBlockSpawn=false;
					PauseBehaviour.PausePopUpHide();
					break;
				}
			}	
		}
	}
	
	public static void Explode(Vector3 position, float radius, float damage)
	{
		foreach (GameObject enemy in Enemy.all)
		{
			Vector3 delta = enemy.transform.position - position;
			float dist = Mathf.Sqrt(delta.x * delta.x + delta.y * delta.y);	
			
			if (dist < radius)
			{
				enemy.GetComponent<Enemy>().Damage(damage);
			}
		}
	}
	public static void Stun(Vector3 position, float radius)
	{
		foreach (GameObject enemy in Enemy.all)
		{
			Vector3 delta = enemy.transform.position - position;
			float dist = Mathf.Sqrt(delta.x * delta.x + delta.y * delta.y);	
			
			if (dist < radius)
			{
				///enemy.GetComponent<Enemy>().Slow(0.01f , sec);
				enemy.GetComponent<Enemy>().isStuned=true;
			}
		}
	}
	public static void notStun(Vector3 position, float radius)
	{
		foreach (GameObject enemy in Enemy.all)
		{
			Vector3 delta = enemy.transform.position - position;
			float dist = Mathf.Sqrt(delta.x * delta.x + delta.y * delta.y);	
			
			if (dist < radius)
			{
				///enemy.GetComponent<Enemy>().Slow(0.01f , sec);
				enemy.GetComponent<Enemy>().isStuned=false;
			}
		}
	}	
	void OnDestroy()
	{
		
		Stats.AddTotalTime(timer);
		//Debug.Log("Total time:" + Stats.GetTotalTime());
		//Debug.Log("Total currency: " + Stats.GetTotalCurrency());
		SiriusPrefs.Save();
	}

	public static void TowerToolbarHide ()
	{
		TowerToolbar.towerToolbar.animation.Play("TowerHide");
		BuyTowerBox.BlockTowerSpawn();
		SpellSelection.Block();
		Sirius.ExecAfter(0.5f,()=>
		{
			SpellSelection.Unblock();
		}
		);
		TowerToolbar.shown = !TowerToolbar.shown;
	}

	public static void TowerToolbarShow ()
	{
		TowerToolbar.towerToolbar.animation.Play("TowerShow");
		BuyTowerBox.UnblockTowerSpawn();
		SpellSelection.Block();
		Sirius.ExecAfter(0.5f,()=>
		{
			SpellSelection.Unblock();
		}
		);
		TowerToolbar.shown = !TowerToolbar.shown;
	}
	public static void DropSpellCooldown(ref float cooldown)
	{
		if(cooldown >0)
		{
			cooldown-=Time.deltaTime;	
		}
	}
	
	void AnimateTowerToolbar()
	{
		if(TowerToolbar.shown)
		{
			TowerToolbarHide ();
		}
		else
		{
			TowerToolbarShow ();
		}
		
	}

	public static void SpellToolbarHide ()
	{
		SpellToolbar.spellToolbar.animation.Play("SpellHide");
		SpellSelection.Block();
		BuyTowerBox.BlockTowerSpawn();
		Sirius.ExecAfter(0.5f,()=>
		{
			BuyTowerBox.UnblockTowerSpawn();
		}
		);
		SpellToolbar.shown = !SpellToolbar.shown;
	}

	public static void SpellToolbarShow ()
	{
		SpellToolbar.spellToolbar.animation.Play("SpellShow");
		SpellSelection.Unblock();
		BuyTowerBox.BlockTowerSpawn();
		Sirius.ExecAfter(0.5f,()=>
		{
			BuyTowerBox.UnblockTowerSpawn();
		}
		);
		SpellToolbar.shown = !SpellToolbar.shown;
	}
	
	void AnimateSpellToolbar()
	{
		if(SpellToolbar.shown)
		{
			SpellToolbarHide ();
		}
		else
		{
			SpellToolbarShow ();
		}
		
	}
	
	public static bool InRange(GameObject target1, GameObject target2, float range)
	{
		if ((target1.transform.position - target2.transform.position).magnitude < range)
		{
			return true;
		}
		return false;
	}
	
	public static bool InRange(Vector3 target1, Vector3 target2, float range)
	{
		if ((target1 - target2).magnitude < range)
		{
			return true;
		}
		return false;
	}
	
	public static bool InRange(GameObject target1, Vector3 target2,float range)
	{
		if ((target1.transform.position - target2).magnitude < range)
		{
			return true;
		}
		return false;
	}
	
	public static void SetBuildingBlockFalse()
	{
		blockTowerSpawn = false;
	}
	

	public static bool OnScreen(GameObject obj)
	{
		if (Mathf.Abs(obj.transform.position.x) > background.GetComponent<exSprite>().width/2)
		{
			return false;
		}
		if (Mathf.Abs(obj.transform.position.y) > background.GetComponent<exSprite>().height/2)
		{
			return false;
		}
		return true;
	}
	
	public static Vector3 GetMouseCoord()
	{
		Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
		RaycastHit hit;
		Vector3 vec;
		
		if(!Physics.Raycast(ray, out hit))
			{
				vec=new Vector3 (0 , 0 ,0);		
			}
			else
			{
				vec=hit.point;			
			}
		return vec;
		
	}
	
	void OnApplicationQuit()
	{
		SiriusPrefs.Save ();
	}
}
