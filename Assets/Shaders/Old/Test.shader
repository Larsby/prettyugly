Shader "Custom/Test" {
	   Properties 
     {
         _Color ("Main Color", Color) = (1,1,1,1)
         _MainTex ("Base (RGB) Trans (A)", 2D) = "white" {} 
         _BlendTex ("Blend (RGB)", 2D) = "white"
     }
     SubShader 
     {
         Tags { "Queue"="Geometry-9" "IgnoreProjector"="True" "RenderType"="Transparent" }
         Lighting Off
         ZTest GEqual
         LOD 200
         //Blend SrcAlpha OneMinusSrcAlpha
         
         CGPROGRAM
         #pragma surface surf Lambert
         // add "alpha" to pragma to turn on alpha
         
         sampler2D _MainTex;
         fixed4 _Color;
         
         struct Input {
             float2 uv_MainTex;
         };
         
         void surf (Input IN, inout SurfaceOutput o) {
             fixed4 c = tex2D(_MainTex, IN.uv_MainTex) * _Color;
             o.Albedo = c.rgb;
             o.Alpha = c.a;
         }
         ENDCG
         
         Pass {
             SetTexture [_MainTex] 
             SetTexture [_BlendTex] {
                 constantColor (0, 0, 0, 0.9) // last value is blend opacity
                 combine texture lerp (constant) previous
             }
         }
     }
     
     Fallback "Transparent/VertexLit"
 }
