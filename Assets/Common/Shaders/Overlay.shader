Shader "@ Sirius/Overlay"
{
	Properties
	{
		_Color("Color", Color) = (0.0, 0.0, 0.0, 1.0)
	}

	SubShader 
	{
		Tags 
  		{
            "Queue" = "Transparent" 
        }
        
        Cull Back 
        Lighting Off 
        ZWrite On
        ZTest LEqual
        Blend SrcAlpha OneMinusSrcAlpha

	    Pass 
	    {
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			#include "UnityCG.cginc"
			
			float4 _Color;
			
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
			    return(_Color);
			}
			
			ENDCG
	    }
	}
} 