using UnityEngine;
using System.Collections;

public class Tower2Behaviour : TowerBehaviour {
	
//	public new class Config : TowerBehaviour.Config
//	{
//		public float radius = 270f; 
//		public float cooldownMax = 0.1f;
//		public float price = Currency.Prices.Tower2Price;
//	}
//	
//	public static new Config config = new Config();
	
	private Dictionary<int,string> textDict;
	public GameObject target;
	public GameObject muzzle;
	public bool rotating;
	public float rosetteTime;
	public bool rosetteActive;
	
	protected void CompletionCheck()
	{
		if (currentVal >= maxVal)
		{
			Complete();
		}
		else
		{
			NotCompleated();
		}
	}
	public void Complete()
	{
		achObj.GetComponent<exSprite>().color = new Color(0.2f,0.8f,0.2f,1.0f);
		
	}
	
	public void NotCompleated()
	{
		achObj.GetComponent<exSprite>().color = new Color(0.2f,0.2f,0.2f,0.7f);
	}
	
	protected void Start(){
	Init ();
	currentVal = AchievementBehaviour.achievements[title.GetComponent<exSpriteFont>().text].currentVal;
	maxVal = AchievementBehaviour.achievements[title.GetComponent<exSpriteFont>().text].maxVal;
	int progressVal = currentVal;
	if (progressVal > maxVal ){
		progressVal = maxVal;
	}
	progress.GetComponent<exSpriteFont>().text = progressVal.ToString() + "/" + maxVal.ToString();
	CompletionCheck();
}
	
	new void Start () 
	{
		base.Start();
		cooldown = 0;
		target = null;
		muzzle = gameObject.FindChild("TowerMuzzle");
		rotating = false;
		currDamage = Game.Towers.Damage.Tower2;
		auraBonusSpeed = 1;
	}
	void Start () 
	{
		if(!textDict.ContainsKey(MapNodeBehaviour.loadedLevel))
		{
			Destroy(gameObject);
			return;
		}
		SiriusBehaviour.PauseAll();
		//...
	}

	void Update () {
		
		
		if(Input.GetMouseButtonUp(0))
		{
			if (lineCount < storyText.Count)
			{
				storyTextBox.GetComponent<exSpriteFont>().text = storyText[lineCount];
				lineCount += 1;
			}
			else
			{
				SiriusBehaviour.ResumeAll();
				TowerSpawn.popUpBlockSpawn = false;
				Destroy(gameObject);
			}
		}
		
		if (target == null)
		{
			for(int i = 0; i < Enemy.all.Count ; i++)
			{
				Vector3 temp = gameObject.transform.position  - Enemy.all[i].transform.position;
					
			{
				0,  "Bzzt, bzzt. Captain, I am your informational officer.\n" +
				"You were teleported here in order to command\n" +
				"the defense of RZ1302 weapon facility\n" +
				"Use the command interface to build towers as fast as possible!"
			},
			{
				1,  "I am sorry for the unexpected teleportations\n" +
				",but there are rebelios facilility androids\n" +
				"in every (Bzzt, bzzt) in the galaxy\n" +
				"I hope I will , bZZZT <- Connection lost ->"
			},
			{
				2,  "Found ya! (Bzzt), sorry for that. \n" +
				"This time there will be flying enemies\n" +
				"Use Anti-Air turrent (Upgrade the Machinegun tower)"
			},
			{
				3,  "It is hard to maintain the (Bzzt, bZzt).\n" +
				"I will be able to teleport you around, but\n" +
				"I will not (Bzzt) to give you intel."
			},
			{
				4, "Ohh my god, (Bzzt, bZzt).\n" +
				"Captain, just run. RUN!"
			}
				if (temp.magnitude < type.range && AirUnitAtack(Enemy.all[i],type))
				{
					target = Enemy.all[i];
				}
			}
		}
		if (target != null)
		{
				Quaternion rotation = gameObject.transform.localRotation;
				Vector3 rotor = rotation.eulerAngles;
				float targetAngle = (target.transform.position - transform.position).GetAngle() + 180;
				float objectAngle = rotor.z;
				float angleToTarget = Mathf.DeltaAngle(targetAngle, objectAngle);
			
				if (Mathf.Abs(angleToTarget) < 170f)
				{
					
					
					if (angleToTarget < 0 )
					{
						rotor.z -= 150.0f * Time.deltaTime * timeshift;
					}
					else
					{
						rotor.z += 150.0f * Time.deltaTime * timeshift;
					}
					rotation.eulerAngles = rotor;
					gameObject.transform.rotation = rotation;
					rotating = true;
					
				}
				else
				{
					rotating = false;
				}
		}		
		
		if (cooldown <= 0 && target != null && !rotating){
			Vector3 temp = gameObject.transform.position  - target.transform.position;
			currDamage = Game.Towers.Damage.Tower2 * auraBonusDamage;
			
			if (temp.magnitude < type.range)
			{
				GameObject laser = Game.Prototypes.Projectiles.Tower2shot.Clone();
				laser.transform.position = muzzle.transform.position;
				laser.AddComponent<Laser>().target = target;
				laser.GetComponent<Laser>().currDamage = currDamage;
				laser.GetComponent<Laser>().origin = gameObject;
				cooldown = type.cooldownMax * auraBonusSpeed;
			}
			else
			{
				target = null;
			}
		
			
		}
		else
		{
			cooldown -= Time.deltaTime * timeshift;
		}
		
		if(target != null && target.GetComponent<Enemy>().isDead)
		{
			target = null;
		}
		
		

	}
	
//	public override void ShowRosette()
//	{
//		if(RosetteBehaviour.instance == null)
//		{
//			rosette = Game.Prototypes.Rosette.Center.Clone();
//			rosette.AddComponent<RosetteBehaviour>().owner = gameObject;
//			
//		}
//		else
//		{
//			RosetteBehaviour.instance.DestroyRosette();
//			rosette = Game.Prototypes.Rosette.Center.Clone();
//			rosette.AddComponent<RosetteBehaviour>().owner = gameObject;
//			
//		}
//	}
	
	
	public override void Sell()
	{
		float val = Currency.Prices.Tower2Price * 0.75f;
		Currency.GiveCurrency(val);
		FloatingTextSpawn(val,gameObject);
		TowerSpawn.DestroyInPosList(gameObject.transform.position);
		Destroy (gameObject);
		
	}
	
	public static void FloatingTextSpawn(float valueSpent,GameObject pos)
	{
		FloatingText floatie = Game.Prototypes.FloatingTexts.Cash.Clone(pos.transform.position);
		floatie.SetText("+" + valueSpent.ToString());
		floatie.Speed = new Vector3(0,1.5f,0);
		
	}
	
}
