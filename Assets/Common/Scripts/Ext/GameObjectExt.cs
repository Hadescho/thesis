using UnityEngine;

public static class GameObjectExt
{
	public static GameObject Find(string path)
	{
		Object[] arr = Resources.FindObjectsOfTypeAll(typeof(GameObject));
		
		for(int i = 0; i < arr.Length; i++)
		{
			GameObject go = (GameObject)arr[i];
			
			if(go.GetPath() == path)
			{
				return(go);	
			}
		}
		
		return(null);
	}

	public static GameObject Clone(this GameObject go)
	{
		GameObject clone = (GameObject)GameObject.Instantiate(go);
		clone.name = go.name;
		return(clone);
	}
	
	public static GameObject Clone(this GameObject go, string name)
	{
		GameObject clone = (GameObject)GameObject.Instantiate(go);
		clone.name = name;
		return(clone);
	}
	
	public static GameObject Clone(this GameObject go, Vector3 position)
	{
		GameObject clone = (GameObject)GameObject.Instantiate(go);
		clone.name = go.name;
		clone.transform.position = position;
		return(clone);
	}
	
	public static GameObject Clone(this GameObject go, string name, Vector3 position)
	{
		GameObject clone = (GameObject)GameObject.Instantiate(go);
		clone.name = name;
		clone.transform.position = position;
		return(clone);
	}
	
	public static void Destroy(this GameObject go)
	{
		Destroy(go);	
	}
	
	public static void DestroyAfter(this GameObject go, float time)
	{
		TimedLifeBehaviour.Attach(go, time);
	}
	
	public static void SetSprite(this GameObject go, string name, bool changeDefaultAnimSprite = false)
	{
		exSprite exs = go.GetComponent<exSprite>();
		exs.SetSprite(name, changeDefaultAnimSprite);
	}

    public static void SetText(this GameObject go, string text)
    {
        exSpriteFont exf = go.GetComponent<exSpriteFont>();
        exf.text = text;
    }

    public static string GetText(this GameObject go)
    {
        exSpriteFont exf = go.GetComponent<exSpriteFont>();
        return(exf.text);
    }
	
	public static string GetPath(this GameObject go)
	{
		if(go == null)
		{
			return(null);
		}

	    string path = "/" + go.name;
	    
		while(go.transform.parent != null)
	    {
	        go = go.transform.parent.gameObject;
	        path = "/" + go.name + path;
		}
		
	    return(path);
	}

	public static void SetX(this GameObject go, float x)
	{
		Vector3 position = go.transform.position;
		position.x = x;
		go.transform.position = position;
	}
	
	public static void SetY(this GameObject go, float y)
	{
		Vector3 position = go.transform.position;
		position.y = y;
		go.transform.position = position;
	}
	
	public static void SetZ(this GameObject go, float z)
	{
		Vector3 position = go.transform.position;
		position.z = z;
		go.transform.position = position;
	}

	public static void SetXY(this GameObject go, float x, float y)
	{
		Vector3 position = go.transform.position;
		position.x = x;
		position.y = y;
		go.transform.position = position;
	}
	
	public static void SetXZ(this GameObject go, float x, float z)
	{
		Vector3 position = go.transform.position;
		position.x = x;
		position.z = z;
		go.transform.position = position;
	}
	
	public static void SetYZ(this GameObject go, float y, float z)
	{
		Vector3 position = go.transform.position;
		position.y = y;
		position.z = z;
		go.transform.position = position;
	}
	
	public static void SetRotation(this GameObject go, float z)
	{
		Vector3 rotation = go.transform.rotation.eulerAngles;
		rotation.z = z;
		go.transform.rotation = Quaternion.Euler(rotation);
	}
	
	public static float GetRotation(this GameObject go)
	{
		return(go.transform.rotation.eulerAngles.z);	
	}
	
	public static void SetLocalRotation(this GameObject go, float z)
	{
		Vector3 rotation = go.transform.localRotation.eulerAngles;
		rotation.z = z;
		go.transform.localRotation = Quaternion.Euler(rotation);
	}
	
	public static float GetLocalRotation(this GameObject go)
	{
		return(go.transform.localRotation.eulerAngles.z);	
	}
	
	public static void SetLocalX(this GameObject go, float x)
	{
		Vector3 position = go.transform.localPosition;
		position.x = x;
		go.transform.localPosition = position;
	}
	
	public static float GetLocalX(this GameObject go)
	{
		return(go.transform.localPosition.x);	
	}
	
	public static void SetLocalY(this GameObject go, float y)
	{
		Vector3 position = go.transform.localPosition;
		position.y = y;
		go.transform.localPosition = position;
	}
	
	public static float GetLocalY(this GameObject go)
	{
		return(go.transform.localPosition.y);	
	}
	
	public static void SetLocalZ(this GameObject go, float z)
	{
		Vector3 position = go.transform.localPosition;
		position.z = z;
		go.transform.localPosition = position;
	}
	
	public static float GetLocalZ(this GameObject go)
	{
		return(go.transform.localPosition.z);	
	}

