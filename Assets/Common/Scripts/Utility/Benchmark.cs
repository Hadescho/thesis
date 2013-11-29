using UnityEngine;
using System.Diagnostics;
using System.Collections;

public static class Benchmark
{
	static string _title;
	static Stopwatch _watch = new Stopwatch();
	
	public static void Begin(string title)
	{
		_title = title;
		_watch.Start();
	}
	
	public static void End()
	{
		_watch.Stop();
		UnityEngine.Debug.Log(_title + ": " + _watch.ElapsedMilliseconds);
		_watch.Reset();
	}
}

