using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EnemySpawn : SiriusBehaviour
{
	
	public class Config
	{
		public float timeSpawn = 0.5f;
		public float waveTime = 10.0f;
	}
	
	public static Config config = new Config();
	static float timer=2.0f;
	public static int enemy_type;
	public static int _levels;
	public static int _wave=0;
	public static int _creep=0;
	public static int currCreep=0;
	public static int pathID=0;
	public static Vector3 startPoint;
	public static float startMoney;
	public bool isFirstWave;
	
	
	void Awake()
	{
		Path.points.Clear();
	}
	void Start ()
	{
		
		_levels=MapNodeBehaviour.loadedLevel;
		_wave=0;
		_creep=0;
		currCreep=0;
		Enemy.PreparePopWin();
		HeartStack.PreparePopDefeat();
		//Path.SpawnPathPoints();
		
		startMoney=Stats.GetTotalCurrency();
		isFirstWave=false;
	}
	
	
	void Update ()
	{
		if(!isFirstWave)
			{
				FirstWave();
				isFirstWave=true;
			}
		if(_Timer())
		{
			
			Spawn();
		}
		
	}
	
	
	
	private static bool _Timer()
	{
		timer+=Time.deltaTime;
		if(timer>=config.timeSpawn)
		{
			Game.Prototypes.PopUp.wave.SetActive(false);
			timer=0;
			return true;	
			
		}
		else
		{
			return false;
		}	
		
	}
	
	public static void Spawn()
	{
		GameObject cloneEnemy;	
			if(_wave <Levels.config.enemies[_levels].Length)
			{
	
				if(_creep<Levels.config.enemies[_levels][_wave].Length)
				{				
					cloneEnemy=Game.Prototypes.Enemies.EnemyArr[Levels.config.enemies[_levels][_wave][_creep]].Clone();
					cloneEnemy.transform.position=startPoint;
					cloneEnemy.AddComponent<Enemy>();
					enemy_type=Levels.config.enemies[_levels][_wave][_creep];
					cloneEnemy.GetComponent<Enemy>().EnemyPath = Levels.config.enemiesPaths[_levels][_wave][_creep];
					_creep++;
				
				}
				if(currCreep==Levels.config.enemies[_levels][_wave].Length && Levels.config.enemies[_levels].Length-1 != _wave)
				{
					NextWave();
					_creep=0;
					currCreep=0;
					_wave++;
				}
				
			}
		
	}
	public static void NextWave()
	{
			timer-=config.waveTime;
			int i = _wave;
			i+=2;
			Game.Prototypes.PopUp.wave.GetChild(0).GetComponent<exSpriteFont>().text=" "+i.ToString();
			Game.Prototypes.PopUp.wave.SetActive(true);
			Game.Prototypes.PopUp.wave.animation.Play("WaveAnimation");
	}
	public static void FirstWave()
	{
			timer-=5;
			int i = 1;
			Game.Prototypes.PopUp.wave.GetChild(0).GetComponent<exSpriteFont>().text=" "+i.ToString();
			Game.Prototypes.PopUp.wave.SetActive(true);
			Game.Prototypes.PopUp.wave.animation.Play("WaveAnimation");
	}
	
}
