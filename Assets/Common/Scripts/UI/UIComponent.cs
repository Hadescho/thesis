using UnityEngine;
using System.Collections;

[RequireComponent(typeof(BoxCollider))]
public abstract class UIComponent : MonoBehaviour
{
	public abstract void OnPress();
	public abstract void OnRelease();
	public abstract void OnClick();
}

