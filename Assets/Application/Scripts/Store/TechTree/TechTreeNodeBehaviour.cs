using UnityEngine;
using System.Collections;

public class TechTreeNodeBehaviour : MonoBehaviour
{
	
	public class Config
	{
		
	}
	
	private GameObject _requirement;
	//private GameObject _text;
	public int cost;
	public string description;
	public bool unlocked;
	public bool unlockable;
	
	
	void Awake()
	{
		_requirement = gameObject.GetParent();
		//_text = gameObject.FindChild("Text");
		unlocked = SiriusPrefs.B[gameObject.name + "unlocked"];
	
	}
	void Start()
	{
		unlockable = false;
		gameObject.FindChild("Text").GetComponent<exSpriteFont>().text = gameObject.name + description;
		//-------------
		cost = TowerBehaviour.config.towers[gameObject.name].requisitionPrice;
		Action();
		//-------------
		
		
		
	}

	void Action ()
	{
		if (_requirement.GetComponent<TechTreeNodeBehaviour>() != null && _requirement.GetComponent<TechTreeNodeBehaviour>().unlocked)
		{
			unlockable = true;
		}
		
		if (!unlockable && !unlocked)
		{
			gameObject.GetComponent<exSprite>().color = new Color(1.0f,0.3f,0.3f,1.0f);
		
		}
		if (unlockable && !unlocked)
		{
			if(Requisition.GetRequisitionVal() < cost)
			{
				gameObject.GetComponent<exSprite>().color = new Color(1.0f,0.3f,0.3f,1.0f);
				gameObject.collider.enabled = false;
			}
			else
			{
				gameObject.GetComponent<exSprite>().color = new Color(0.3f,1.0f,0.3f,1.0f);
				gameObject.collider.enabled = true;
			}
			
		}
		if (unlocked)
		{
			gameObject.GetComponent<exSprite>().color = new Color(0.3f,0.3f,1.0f,1.0f);
		}
	}
	
	void Update()
	{
		Invoke("Action",0.1f);
		
	}
	
	public bool Purchase()
	{
		
		if (!unlocked && unlockable && Requisition.Spend(cost))
		{
			Debug.Log("Vliza v true na purchase");
			unlocked = true;
			SiriusPrefs.B[gameObject.name + "unlocked"] = true;
			unlockable = false;
			gameObject.GetComponent<exSprite>().color = new Color (0.0f,1.0f,0.0f,1.0f);
			SiriusPrefs.Save();
			return true;
		}
		return false;
			
	}
	
}
