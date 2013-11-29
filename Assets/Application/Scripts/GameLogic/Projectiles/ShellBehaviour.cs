using UnityEngine;
using System.Collections;

public class ShellBehaviour : RocketBehaviour {
	
	
	protected bool positionCalculated;
	protected Vector3 temp;
	protected GameObject tempObj;
	
	void Start()
	{
		positionCalculated = false;
	}
	
	void Update()
	{
		if (!positionCalculated && target != null)
		{
			temp = (target.transform.position - gameObject.transform.position).normalized;
			tempObj = new GameObject();
			tempObj.transform.position = target.transform.position;
			positionCalculated = true;
		}
		if (positionCalculated)
		{
			MoveProj();
		}
	}
	
	protected new void MoveProj()
	{	
		
		if (!Game.InRange(gameObject,tempObj,config.hitCircleRadius))
		{
				gameObject.transform.position += temp * Time.deltaTime * config.projectileSpeed * 1.4f;
				
				Quaternion rotation = gameObject.transform.rotation;
				Vector3 rotor = rotation.eulerAngles;
				rotor.z = temp.GetAngle();
				rotation.eulerAngles = rotor;
				gameObject.transform.rotation = rotation;
		}
		
		else
		{
			Explode();
			Destroy(tempObj);
			Destroy(gameObject);
		}
	}	
}
