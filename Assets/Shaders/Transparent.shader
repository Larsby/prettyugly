// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "Custom/Transparent2" {
  Properties {
       _MainTex ("Texture Image", 2D) = "white" {}
       _CutOff("Cut off", float) = 0.1
       _FoodTex("Food Texture", 2D) = "red"{}
        _BumpMap ("Normalmap", 2D) = "bump" {}
        _BumpIntensity ("NormalMap Intensity", Range (-1, 2)) = 1
        _BumpIntensity ("NormalMap Intensity", Float) = 1
    }
    SubShader {
     	
    
       Pass {   
       	

          CGPROGRAM
  
          #pragma vertex vert  
          #pragma fragment frag 
  
                    
          uniform sampler2D _MainTex;        
          uniform float _CutOff;
  		  uniform sampler2D _FoodTex;
  
          struct vertexInput {
             float4 vertex : POSITION;
             float4 tex : TEXCOORD0;
          };
          struct vertexOutput {
             float4 pos : SV_POSITION;
             float4 tex : TEXCOORD0;
          };
  
          vertexOutput vert(vertexInput input) 
          {
             vertexOutput output;

             output.pos =  UnityObjectToClipPos(input.vertex);
  
             output.tex = input.tex;
  
             return output;
          }
  
          float4 frag(vertexOutput input) : COLOR
          {
             
             float4 color = tex2D(_MainTex, float2(input.tex.xy));
            
                return color;
          }
  
          ENDCG
       }
    }
 
}