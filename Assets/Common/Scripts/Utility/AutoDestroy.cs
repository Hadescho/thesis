using System.Collections.Generic;
using UnityEngine;

class AutoDestroy : MonoBehaviour
{
	void Awake()
	{
		Destroy(this);
	}
}