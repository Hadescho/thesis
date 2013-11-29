using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SpawnHearts : MonoBehaviour
{
	public static List<GameObject> hearts=new List<GameObject>();
	public static GameObject heart ;
	public static  GameObject clone_heart ;
	public static  Vector3 vec;
	
	void Awake()
	{
		hearts = new List<GameObject>();
		Game.Prototypes.Hearts.Heart1 = GameObject.Find ("/Hud/HeartStack/Heart1");
		heart = Game.Prototypes.Hearts.Heart1.Clone();
		vec=Game.Prototypes.Hearts.Heart1.transform.position;
		vec.z=-7.0f;
		vec.y-=20.0f;
		vec.x-=8.0f;
		for(int i = 0; i<HeartStack.lifes; i++)
		{
			clone_heart=heart.Clone();			
			hearts.Add(clone_heart);
			clone_heart.transform.position=vec;
			vec.x+=5.0f;
		}
		Destroy(heart);
	}
	
	void Start () 
	{
	
	}
	

	void Update ()
	{
	
	}
	

	public static void spawnHearts()
	{
		heart = Game.Prototypes.Hearts.Heart1.Clone();
		vec=Game.Prototypes.Hearts.Heart1.transform.position;
		for(int i = 0; i<HeartStack.lifes; i++)
		{
			clone_heart=heart.Clone();			
			hearts.Add(clone_heart);
			clone_heart.transform.position=vec;
			vec.x+=5.0f;
		}
		Destroy(heart);
	}
	public static void spawnHeartsForWin()
	{
		Debug.Log("spawnHeartsForWin");
		hearts=new List<GameObject>();
		spawnHearts();
	}
}
