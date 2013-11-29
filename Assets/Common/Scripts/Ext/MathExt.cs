using UnityEngine;

class MathExt
{
	public static Vector3 CubicInterpolate(Vector3 p0, Vector3 p1, Vector3 p2, Vector3 p3, float t) 
	{
		Vector3 result = p1 + 0.5f * t * (p2 - p0 + t * (2.0f * p0 - 5.0f * p1 + 4.0f * p2 - p3
							+ t * (3.0f * (p1 - p2) + p3 - p0)));
		return(result);
	}
	
	public static bool Line2CircleIntersect(float x1, float y1, float x2, float y2, float cx, float cy, float cr)
	{
		float slope;
	    float yoff;
	    float m;
	    float a, b, c;
	    float D;
	    
	    if(x1 == x2)
		{
	        x2++;
		}
	    
	    slope = (y2 - y1) / (x2 - x1);
	    
	    yoff = y1 - slope * x1;
	    m = yoff - cy;
	    
	    a = slope * slope + 1;
	    b = 2 * (slope * m - cx);
	    c = m * m - cr * cr + cx * cx;
	    
	    D = b * b - 4 * a * c;
	    
	    if(D < 0)
	    {
	        return(false);
	    }
		
		float maxx;
		float minx;
        
        if(x1 > x2)
        {
            maxx = x1;
            minx = x2;
        }
        else
        {
            maxx = x2;
            minx = x1;
        }
		
        float intersectX1 = (-b + Mathf.Sqrt(D)) / (2 * a);
        //float intersectY1 = slope * intersectX1 + yoff;
        
		if(intersectX1 > minx && intersectX1 < maxx)
		{
            return(true);
		}
		
        float intersectX2 = (-b - Mathf.Sqrt(D)) / (2 * a);
        //float intersectY2 = slope * intersectX2 + yoff;
     
        if(intersectX2 > minx && intersectX2 < maxx)
		{
           return(true);
		}
        
        return(false);
	}
};

