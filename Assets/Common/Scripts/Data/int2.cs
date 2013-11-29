using System;
using System.Runtime;
using System.Runtime.InteropServices;

[StructLayout(LayoutKind.Explicit)]
struct int2
{
	public int2(int x)
	{
		this.x = x;
		this.y = x;
		min = x;
		max = x;
		a = x;
		b = x;
	}
	
	public int2(int x, int y)
	{
		this.x = x;
		this.y = y;
		min = x;
		max = y;
		a = x;
		b = y;
	}
	
	[FieldOffset(0)]
	public int x;
	
	[FieldOffset(4)]
	public int y;
	
	[FieldOffset(0)]
	public int min;
	
	[FieldOffset(4)]
	public int max;
	
	[FieldOffset(0)]
	public int a;
	
	[FieldOffset(4)]
	public int b;
};