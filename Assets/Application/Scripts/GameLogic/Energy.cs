using UnityEngine;
using System.Collections;

public class Energy : SiriusBehaviour {
	
	public class Config
	{
		public float maxEnergy = 100f;
		public float regenTime = 0.5f;
	}
	public static Config config = new Config();
	
	public static float currentEnergy;
	public static GameObject energyIcon;
	public static GameObject energyText;
	public static float regenTime;
	
	
	void Awake() {
		energyText = Game.Prototypes.HUD.Energy;
		energyIcon = Game.Prototypes.HUD.EnergyFrame;
		currentEnergy =  config.maxEnergy;
	}
	
	void Start()
	{
		energyText.GetComponent<exSpriteFont>().text = currentEnergy.ToString() + "/" + config.maxEnergy.ToString();
		regenTime = config.regenTime;
	}
	// Update is called once per frame
	void Update () {
		if (regenTime <= 0)
		{
			GiveEnergy(1);
			regenTime = config.regenTime;
		}
		else
		{
			regenTime -= Time.deltaTime;
		}
	}
	
	public static bool UseEnergy(float val)
	{
		if(currentEnergy - val >= 0)
		{
			currentEnergy -= val;
			UpdateIconAndFont();
			
			return true;
		}
		else
		{
			Debug.Log("You don't have enough energy to cast this spell");
			return false;
		}
		
	}
	
	public static void GiveEnergy(float val)
	{
		if (currentEnergy == config.maxEnergy)
		{
			return;
		}
		
		if(currentEnergy + val <= config.maxEnergy)
		{
			currentEnergy += val;
		}
		else
		{
			currentEnergy = config.maxEnergy;
		}
		
		UpdateIconAndFont();
	}
	
	public static void UpdateIconAndFont()
	{
		if (currentEnergy >= 80)
		{
			energyIcon.GetComponent<exSprite>().SetSprite("Batery-1");
		}
		else if (currentEnergy < 80 && currentEnergy >= 60)
		{
			energyIcon.GetComponent<exSprite>().SetSprite("Batery-2");
		}
		else if (currentEnergy < 60 && currentEnergy >= 40)
		{
			energyIcon.GetComponent<exSprite>().SetSprite("Batery-3");
		}
		else if (currentEnergy < 40 && currentEnergy >= 20)
		{
			energyIcon.GetComponent<exSprite>().SetSprite("Batery-4");
		}
		else if (currentEnergy < 20 && currentEnergy > 0)
		{
			energyIcon.GetComponent<exSprite>().SetSprite("Batery-5");
		}
		else if(currentEnergy == 0)
		{
			energyIcon.GetComponent<exSprite>().SetSprite("Batery-6");
		}
		
		energyText.GetComponent<exSpriteFont>().text = Mathf.Round(currentEnergy).ToString() + "/" + config.maxEnergy.ToString();
	}
	
	
	
	public static void FloatingTextSpawn(float val,Vector3 pos)
	{
		Game.Prototypes.FloatingTexts.Energy.Clone("+" + val.ToString(), pos);
	}
}
