using System;
using UnityEngine;

public static class RigidbodyExt
{
	public static void AddForceToAngle(this Rigidbody rbody, float angle, float force)
	{
		float x = Mathf.Cos(angle) * force;
		float y = Mathf.Sin(angle) * force;

		rbody.AddForce(new Vector3(x, y, 0.0f));
	}
}