using UnityEngine;
using System.Collections;

public class HeartStack : SiriusBehaviour
{
	
	public class Config
	{
		public int Maxlifes = 3;
		
		public float defaultMedalPenalties=1.0f;
		public float bronzeMedalPenalties=0.75f;
		public float silverMedalPenalties=0.50f;
		public float goldMedalPenalties=0.25f;
	}
	
	public static Config config = new Config();
	
	public static int lifes = config.Maxlifes;
	public static GameObject heartstack;
	public bool isDefeatShown;
	
	void Start () 
	{
		heartstack=Game.Prototypes.Hearts.HeartStack.Clone ();
		heartstack.AddComponent<exSpriteFont>().text=lifes.ToString();
		Game.Prototypes.Hearts.Heart1.SetActive(false);
		Game.Prototypes.Hearts.HeartNum.GetComponent<exSpriteFont>().text=lifes.ToString();
		isDefeatShown=false;
		
	}
	
	
	void Update () 
	{
		if(lifes==0 && !isDefeatShown)
		{
			
			SetPopDefeat(Game.Prototypes.Counters.enemie_counter);
			Game.Prototypes.PopUp.lose_pop.SetActive(true);
			isDefeatShown=true;
			if (Game.TowerToolbar.shown)
			{
				Game.TowerToolbar.towerToolbar.animation.Play("TowerHide");
			}
			if (Game.SpellToolbar.shown)
			{
				Game.SpellToolbar.spellToolbar.animation.Play("SpellHide");
			}
			//SiriusBehaviour.PauseAll();
		}
	}
	
	
	public static void TakeLife()
	{
		if(lifes>0)
		{
			lifes-=1;
			heartstack.GetComponent<exSpriteFont>().text=lifes.ToString();
			SpawnHearts.hearts[lifes].transform.GetChild(0).animation.Play("HeartAnimation");
			SpawnHearts.hearts[lifes].DestroyAfter(1.2f);
			SpawnHearts.hearts.Remove(SpawnHearts.hearts[lifes]);
			Game.Prototypes.Hearts.HeartNum.GetComponent<exSpriteFont>().text=lifes.ToString();
		}
	}
	
	public static void RemoveAllHearts()
	{
		for(int i = 0 ; i<SpawnHearts.hearts.Count ; i++)
		{
			SpawnHearts.hearts.Remove(SpawnHearts.hearts[i]);
		}
	}
	
	public void SetPopDefeat(int [] enemies)
	{
		TowerSpawn.popUpBlockSpawn=true;
		
		Game.Prototypes.PopUp.lose_pop.FindChild("Enemies").AddComponent<UIFlow>();
		Game.Prototypes.PopUp.lose_pop.FindChild("Enemies").GetComponent<UIFlow>().VerticalDistance=80.0f;
		Game.Prototypes.PopUp.lose_pop.FindChild("Enemies").GetComponent<UIFlow>().HorizontalDistance=0.0f;
		Game.Prototypes.PopUp.lose_pop.FindChild("Enemies").GetComponent<UIFlow>().Alignment=UI.Alignment.TopLeft;
		Game.Prototypes.PopUp.lose_pop.FindChild("Enemies").GetComponent<UIFlow>().Rows=4;
		Game.Prototypes.PopUp.lose_pop.FindChild("Enemies").GetComponent<UIFlow>().Columns=1;
		
		//Reward Section
		
		float reward = Stats.GetTotalCurrency() - EnemySpawn.startMoney;
		float rewardBonus=CalculatePenalty();
		float penaly = ConvertPenalty(rewardBonus);
		
		reward=reward*rewardBonus;
		Stats.AddTotalCurrency(reward);
		
		for (int i = 0; i < Levels.config.enemiesOnLevel[EnemySpawn._levels].Length; i++)
		{
				
			string tempIcon="Enemy"+Levels.config.enemiesOnLevel[EnemySpawn._levels][i].ToString()+"icon";	
			Game.Prototypes.PopUp.lose_pop.FindChild("Enemies").FindChild(tempIcon).GetChild(0).GetComponent<exSpriteFont>().text=enemies[Levels.config.enemiesOnLevel[EnemySpawn._levels][i]-1].ToString();
		}
		
		int tempCurrency =(int) Currency.currency;
		CountingTextBehaviour.Attach(Game.Prototypes.PopUp.lose_pop.FindChild("CashLeft").GetChild(0).GetChild(0), 0.0f, "", 0, tempCurrency, 1.5f);
		
		Game.Prototypes.PopUp.lose_pop.FindChild("LivesLeft").GetChild(0).GetChild(0).GetComponent<exSpriteFont>().text="x "+HeartStack.lifes.ToString();
		
		tempCurrency =(int) reward;
		CountingTextBehaviour.Attach(Game.Prototypes.PopUp.lose_pop.FindChild("PointsFrame/Points"), 0.0f, "", 0, tempCurrency, 1.5f);
		
		string tempPenalty="-"+penaly.ToString()+"%";
		Game.Prototypes.PopUp.lose_pop.FindChild("Penalty/PenaltyNum").GetComponent<exSpriteFont>().text=tempPenalty;
		Game.Prototypes.PopUp.lose_pop.FindChild("Medal").SetSprite("DefeatMedal");
		
		
		for(int i = 0 ; i<TowerSpawn.allTowers.Count; i++)
		{
			Destroy(TowerSpawn.allTowers[i]);
		}
		//reset the level
		
		EnemySpawn._wave=Levels.config.enemies[EnemySpawn._levels].Length-1;
		EnemySpawn._creep=Levels.config.enemies[EnemySpawn._levels][EnemySpawn._wave].Length;
		//EnemySpawn.currCreep=0;
		//SpawnHearts.spawnHearts();
		//lifes=config.Maxlifes;
		//Path.points.Clear();
		//Application.LoadLevel("Game");
		
		//SiriusBehaviour.ResumeAll();
		
	}
	public float ConvertPenalty(float currPenalty)
	{
		if(currPenalty==config.goldMedalPenalties)
		{
			return 75.0f;
		}
		if(currPenalty==config.silverMedalPenalties)
		{
			return 50.0f;
		}
		if(currPenalty==config.bronzeMedalPenalties)
		{
			return 25.0f;
		}
		else
		{
			return 0.0f;	
		}
	}
	
	public float CalculatePenalty()
	{
		if(SiriusPrefs.I["Medal #" + EnemySpawn._levels]==3)
		{
			return config.goldMedalPenalties;
		}
		if(SiriusPrefs.I["Medal #" + EnemySpawn._levels]==2)
		{
			return config.silverMedalPenalties;
		}
		if(SiriusPrefs.I["Medal #" + EnemySpawn._levels]==1)
		{
			return config.bronzeMedalPenalties;
		}
		else
		{
			return config.defaultMedalPenalties;
		}
	}
	
	public static void PreparePopDefeat()
	{	
		for (int i = 0; i < Levels.config.enemiesNotOnLevel[EnemySpawn._levels].Length; i++)
		{
				
			string tempIcon="Enemy"+Levels.config.enemiesNotOnLevel[EnemySpawn._levels][i].ToString()+"icon";	
			//Game.Prototypes.PopUp.win_pop.FindChild("Enemies").FindChild(tempIcon).Destroy();
			Destroy(Game.Prototypes.PopUp.lose_pop.FindChild("Enemies").FindChild(tempIcon));
		}
	}
	
}
