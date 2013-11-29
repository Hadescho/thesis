Shader "Transition/Fade"
{
	Properties
	{
		_TimeOffset("Time Offset", Float) = 0.0
		_Speed("Speed", Float) = 1.0
		_Color("Color", Color) = (0.0, 0.0, 0.0, 1.0)
		_Reverse("Reverse", Float) = 0.0
	}

	SubShader 
	{
		Tags 
  		{
            "Queue" = "Overlay" 
        }
        
        Cull Back 
        Lighting Off 
        ZWrite On
        Blend SrcAlpha OneMinusSrcAlpha

	    Pass 
	    {
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			#include "UnityCG.cginc"
			
			float _TimeOffset;
			float _Speed;
			float4 _Color;
			float _Reverse;
			
			struct a2v
			{
				float4 position : POSITION;
			};
			
			struct v2f
			{
			    float4 position : SV_POSITION;
			};
			
			v2f vert(a2v input)
			{
			    v2f output;
			    output.position = mul(UNITY_MATRIX_MVP, input.position);
			    return(output);
			}
			
			float4 frag(v2f input) : COLOR
			{
				float alpha;
				
				alpha = abs(clamp((_Time.y - _TimeOffset) * _Speed, 0.0f, 1.0f) - (1.0f - _Reverse));

			    return(float4(_Color.xyz, alpha));
			}
			
			ENDCG
	    }
	}
} 