using UnityEngine;

public static class VectorExt
{
	public static float GetAngle(this Vector3 vec)
	{
		return(Mathf.Atan2(vec.y, vec.x) * 180.0f / Mathf.PI);	
	}
};