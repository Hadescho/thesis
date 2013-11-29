using System;
using System.Collections.Generic;
using UnityEngine;

class RectExt
{
    public static Rect Intersect(Rect a, Rect b)
    {
        Rect r = new Rect();
        r.x = Mathf.Max(a.x, b.x);
        r.y = Mathf.Max(a.y, b.y);
        r.xMax = Mathf.Min(a.xMax, b.xMax);
        r.yMax = Mathf.Min(a.yMax, b.yMax);
        return(r);
    }
}