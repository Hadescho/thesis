using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Path : SiriusBehaviour 
{
	public static List<List<Vector3>> points = new List<List<Vector3>>();
	void Awake()
	{
		points.Add (new List<Vector3>());
		points.Add (new List<Vector3>());
		
		
	}
	void Start () 
	{
		for(int i = 1; true; i++)
		{
			GameObject point = GameObject.Find("/Path/PathPoint" + i);
			
			if(point == null)
			{
				break;	
			}
			
			points[0].Add(point.transform.position );
			Destroy(point);
		}
		for(int i = 1; true; i++)
		{
			GameObject point = GameObject.Find("/Path/PathPointTwo" + i);
			
			if(point == null)
			{
				break;	
			}
			
			points[1].Add(point.transform.position );
			Destroy(point);
		}
	}
	void Update () 
	{
	
	}
	
	public static void SpawnPathPoints()
	{
		points.Clear();
		points = new List<List<Vector3>>();
		points.Add (new List<Vector3>());
		points.Add (new List<Vector3>());
		for(int i = 1; true; i++)
		{
			GameObject point = GameObject.Find("/Path/PathPoint" + i);
			
			if(point == null)
			{
				break;	
			}
			
			points[0].Add(point.transform.position );
			Destroy(point);
		}
		for(int i = 1; true; i++)
		{
			GameObject point = GameObject.Find("/Path/PathPointTwo" + i);
			
			if(point == null)
			{
				break;	
			}
			
			points[1].Add(point.transform.position );
			Destroy(point);
		}
	}
	
}
