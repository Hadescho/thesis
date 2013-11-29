using System;
using UnityEngine;

class HexColor
{
	private int _code;
	
	public HexColor(int code)
	{
		_code = code;
	}
	
	public static implicit operator Color(HexColor hex)
	{
		int r = (hex._code & 0xFF0000) >> 16;
		int g = (hex._code & 0x00FF00) >>  8;
		int b = (hex._code & 0x0000FF) >>  0;
		Color color = new Color(r / 255.0f, g / 255.0f, b / 255.0f);
		
		return(color);	
	}
}
