Shader "Custom/SnowShader" {
Properties {
		_MainTex ("Base (RGB)", 2D) = "white" {}
		 _Bump ("Bump", 2D) = "bump" {}
	
	}
	
	SubShader {
		Tags {
			"Queue" = "Transparent"
			"IgnoreProjector" = "True"
			"RenderType" = "Transparent"
			"PreviewType" = "Plane"
			"CanUseSpriteAtlas" = "True"
		}
		

			
	

        CGPROGRAM
        #pragma surface surf Lambert
 
       
       sampler2D _MainTex;
         sampler2D _Bump;
         
 
        struct Input {
            float2 uv_MainTex;
             float2 uv_Bump;
            
         };
 
         void surf (Input IN, inout SurfaceOutput o) { 
              //
              half4 c = tex2D (_MainTex, IN.uv_MainTex);
 
             //Extract the normal map information from the texture
             o.Normal = UnpackNormal(tex2D(_Bump, IN.uv_Bump));
  			o.Albedo = 1;
           //  o.Albedo = c.rgb;
          	//o.Alpha = 0;
              
 
              
         }
         ENDCG
    } 

   
}