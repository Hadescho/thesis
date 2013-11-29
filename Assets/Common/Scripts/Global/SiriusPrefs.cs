using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Threading;
using System.IO;
using System.Security.Cryptography;

public class SiriusPrefs : MonoBehaviour
{
	public class Proxy<T>
	{
		public T this[string key, T dvalue = default(T)]
		{
			get
			{
				if(Data.Contains(key))
				{
					return((T)Data[key]);
				}
				else
				{
					return(dvalue);	
				}
			}
			set
			{
				if(Data.Contains(key))
				{
					Data[key] = value;
				}
				else
				{
					Data.Add(key, value);					
				}
				
				_requiresCommit = true;
			}
		}
	}
	
	public static SiriusPrefs instance;

	public static Proxy<bool> B = new Proxy<bool>();
	public static Proxy<int> I = new Proxy<int>();
	public static Proxy<float> F = new Proxy<float>();
	public static Proxy<string> S = new Proxy<string>();
	public static Hashtable Data = new Hashtable();

	private static Thread _thread;
	private static bool _requiresCommit;
	private static Rijndael _rijndael;

	public string FileName = "SiriusPrefs.bin";
	public string EncryptionKey = "7@SiriusSoftware";
	public float TimeAutoSave = 1.0f;
	public string Path = "";

	void Awake()
	{
		if(instance != null)
		{
			Debug.LogError("More than one SiriusPrefs instances are not allowed.");	
			Destroy(this);
			return;
		}
		
		instance = this;

		if(EncryptionKey.Length != 16)
		{
			Debug.LogError("Encryption key is not 16 characters long.");
			Destroy(this);
			return;
		}

		_rijndael = RijndaelManaged.Create();
		_rijndael.Padding = PaddingMode.PKCS7;
		_rijndael.Key = System.Text.Encoding.ASCII.GetBytes(EncryptionKey);
		_rijndael.IV = new byte[] { 0x1, 0x3, 0x3, 0x7, 0x1, 0x3, 0x3, 0x7, 0x1, 0x3, 0x3, 0x7, 0x1, 0x3, 0x3, 0x7 };

		if(Path == "")
		{
			Path = Application.persistentDataPath + "/" + FileName;
		}

		Load();
		_requiresCommit = false;

		_thread = new Thread(_AutoSaveProc);
		_thread.Start();
	}
	
	void OnDestroy()
	{
		_thread.Abort();
	}
	
	void OnApplicationQuit()
	{
		Save();
	}
	
	private static void _AutoSaveProc()
	{
		while(true)
		{
			while(!_requiresCommit)
			{
				Thread.Sleep((int)(instance.TimeAutoSave * 1000));
			}
			
			Save();
			_requiresCommit = false;
		}
	}
	
	public static void Save()
	{
		Monitor.Enter(instance);

		FileStream fs = File.Open(instance.Path, FileMode.Create);
		CryptoStream cs = new CryptoStream(fs, _rijndael.CreateEncryptor(_rijndael.Key, _rijndael.IV), CryptoStreamMode.Write);
		BinaryWriter w = new BinaryWriter(cs);

		try
		{
			w.Write(Data.Count);

			foreach(DictionaryEntry entry in Data)
			{
				w.Write((string)entry.Key);

				if(entry.Value is bool)
				{
					w.Write((byte)0);
					w.Write((bool)entry.Value);
				}
				else if(entry.Value is int)
				{
					w.Write((byte)1);
					w.Write((int)entry.Value);
				}
				else if(entry.Value is float)
				{
					w.Write((byte)2);
					w.Write((float)entry.Value);
				}
				else if(entry.Value is double)
				{
					w.Write((byte)3);
					w.Write((double)entry.Value);
				}
				else if(entry.Value is string)
				{
					w.Write((byte)4);
					w.Write((string)entry.Value);
				}
			}
			
			cs.FlushFinalBlock();
		}
		catch(Exception ex)
		{
			Debug.LogException(ex);
		}
		finally
		{
			cs.Close();
			fs.Close();
			w.Close();
		}

		Monitor.Exit(instance);
	}
	
	public static void Load()
	{
		Monitor.Enter(instance);

		Data.Clear();

		if(File.Exists(instance.Path))
		{
			FileStream fs = File.Open(instance.Path, FileMode.OpenOrCreate);
			CryptoStream cs = new CryptoStream(fs, _rijndael.CreateDecryptor(_rijndael.Key, _rijndael.IV), CryptoStreamMode.Read);
			BinaryReader r = new BinaryReader(cs);

			try
			{
				int count = r.ReadInt32();

				for(int i = 0; i < count; i++)
				{
					string key = r.ReadString();
					byte type = r.ReadByte();

					if(type == 0)
					{
						Data.Add(key, r.ReadBoolean());
					}
					else if(type == 1)
					{
						Data.Add(key, r.ReadInt32());
					}
					else if(type == 2)
					{
						Data.Add(key, r.ReadSingle());
					}
					else if(type == 3)
					{
						Data.Add(key, r.ReadDouble());
					}
					else if(type == 4)
					{
						Data.Add(key, r.ReadString());
					}
				}
			}
			catch(Exception ex)
			{
				Debug.LogException(ex);
			}
			finally
			{
				r.Close();
				cs.Close();
				fs.Close();
			}
		}
		
		Monitor.Exit(instance);
	}
	
	public static void DeleteAll()
	{
		Monitor.Enter(instance);
		
		Data.Clear();
		File.Delete(instance.Path);
		
		Monitor.Exit(instance);
	}
}

