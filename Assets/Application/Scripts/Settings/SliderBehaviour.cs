using UnityEngine;
using System.Collections;

public class SliderBehaviour : MonoBehaviour {

	public GameObject leftEnd;
	public GameObject rightEnd;
	public GameObject cursor;
	private bool _clicked;
	public float val;
	
	
	void Awake ()
	{
		CheckGameObjects ();
		if (SiriusPrefs.F[gameObject.GetParent().name + "value"] >= 0)
		{
			val = SiriusPrefs.F[gameObject.GetParent().name + "value"];
			Vector3 position = new Vector3();
			position.x = leftEnd.transform.position.x + (rightEnd.transform.position.x  - leftEnd.transform.position.x) * val;
			position.y = leftEnd.transform.position.y;
			position.z = cursor.transform.position.z;
			cursor.transform.position = position;
		}
		else
		{
			cursor.transform.position = new Vector3(gameObject.transform.position.x,leftEnd.transform.position.y,cursor.transform.position.z);
			Debug.Log ("Value for the gameObject not found in SiriusPrefs");
		}
		
		_clicked = false;
	}
	
	
	void Update()  
	{
		if(Input.GetMouseButtonDown(0))
		{
			Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
			RaycastHit hit;
			
			if(!Physics.Raycast(ray, out hit))
			{
				return;		
			}
			else
			{
				GameObject hitObj = hit.transform.gameObject;
				if(hitObj == cursor)
				{
					_clicked = true;
				}
				if(hitObj == gameObject)
				{
					Vector3 position = cursor.transform.position;
					if (hit.point.x > leftEnd.transform.position.x && hit.point.x < rightEnd.transform.position.x)
					{		
						position.x = hit.point.x;
						cursor.transform.position = position;
					}
					else if (hit.point.x < leftEnd.transform.position.x)
					{
						cursor.transform.position = new Vector3(leftEnd.transform.position.x,cursor.transform.position.y,cursor.transform.position.z);
					}
					else if (hit.point.x > rightEnd.transform.position.x)
					{
						cursor.transform.position = new Vector3(rightEnd.transform.position.x,cursor.transform.position.y,cursor.transform.position.z);
					}
					
					if (cursor.transform.position.x == leftEnd.transform.position.x)
					{
						val = 0;
					}
					else if (cursor.transform.position.x == rightEnd.transform.position.x)
					{
						val = 1;
					}
					else
					{
						val = (leftEnd.transform.position - cursor.transform.position).magnitude / (leftEnd.transform.position - rightEnd.transform.position).magnitude;
					}
					SiriusPrefs.F[gameObject.GetParent().name + "value"] = val;
					}
			}
		}
		if (Input.GetMouseButton(0))
		{
			Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
			RaycastHit hit;
			
			if(!Physics.Raycast(ray, out hit))
			{
				return;		
			}
			else
			{
				if (_clicked)
				{	
					Vector3 position = cursor.transform.position;
					if (hit.point.x > leftEnd.transform.position.x && hit.point.x < rightEnd.transform.position.x)
					{		
						position.x = hit.point.x;
						cursor.transform.position = position;
					}
					else if (hit.point.x < leftEnd.transform.position.x)
					{
						cursor.transform.position = new Vector3(leftEnd.transform.position.x,cursor.transform.position.y,cursor.transform.position.z);
					}
					else if (hit.point.x > rightEnd.transform.position.x)
					{
						cursor.transform.position = new Vector3(rightEnd.transform.position.x,cursor.transform.position.y,cursor.transform.position.z);
					}
				}
			}
		}
		if(Input.GetMouseButtonUp(0))
		{
			if (_clicked)
			{
				_clicked = false;
				if (cursor.transform.position.x == leftEnd.transform.position.x)
				{
					val = 0;
				}
				else if (cursor.transform.position.x == rightEnd.transform.position.x)
				{
					val = 1;
				}
				else
				{
					val = (leftEnd.transform.position - cursor.transform.position).magnitude / (leftEnd.transform.position - rightEnd.transform.position).magnitude;
				}
				SiriusPrefs.F[gameObject.GetParent().name + "value"] = val;
					
			}
		}
	}
	
	void CheckGameObjects ()
	{
		if(leftEnd == null)
		{
			leftEnd = gameObject.FindChild("LeftEnd");
			if (leftEnd == null)
			{
				Debug.Log ("No child with name \"LeftEnd\" was found. Please add it manually via editor or add gameobject with the given name");
				Destroy (this);
				return;
			}
		}
		if(rightEnd == null)
		{
			rightEnd = gameObject.FindChild("RightEnd");
			if (rightEnd == null)
			{
				Debug.Log ("No child with name \"RightEnd\" was found. Please add it manually via editor or add gameobject with the given name");
				Destroy (this);
				return;
			}
		
		}
		if(cursor == null)
		{
			cursor = gameObject.FindChild("SliderCursor");
			if (cursor == null)
			{
				Debug.Log ("No child with name \"SliderCursor\" was found. Please add it manually via editor or add gameobject with the given name");
				Destroy (this);
				return;
			}
		}
		
			Debug.Log("RightEnd and LeftEnd aren't on the same Y, I'll set the right on the same \"y\" as the left");
			rightEnd.transform.position = new Vector3(rightEnd.transform.position.x,rightEnd.GetParent().transform.position.y,rightEnd.transform.position.z);
			leftEnd.transform.position = new Vector3(leftEnd.transform.position.x,leftEnd.GetParent().transform.position.y,leftEnd.transform.position.z);
		
	}
	
	void OnDestroy()
	{
		SiriusPrefs.F[gameObject.GetParent().name + "value"] = val;
		SiriusPrefs.Save();
	}
	public void SetToZero()
	{
		val = 0;
		cursor.transform.position = new Vector3(leftEnd.transform.position.x,cursor.transform.position.y,cursor.transform.position.z);
	}
}
