using UnityEngine;

public class PhysicsExt
{
	public static GameObject Raycast()
	{
		Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
		RaycastHit hit;
		
		ray.origin -= new Vector3(0.0f, 0.0f, 0.1f);
		
		if(!Physics.Raycast(ray, out hit))
		{
			return(null);
		}
		
		return(hit.transform.gameObject);
	}
	
	public static Vector3? RaycastPoint()
	{
		Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
		RaycastHit hit;
		
		ray.origin -= new Vector3(0.0f, 0.0f, 0.1f);
		
		if(!Physics.Raycast(ray, out hit))
		{
			return(null);
		}
		
		return(hit.point);
	}
}