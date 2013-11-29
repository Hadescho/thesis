using UnityEngine;
using System.Collections;

public class HellfireShellBehaviour : ShellBehaviour {

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
	
	public override void Explode()
	{
		base.Explode();
		HellfireArea.Spawn(gameObject.transform.position);
		
	}
}
