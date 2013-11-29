using UnityEngine;
using System.Collections;

public class HorizontalDrag : MonoBehaviour {
	
	Vector3 prevPoint;
	Vector3 newPoint;
	public float minX;
	public float maxX;
	
	void Awake ()
	{
	}
	
	
	void Update ()
	{
		if (Input.GetMouseButtonDown(0))
		{
			Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
			RaycastHit hit;
			if(!Physics.Raycast(ray, out hit))
			{
				return;
			}
			if(hit.transform.gameObject.name == "Backgrounds" || hit.transform.gameObject.name.Contains("Level "))
			{
				prevPoint = hit.point;
			}
		}
		if(Input.GetMouseButton(0))
		{
			Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
			RaycastHit hit;
			if(!Physics.Raycast(ray, out hit))
			{
				return;
			}
			if(hit.transform.gameObject.name == "Backgrounds" || hit.transform.gameObject.name.Contains("Level "))
			{
					Vector3 position = new Vector3 (transform.position.x - (prevPoint.x - hit.point.x)/*Time.deltaTime*/,transform.position.y,transform.position.z);
					if (position.x > minX)
					{
						position.x = minX;
					}
					if (position.x < maxX)
					{
						position.x = maxX;
					}
					gameObject.transform.position = position;
				}
				prevPoint = hit.point;
				
		}
		if(Input.GetMouseButtonUp(0))
		{
			prevPoint = new Vector3();
		}
	
	}
}
