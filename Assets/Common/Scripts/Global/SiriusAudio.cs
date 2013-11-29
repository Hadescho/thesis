using UnityEngine;
using System.Collections.Generic;

public class SiriusAudio : SiriusBehaviour
{
    public static SiriusAudio instance;

    public string SettingSound = "Setting Sound";
    public string SettingMusic = "Setting Music";
    public List<AudioClip> Clips = new List<AudioClip>();

    void Awake()
    {
        if(instance != null)
        {
            Debug.LogError("More than one SiriusAudio instances are not allowed.");
            Destroy(this);
            return;
        }

        instance = this;
    }

	public static void Play(AudioClip clip)
	{
		if(clip == null || !SiriusPrefs.B[instance.SettingSound])
		{
			return;
		}

		AudioSource.PlayClipAtPoint(clip, Vector3.zero);
	}

    public static void Play(string name)
    {
        for(int i = 0; i < instance.Clips.Count; i++)
        {
            if(instance.Clips[i].name == name)
            {
                Play(instance.Clips[i]);
                return;
            }
        }

        Debug.LogError("No audio clip named '" + name + "' was found.");
    }

	public static GameObject PlayLoop(AudioClip clip)
	{
		if(clip == null || !SiriusPrefs.B[instance.SettingSound])
		{
			return(null);
		}

		GameObject go = new GameObject("Loop audio");
		AudioSource audio = go.AddComponent<AudioSource>();
		audio.clip = clip;
		audio.loop = true;
		audio.playOnAwake = true;
		audio.Play();
		return(go);
	}

	public static GameObject PlayLoop(string name)
	{
		for(int i = 0; i < instance.Clips.Count; i++)
		{
			if(instance.Clips[i].name == name)
			{
				return(PlayLoop(instance.Clips[i]));
			}
		}

		Debug.LogError("No audio clip named '" + name + "' was found.");
		return(null);
	}

	public static GameObject PlayMusic(AudioClip clip)
	{
		if(clip == null || !SiriusPrefs.B[instance.SettingMusic])
		{
			return (null);
		}

		GameObject go = new GameObject("Loop audio");
		AudioSource audio = go.AddComponent<AudioSource>();
		audio.clip = clip;
		audio.loop = true;
		audio.playOnAwake = true;
		audio.Play();
		return (go);
	}

	public static GameObject PlayMusic(string name)
	{
		for(int i = 0; i < instance.Clips.Count; i++)
		{
			if(instance.Clips[i].name == name)
			{
				return(PlayMusic(instance.Clips[i]));
			}
		}

		Debug.LogError("No audio clip named '" + name + "' was found.");
		return (null);
	}
}