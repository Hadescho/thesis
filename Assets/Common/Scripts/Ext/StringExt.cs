using UnityEngine;

public static class StringExt
{
	public static string Variation(this string str, int v)
	{
		return(str + "_" + v.ToString().PadLeft(4, '0'));
	}
	
//	public static Stream ToStream(this string str)
//	{
//	    MemoryStream stream = new MemoryStream();
//	    StreamWriter writer = new StreamWriter(stream);
//		
//	    writer.Write(str);
//	    writer.Flush();
//	    stream.Position = 0;
//		
//	    return(stream);
//	}
};