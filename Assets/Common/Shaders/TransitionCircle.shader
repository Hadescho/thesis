Shader "Transition/Circle"
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
			    float2 tex : TEXCOORD0;
			};
			
			v2f vert(a2v input)
			{
			    v2f output;
			    output.position = mul(UNITY_MATRIX_MVP, input.position);
			    output.tex = float2(output.position.x, output.position.y);
			    return(output);
			}
			
			float4 frag(v2f input) : COLOR
			{
				float t;
				float alpha;
				
				t = _Reverse * 2.0f - clamp((_Time.y - _TimeOffset) * _Speed, 0.0f, 2.0f);
				alpha = input.tex.x * input.tex.x + input.tex.y * input.tex.y;
				alpha /= (t * t);
				alpha *= alpha * alpha;
				
			    return(float4(_Color.xyz, alpha));
			}
			
			ENDCG
	    }
	}
} 