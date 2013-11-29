using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

static class DateTimeExt
{
	public static DateTime Epoch = new DateTime(1970, 1, 1, 0, 0, 0, 0).ToLocalTime();

	public static DateTime FromUnixTime(int time)
	{
		DateTime date = Epoch.AddSeconds(time);
		return(date);
	}

	public static int ToUnixTime(this DateTime obj)
	{
		TimeSpan span = (obj - Epoch);
		return((int)span.TotalSeconds);
	}
}