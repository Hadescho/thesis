using UnityEngine;
using System.Collections;

public class SiriusCamera : MonoBehaviour 
{
	public static SiriusCamera instance;

    public enum Align
    {
        Top,
        Middle,
        Bottom
    }

    public float Width = 1024.0f;
    public float Height = 640.0f;
    public Align Alignment = Align.Middle;

	void Awake()
	{
		if(instance == null)
		{
			instance = this;
			DontDestroyOnLoad(gameObject);
			return;
		}
		
		Destroy(gameObject);
		
		object[] obj = GameObject.FindSceneObjectsOfType(typeof(GameObject));
		
		foreach(object o in obj)
		{
			GameObject go = (GameObject)o;
			exSprite sprite = go.GetComponent<exSprite>();
			
			if(sprite != null)
			{
				sprite.renderCamera = Camera.main;
			}
			
			exSpriteFont font = go.GetComponent<exSpriteFont>();
			
			if(font != null)
			{
				font.renderCamera = Camera.main;
			}
			
			exClipping clip = go.GetComponent<exClipping>();
			
			if(clip != null)
			{
				clip.renderCamera = Camera.main;
			}
		}
	}
	
	void Start()
	{
        float size = Height / 2.0f;

		if(Screen.width > Screen.height)
		{
            float sizeOld = size;

			float rw = Screen.width / Width;
			float rh = Screen.height / Height;
			
			if(rw > rh)
			{
				size /= ((float)Screen.width / (float)Screen.height) / (Width / Height);
			}
	
            Vector3 delta = new Vector3(0.0f, size - sizeOld, 0.0f);

            if(Alignment == Align.Top)
            {
                camera.transform.position -= delta;
            }
            else if(Alignment == Align.Bottom)
            {
                camera.transform.position += delta;
            }
		}

        camera.orthographicSize = size;
	}
}