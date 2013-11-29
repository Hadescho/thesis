using UnityEngine;
using System.Collections.Generic;

public static class ListExt
{
	public static T GetRandom<T>(this List<T> list)
	{
		return(list[UnityEngine.Random.Range(0, list.Count)]);
	}
	
	public static T Last<T>(this List<T> list)
	{
		return(list[list.Count - 1]);	
	}
	
	public static void RemoveLast<T>(this List<T> list)
	{
		list.RemoveAt(list.Count - 1);	
	}
}

