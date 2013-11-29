using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Enemy:SiriusBehaviour
{
	
	public class Config
	{
		public float max_health=300.0f;
		public float _DamageTimer=0.5f;
		
		public float goldMedalBonus=2.0f;
		public float silverMedalBonus=1.5f;
		public float bronzeMedalBonus=1.25f;
		public float defaultMedalBonus = 1.0f;	
		
		
		public float [] max_healths= new float []
		{
			1000.0f , 400.0f ,1400.0f , 1000.0f , 400.0f ,1500.0f
		};
		public int []creep_speeds= new int []
		{
			50 , 100 , 30 , 50 , 100 , 30
		};
		public float []creep_money= new float []
		{
			1000.0f , 15.0f , 20.0f , 1000.0f , 15.0f , 20.0f
		};
		public int []creepInGameMoney= new int []
		{
			1 , 2 , 3 ,4 ,5 ,6
		};
		
	}
	
	public bool isFlying;
	
	public static Config config = new Config();
	
	public static List<GameObject> all=new List<GameObject>();
	
	public List<Vector3> path=new List<Vector3>();
	
	private int currPoint;
	private float dist;
	private float moveDist;
	private Vector3 vec;
	public float _health;
	public float target;
	
	public bool isDead;
	public bool isStuned;
	
	private GameObject clone_healthbar;	
	
	private int _curr_speed;
	private float _curr_money;
	//Slow
	private bool _slowed;
	private float _slowedTime;
	
	public bool _damaged;
	private float _speedModifire;
	private float _mydamage;
	private float _textDamage;
	
	private int _curr_enemy;
	public int currEnemyTypeForHealthbar;
	
	public int EnemyPath;
	
	void Start()
	{
		if(EnemySpawn.enemy_type==1)
		{
			isFlying=true;
		}
		else
		{
			isFlying=false;	
		}
		
		for (int i = 0; i < Path.points[EnemyPath].Count; i++)
		{
			path.Add(Path.points[EnemyPath][i]);	
		}			
		gameObject.transform.position=path[0];
		
		
		gameObject.GetComponent<exSprite>().color= new Color(44,93,175,255);
		all.Add(gameObject);				
		clone_healthbar  = Game.Prototypes.HealthBar.healthBar.Clone();	
		clone_healthbar.transform.parent=gameObject.transform;
		//attach healthbar
		Vector3 healthpos;
		healthpos=gameObject.transform.position;	
		healthpos.y+=60.0f;
		clone_healthbar.transform.position= healthpos;
		clone_healthbar.AddComponent<Healthbar>();
		
		//creep health
		_health = config.max_healths[EnemySpawn.enemy_type];
		
		//creeep speeds
		_curr_speed=config.creep_speeds[EnemySpawn.enemy_type];
		
		//creep money
		_curr_money=config.creep_money[EnemySpawn.enemy_type];
		
		currPoint=0;
		dist=0.0f;
		moveDist=0.0f;
		
		_curr_enemy=EnemySpawn.enemy_type;
		currEnemyTypeForHealthbar=_curr_enemy;
		
		isDead = false;
		_speedModifire=1.0f;
		_damaged=false;
		isStuned=false;
	}
	
	void OnDestroy()
	{
		all.Remove(gameObject);
	}
	
	void Update()
	{
		FloatingTextSpawn(_mydamage);
		if(!isStuned)
		{
			Move();
		}
		Slower();
		
	}
	
	public void Move()
	{	
		if(currPoint<=path.Count)
		{		
			if(dist <= 0.0f )
			{
				vec=GetVector(path[currPoint]);		
				dist=vec.magnitude;
				
				vec.Normalize();
				MoveDirection(vec);
				vec*=_curr_speed;
				
				moveDist=vec.magnitude;
				currPoint++;

			}
			if(dist>0.0f)
			{
				//vec*=_speedModifire;
				//moveDist*=_speedModifire; //slow
				
				Vector3 temp;
				temp=gameObject.transform.position ;
				temp.z=-2+ temp.y/1000;
				gameObject.transform.position=temp;
				
				gameObject.transform.position+=vec*Time.deltaTime;
				dist-=moveDist*Time.deltaTime;
				
			}
		}
		if(currPoint==path.Count && dist <= 0.0f)
		{				
			Destroy(gameObject);
			HeartStack.TakeLife();
			
			if(Levels.config.enemies[EnemySpawn._levels].Length-1==EnemySpawn._wave && Levels.config.enemies[EnemySpawn._levels][EnemySpawn._wave].Length+1==EnemySpawn.currCreep)
			{	
				if(HeartStack.lifes>0)
				{
				SetPopWin(Game.Prototypes.Counters.enemie_counter);
				}
			}
			EnemySpawn.currCreep++;
		}	
	}
	
	public  Vector3 GetVector( Vector3 vecPoint)
	{
		Vector3 pos;
		pos = vecPoint - gameObject.transform.position;
		return pos;
		
	}
	public void Damage(float damage)
	{
		if(!enabled)
		{
			return;	
		}
		
		_mydamage=damage;
		target = _health-damage;
		clone_healthbar.GetComponent<Healthbar>().Show( target);
		_damaged=true;
		_health-=damage;	
	
			
		if(_health<0)
		{
			clone_healthbar.SetActive(false);
			enabled=false;
			gameObject.GetComponent<exSpriteAnimation>().Play("Explosion");	
			
			Stats .AddTotalCurrency (config.creepInGameMoney[_curr_enemy]);
			Game.Prototypes.Counters.earned_money+=_curr_money;
			Currency.GiveCurrency(_curr_money);
			
			
			Game.Prototypes.Counters.enemie_counter[_curr_enemy]++;
			Game.Prototypes.Counters.enemy_count++;
			
			EnemySpawn.currCreep++;
			//Debug.Log(EnemySpawn.currCreep + " creep");
			//Debug.Log(Levels.config.enemies[EnemySpawn._levels][EnemySpawn._wave].Length+1 + "wave");
			//Debug.Log(Levels.config.enemies[EnemySpawn._levels].Length-1);
			//Debug.Log(EnemySpawn._wave);
			if(EnergyDrain.inRangeCreeps.Contains(gameObject))
			{
				EnergyDrain.GiveEnergyFromEnergyDrain(gameObject);
			}
			if(Levels.config.enemies[EnemySpawn._levels].Length-1==EnemySpawn._wave && Levels.config.enemies[EnemySpawn._levels][EnemySpawn._wave].Length==EnemySpawn.currCreep)
			{
				//PreparePopWin();
				SetPopWin(Game.Prototypes.Counters.enemie_counter);
				Game.Prototypes.PopUp.win_pop.SetActive(true);
				if (Game.TowerToolbar.shown)
				{
					Game.TowerToolbarHide();
				}
				if (Game.SpellToolbar.shown)
				{
					Game.SpellToolbarHide();
				}
				Game.Prototypes.Counters.earned_money=0;
				MapBehaviour.SetLevelStateCompleated(SiriusPrefs.S["LastLoadedLevel"]);
				//SiriusBehaviour.PauseAll();
			}
			
			isDead = true;
			//all.Remove(gameObject);
			Sirius.ExecAfter(0.5f, Boom);
		}	
		
	}
	
	public void SetPopWin(int [] enemies)
	{
		TowerSpawn.popUpBlockSpawn=true;
		
		Game.Prototypes.PopUp.win_pop.FindChild("Enemies").AddComponent<UIFlow>();
		Game.Prototypes.PopUp.win_pop.FindChild("Enemies").GetComponent<UIFlow>().VerticalDistance=80.0f;
		Game.Prototypes.PopUp.win_pop.FindChild("Enemies").GetComponent<UIFlow>().HorizontalDistance=0.0f;
		Game.Prototypes.PopUp.win_pop.FindChild("Enemies").GetComponent<UIFlow>().Alignment=UI.Alignment.TopLeft;
		Game.Prototypes.PopUp.win_pop.FindChild("Enemies").GetComponent<UIFlow>().Rows=4;
		Game.Prototypes.PopUp.win_pop.FindChild("Enemies").GetComponent<UIFlow>().Columns=1;
		
		//Reward Section
		
		float reward = Stats.GetTotalCurrency() - EnemySpawn.startMoney;
		float rewardBonus=CalculateRewardBonus();
		float rewatdTempBonus=ConvertBonusReward(rewardBonus);
		reward=reward*rewardBonus;
		Stats.AddTotalCurrency(reward);
		
		
		//
		for (int i = 0; i < Levels.config.enemiesOnLevel[EnemySpawn._levels].Length; i++)
		{
				
			string tempIcon="Enemy"+Levels.config.enemiesOnLevel[EnemySpawn._levels][i].ToString()+"icon";	
			Game.Prototypes.PopUp.win_pop.FindChild("Enemies").FindChild(tempIcon).GetChild(0).GetComponent<exSpriteFont>().text=enemies[Levels.config.enemiesOnLevel[EnemySpawn._levels][i]-1].ToString();
		}
		//Game.Prototypes.PopUp.win_pop.FindChild("CashLeft").GetChild(0).GetChild(0).GetComponent<exSpriteFont>().text=Currency.currency.ToString();
		int tempCurrency =(int) Currency.currency;
		CountingTextBehaviour.Attach(Game.Prototypes.PopUp.win_pop.FindChild("CashLeft").GetChild(0).GetChild(0), 0.0f, "", 0, tempCurrency, 1.5f);
		
		Game.Prototypes.PopUp.win_pop.FindChild("LivesLeft").GetChild(0).GetChild(0).GetComponent<exSpriteFont>().text="x "+HeartStack.lifes.ToString();
		tempCurrency =(int) reward;
		//Receive money
		Requisition.Recieve(tempCurrency);
		
		CountingTextBehaviour.Attach(Game.Prototypes.PopUp.win_pop.FindChild("PointsFrame/Points"), 0.0f, "", 0, tempCurrency, 1.5f);
		
		string tempRewardBonusPercent="+"+rewatdTempBonus.ToString()+"%";
		Debug.Log(tempRewardBonusPercent);
		Game.Prototypes.PopUp.win_pop.FindChild("Reward/RewardNum").GetComponent<exSpriteFont>().text=tempRewardBonusPercent;
		
		
		//lvl unlock
		
		
		MapBehaviour.SetLevelStateUnlocked(MapBehaviour.currentLevel);	
		//reset the level
		//EnemySpawn._wave=0;
		//EnemySpawn._creep=0;
		//EnemySpawn.currCreep=0;
		//SpawnHearts.spawnHeartsForWin();
		HeartStack.lifes=HeartStack.config.Maxlifes;
		//SpawnHearts.spawnHeartsForWin();
		//path.Clear();
		//Path.points.Clear();
		//Application.LoadLevel("Game");
		
		
		SiriusBehaviour.ResumeAll();
	}
	
	
	public float ConvertBonusReward(float currBonus)
	{
		if(currBonus==config.goldMedalBonus)
		{
			return 100.0f;
		}
		if(currBonus==config.silverMedalBonus)
		{
			return 50.0f;
		}
		if(currBonus==config.bronzeMedalBonus)
		{
			return 25.0f;
		}
		else
		{
			return 0.0f;	
		}
	}
	
	public float CalculateRewardBonus()
	{
		if(HeartStack.lifes == HeartStack.config.Maxlifes)
		{
			if(SiriusPrefs.I["Medal #" + EnemySpawn._levels]<3)
			{
				SiriusPrefs.I["Medal #" + EnemySpawn._levels]=3;
				Game.Prototypes.PopUp.win_pop.FindChild("Medal").SetSprite("GoldMedal");
				return config.goldMedalBonus;
			}
			else
			{
				Game.Prototypes.PopUp.win_pop.FindChild("Medal").SetSprite("GoldMedal");
				return config.defaultMedalBonus;
			}
			
		}
		if(HeartStack.lifes < HeartStack.config.Maxlifes/2)
		{
			if(SiriusPrefs.I["Medal #" + EnemySpawn._levels]<2)
			{
				SiriusPrefs.I["Medal #" + EnemySpawn._levels]=2;
				Game.Prototypes.PopUp.win_pop.FindChild("Medal").SetSprite("SilverMedal");
				return config.silverMedalBonus;
			}
			else
			{
				Game.Prototypes.PopUp.win_pop.FindChild("Medal").SetSprite("SilverMedal");
				return config.defaultMedalBonus;
			}
				
		}
		if(HeartStack.lifes < HeartStack.config.Maxlifes/4)
		{
			if(SiriusPrefs.I["Medal #" + EnemySpawn._levels]<1)
			{
				SiriusPrefs.I["Medal #" + EnemySpawn._levels]=1;
				Game.Prototypes.PopUp.win_pop.FindChild("Medal").SetSprite("BronzeMedal");
				return config.bronzeMedalBonus;
			}
			else
			{
				Game.Prototypes.PopUp.win_pop.FindChild("Medal").SetSprite("BronzeMedal");
				return config.defaultMedalBonus;
			}
				
		}
		else
		{
			if(SiriusPrefs.I["Medal #" + EnemySpawn._levels]==0)
			{
				SiriusPrefs.I["Medal #" + EnemySpawn._levels]=0;
			}
			Game.Prototypes.PopUp.win_pop.FindChild("Medal").SetSprite("DefeatMedal");
			return config.defaultMedalBonus;	
		}
	}
	
	public static void PreparePopWin()
	{	
		for (int i = 0; i < Levels.config.enemiesNotOnLevel[EnemySpawn._levels].Length; i++)
		{
				
			string tempIcon="Enemy"+Levels.config.enemiesNotOnLevel[EnemySpawn._levels][i].ToString()+"icon";	
			//Game.Prototypes.PopUp.win_pop.FindChild("Enemies").FindChild(tempIcon).Destroy();
			Destroy(Game.Prototypes.PopUp.win_pop.FindChild("Enemies").FindChild(tempIcon));
		}
	}
	public void Boom()
	{
		Destroy(gameObject);
	}
	
	public void MoveDirection(Vector3 vec)
	{
		float ang=vec.GetAngle();
	    if(ang<45.0f && ang>-45.0f)
	    {
	   		gameObject.GetComponent<exSpriteAnimation>().Play("EnemyRight");	
	    }
		else if(ang<-45.0f && ang>-135.0f)
	    {
	   		gameObject.GetComponent<exSpriteAnimation>().Play("EnemyDown");	
	    }
		else if(ang<135.0f && ang>45.0f)
	    {
	   		gameObject.GetComponent<exSpriteAnimation>().Play("EnemyUp");	
	    }
		else if(ang>135.0f || ang<-135.0f)
	    {
	   		gameObject.GetComponent<exSpriteAnimation>().Play("EnemyLeft");	
	    }
	}
	
	public void Slow(float percent,float slowTime,bool checkImunity = true)
	{
		Debug.Log("Slowed");
		if (!_slowed && (checkImuneToSlow()==false || !checkImunity))
		{
			_slowed = true;
			_slowedTime = slowTime;
			_speedModifire=percent;
			vec*=_speedModifire;
			moveDist*=_speedModifire;
		}
		else
		{
			_slowedTime = slowTime;
		}
		
	}
	
	private void Slower()
	{
		if (!_slowed )
		{
			return;
			
		}
		else
		{
			gameObject.GetComponent<exSprite>().color= new Color(0.172f,0.364f,0.686f,1.0f);
			if (_slowedTime >= 0)
			{
				_slowedTime -= Time.deltaTime;
			}
			else
			{
				moveDist/=_speedModifire;
				vec /= _speedModifire;
				_slowed = false;
				gameObject.GetComponent<exSprite>().color= new Color(1.0f,1.0f,1.0f,1.0f);
			}
		}
	}
	private bool checkImuneToSlow()
	{
		if(_curr_enemy==3)
		{
			return true;
		}
		else if(_curr_enemy==4)
		{
			return true;
		}
		else if(_curr_enemy==5)
		{
			return true;
		}
		else
		{
			return false;
		}
	}
	public void FloatingTextSpawn(float damage)
	{
		if(_damaged)
		{
			if(config._DamageTimer<0)
			{
				Vector3 pos;
				pos=gameObject.transform.position;
				pos.x=Random.Range(pos.x-15.0f , pos.x+15.0f);
				FloatingText floatie = Game.Prototypes.FloatingTexts.Damage.Clone(pos);
				floatie.SetText("-" + _textDamage.ToString());
				floatie.Speed = new Vector3(0,1.5f,0);
				config ._DamageTimer=0.5f;
				_damaged=false;
				_textDamage=0;
			}
			else
			{
				_textDamage+=damage;
				config ._DamageTimer-=Time.deltaTime;	
			}
		}
	}
}


