using UnityEngine;
using System.Collections;

public class SettingsBehaviour : MonoBehaviour {
	
	GameObject MusicIcon;
	GameObject SoundIcon;
	GameObject MusicSlider;
	GameObject SoundSlider;
	bool musicMuted;
	bool soundMuted;
	
	void Awake()
	{
		
	}
	
	void Start () 
	{
		MusicIcon = GameObject.Find("/Settings/Music/MusicIcon");
		MusicSlider = GameObject.Find ("/Settings/Music/Slider");
		SoundIcon = GameObject.Find ("Settings/Sound/SoundIcon");
		SoundSlider = GameObject.Find ("/Settings/Sound/Slider");
	}
	

	void Update () 
	{
		if(Input.GetMouseButtonUp(0))
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
				
				switch (hitObj.name)
				{
				case "MenuButton":
					Main.Load("Menu");
					break;
				case "MusicIcon":
					MusicSlider.GetComponent<SliderBehaviour>().SetToZero();			
					break;
				case "SoundIcon":
					SoundSlider.GetComponent<SliderBehaviour>().SetToZero();
					break;
				}
			}
		}
		
		if (MusicSlider.GetComponent<SliderBehaviour>().val == 0)
		{
			MusicIcon.GetComponent<exSprite>().color = new Color(1,0.3f,0.3f);
		}
		else
		{
			MusicIcon.GetComponent<exSprite>().color = new Color(1,1,1);
		}
		
		if (SoundSlider.GetComponent<SliderBehaviour>().val == 0)
		{
			SoundIcon.GetComponent<exSprite>().color = new Color(1,0.3f,0.3f);
		}
		else
		{
			SoundIcon.GetComponent<exSprite>().color = new Color(1,1,1);
		}
		
		if(Input.GetKeyDown(KeyCode.Escape))
		{
			Main.Load("Menu");
		}
	}
}
