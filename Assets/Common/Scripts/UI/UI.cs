using UnityEngine;
using System.Collections.Generic;

public abstract class UI : MonoBehaviour
{
    public enum Alignment
    {
        TopCenter,
        TopLeft,
        TopRight,
        MiddleCenter,
        MiddleLeft,
        MiddleRight,
        BottomCenter,
        BottomLeft,
        BottomRight
    }

    public enum Direction
    {
        Horizontal,
        Vertical
    }

	public static List<UI> all = new List<UI>();

	void OnEnable()
	{
		all.Add(this);
	}

	void OnDisable()
	{
		all.Remove(this);
	}

	public static UIComponent Find(string path)
	{
		return(Sirius.FindComponentObject<UIComponent>(path));
	}
	
	public abstract void OnClick(UIComponent hit);
}

