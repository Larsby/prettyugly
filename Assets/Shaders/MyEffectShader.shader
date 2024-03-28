Shader "Custom/MyEffectShader"
{
	Properties {
		_MainTex ("Render Input", 2D) = "white" {}
	}
	SubShader {
		 ZWrite Off ZTest LEqual 
		Pass {
			CGPROGRAM
				#pragma vertex vert_img
				#pragma fragment frag
				#include "UnityCG.cginc"
			
				sampler2D _MainTex;
			
				float4 frag(v2f_img IN) : COLOR {
					half4 c = tex2D (_MainTex, IN.uv);
					//c.a = 0.5;
					//discard;
					return c;
				}
			ENDCG
		}
	}
}