	public static void SetLocalXY(this GameObject go, float x, float y)
	{
		Vector3 position = go.transform.localPosition;
		position.x = x;
		position.y = y;
		go.transform.localPosition = position;
	}
	
	public static void SetLocalXZ(this GameObject go, float x, float z)
	{
		Vector3 position = go.transform.localPosition;
		position.x = x;
		position.z = z;
		go.transform.localPosition = position;
	}
	
	public static void SetLocalYZ(this GameObject go, float y, float z)
	{
		Vector3 position = go.transform.localPosition;
		position.y = y;
		position.z = z;
		go.transform.localPosition = position;
	}
	
    [System.Obsolete("Using transform scaling may damage rendering performance.")]
	public static void SetScaleX(this GameObject go, float x)
	{
		Vector3 scale = go.transform.localScale;
		scale.x = x;
		go.transform.localScale = scale;
	}
	
	public static float GetScaleX(this GameObject go)
	{
		return(go.transform.localScale.x);	
	}
	
    [System.Obsolete("Using transform scaling may damage rendering performance.")]
	public static void SetScaleY(this GameObject go, float y)
	{
		Vector3 scale = go.transform.localScale;
		scale.y = y;
		go.transform.localScale = scale;
	}
	
	public static float GetScaleY(this GameObject go)
	{
		return(go.transform.localScale.y);	
	}
	
    [System.Obsolete("Using transform scaling may damage rendering performance.")]
	public static void SetScaleZ(this GameObject go, float z)
	{
		Vector3 scale = go.transform.localScale;
		scale.z = z;
		go.transform.localScale = scale;
	}
	
	public static float GetScaleZ(this GameObject go)
	{
		return(go.transform.localScale.z);	
	}

    [System.Obsolete("Using transform scaling may damage rendering performance.")]
	public static void SetScaleXY(this GameObject go, float x, float y)
	{
		Vector3 scale = go.transform.localScale;
		scale.x = x;
		scale.y = y;
		go.transform.localScale = scale;
	}
	
    [System.Obsolete("Using transform scaling may damage rendering performance.")]
	public static void SetScaleXZ(this GameObject go, float x, float z)
	{
		Vector3 scale = go.transform.localScale;
		scale.x = x;
		scale.z = z;
		go.transform.localScale = scale;
	}
	
    [System.Obsolete("Using transform scaling may damage rendering performance.")]
	public static void SetScaleYZ(this GameObject go, float y, float z)
	{
		Vector3 scale = go.transform.localScale;
		scale.y = y;
		scale.z = z;
		go.transform.localScale = scale;
	}
	
    [System.Obsolete("Using transform scaling may damage rendering performance.")]
	public static void SetScale(this GameObject go, float x, float y, float z)
	{
		Vector3 scale = new Vector3();
		scale.x = x;
		scale.y = y;
		scale.z = z;
		go.transform.localScale = scale;
	}

    public static void SetSpriteScaleX(this GameObject go, float x)
    {
        exSprite sprite = go.GetComponent<exSprite>();
        Vector2 scale = sprite.scale;
        scale.x = x;
        sprite.scale = scale;
    }

    public static float GetSpriteScaleX(this GameObject go)
    {
        exSprite sprite = go.GetComponent<exSprite>();
        return(sprite.scale.x);
    }

    public static void SetSpriteScaleY(this GameObject go, float y)
    {
        exSprite sprite = go.GetComponent<exSprite>();
        Vector2 scale = sprite.scale;
        scale.y = y;
        sprite.scale = scale;
    }

    public static float GetSpriteScaleY(this GameObject go)
    {
        exSprite sprite = go.GetComponent<exSprite>();
        return(sprite.scale.y);
    }

    public static void SetSpriteScale(this GameObject go, float x, float y)
    {
        exSprite sprite = go.GetComponent<exSprite>();
        Vector2 scale = new Vector2();
        scale.x = x;
        scale.y = y;
        sprite.scale = scale;
    }

    public static Vector2 GetSpriteScale(this GameObject go)
    {
        exSprite sprite = go.GetComponent<exSprite>();
        return(sprite.scale);
    }
	
	public static void SetParent(this GameObject go, GameObject parent)
	{
		go.transform.parent = parent.transform;	
	}
	
	public static GameObject GetParent(this GameObject go)
	{
		Transform t = go.transform.parent;

		if(go.transform.parent == null)
		{
			return(null);
		}

		return(t.gameObject);
	}
	
	public static GameObject GetChild(this GameObject go, int ix)
	{
		Transform t = go.transform.GetChild(ix);

		if(t == null)
		{
			return (null);
		}

		return(t.gameObject);	
	}
	
	public static GameObject FindChild(this GameObject go, string name)
	{
		Transform t = go.transform.FindChild(name);

		if(t == null)
		{
			return(null);
		}

		return(t.gameObject);
	}
	
	public static int GetChildCount(this GameObject go)
	{
		return(go.transform.GetChildCount());
	}
}