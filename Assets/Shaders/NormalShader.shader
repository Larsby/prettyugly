// Upgrade NOTE: replaced '_Object2World' with 'unity_ObjectToWorld'
// Upgrade NOTE: replaced '_World2Object' with 'unity_WorldToObject'
// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

// Upgrade NOTE: replaced '_Object2World' with 'unity_ObjectToWorld'
// Upgrade NOTE: replaced '_World2Object' with 'unity_WorldToObject'
// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "Custom/NormalShader"
{
	Properties {
		_Color ( "Color Tint", Color ) = ( 1.0, 1.0, 1.0, 1.0 )
		_MainTex ("Diffuse Texture", 2D) = "white" {}
		_BumpMap ("Normal Texture", 2D) = "bump" {}
		_BumpDepth ("Bump Depth", Range( -2.0, 2.0 )) = 1
		_SpecColor( "Specular Color", Color ) = ( 1.0, 1.0, 1.0, 1.0 )
		_Shininess( "Shininess", float ) = 10
		_RimColor( "Rim Color", Color ) = ( 1.0, 1.0, 1.0, 1.0 )
		_RimPower( "Rim Power", Range( 0.1, 10.0 )) = 3.0
		_Tint ("Tint and Transparency", Color) = (1.0, 1.0, 1.0, 1.0)
		_SoftHardMix ("Unshadowed/Shadowed Mix", Range(0.0, 1.0)) = 0.0
		_AmbientOnlyMix ("Lit/Ambient Mix", Range(0.0, 1.0)) = 0.0
		_Glow ("Self Illumination", Color) = (0.0, 0.0, 0.0, 0.0)
		[MaterialToggle] PixelSnap ("Pixel snap", Float) = 0
	}
	SubShader {
		
		Pass{
			Tags { "LightMode" = "ForwardBase" 
			"Queue" = "Transparent"
			"IgnoreProjector" = "True"
			"RenderType" = "Transparent"
			"PreviewType" = "Plane"
			"CanUseSpriteAtlas" = "True"}
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			#pragma exclude_renderers flash
			
			// User Defined Variables
			uniform float4 _Color;
			uniform sampler2D _MainTex;
			uniform float4 _MainTex_ST;
			uniform sampler2D _BumpMap;
			uniform float4 _BumpMap_ST;
			uniform float _BumpDepth;
			uniform float4 _SpecColor;
			uniform float _Shininess;
			uniform float4 _RimColor;
			uniform float _RimPower;
			
			// Unity Defined Variables;
			uniform float4 _LightColor0;
			
			// Base Input Structs
			struct vertexInput {
				float4 vertex: POSITION;
				float3 normal: NORMAL;
				float4 texcoord: TEXCOORD0;
				float4 tangent: TANGENT;
			};
			struct vertexOutput {
				float4 pos: SV_POSITION;
				float4 tex: TEXCOORD0;
				float4 posWorld: TEXCOORD1;
				float3 normalWorld: TEXCOORD2;
				float3 tangentWorld: TEXCOORD3;
				float3 binormalWorld: TEXCOORD4;
			};
			
			// Vertex Function
			vertexOutput vert( vertexInput v ) {
				vertexOutput o;
				
				o.normalWorld = normalize( mul( float4( v.normal, 0.0 ), unity_WorldToObject ).xyz );		
				o.tangentWorld = normalize( mul( unity_ObjectToWorld, v.tangent ).xyz );
				o.binormalWorld = normalize( cross( o.normalWorld, o.tangentWorld ) );
						
				o.posWorld = mul( unity_ObjectToWorld, v.vertex );
				o.pos = UnityObjectToClipPos( v.vertex );
				o.tex = v.texcoord;
				
				return o;
			}
			
			// Fragment Function
			float4 frag( vertexOutput i ): COLOR {
				float3 viewDirection = normalize( _WorldSpaceCameraPos.xyz - i.posWorld.xyz );
				float3 lightDirection;
				float atten;
				
				if( _WorldSpaceLightPos0.w == 0.0 ) { // Directional Light
					atten = 1.0;
					lightDirection = normalize( _WorldSpaceLightPos0.xyz );
				} else {
					float3 fragmentToLightSource = _WorldSpaceLightPos0.xyz - i.posWorld.xyz;
					float distance = length( fragmentToLightSource );
					float atten = 1 / distance;
					lightDirection = normalize( fragmentToLightSource );
				}
				
				// Texture Maps
				float4 tex = tex2D( _MainTex, i.tex.xy * _MainTex_ST.xy + _MainTex_ST.zw );
				float4 texN = tex2D( _BumpMap, i.tex.xy * _BumpMap_ST.xy + _BumpMap_ST.zw );
				
				// unpackNormal Function
				float3 localCoords = float3(2.0 * texN.ag - float2(1.0,1.0), 0.0);
				//localCoords.z = 1.0 - 0.5 * dot( localCoords, localCoords );
				//localCoords.z = 1.0;
				localCoords.z = _BumpDepth;
				
				// Normal Transpose Matrix
				float3x3 local2WorldTranspose = float3x3(
					i.tangentWorld,
					i.binormalWorld,
					i.normalWorld
				);
				
				// Calculate Normal Direction
				float3 normalDirection = normalize( mul( localCoords, local2WorldTranspose ) );
				
				// Lighting
				float3 diffuseReflection = atten * _LightColor0.rgb * saturate( dot( normalDirection, lightDirection ) );
				float3 specularReflection = diffuseReflection * _SpecColor.rgb * pow( saturate( dot( reflect( -lightDirection, normalDirection ), viewDirection ) ), _Shininess);
				
				// Rim Lighting
				float rim = 1 - saturate( dot( viewDirection, normalDirection ) );
				float3 rimLighting = saturate( pow( rim, _RimPower ) * _RimColor.rgb * diffuseReflection);
				float3 lightFinal = diffuseReflection + specularReflection + rimLighting + UNITY_LIGHTMODEL_AMBIENT.rgb;
				

				return float4( tex.xyz * lightFinal * _Color.xyz, 1.0);
			}
			
			ENDCG
		}
		Pass{
			Tags { "LightMode" = "ForwardAdd" 
				"Queue" = "Transparent"
			"IgnoreProjector" = "True"
			"RenderType" = "Transparent"
			"PreviewType" = "Plane"
			"CanUseSpriteAtlas" = "True"}
			Blend One One
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			#pragma exclude_renderers flash
			
			// User Defined Variables
			uniform float4 _Color;
			uniform sampler2D _MainTex;
			uniform float4 _MainTex_ST;
			uniform sampler2D _BumpMap;
			uniform float4 _BumpMap_ST;
			uniform float _BumpDepth;
			uniform float4 _SpecColor;
			uniform float _Shininess;
			uniform float4 _RimColor;
			uniform float _RimPower;
			
			// Unity Defined Variables;
			uniform float4 _LightColor0;
			
			// Base Input Structs
			struct vertexInput {
				float4 vertex: POSITION;
				float3 normal: NORMAL;
				float4 texcoord: TEXCOORD0;
				float4 tangent: TANGENT;
			};
			struct vertexOutput {
				float4 pos: SV_POSITION;
				float4 tex: TEXCOORD0;
				float4 posWorld: TEXCOORD1;
				float3 normalWorld: TEXCOORD2;
				float3 tangentWorld: TEXCOORD3;
				float3 binormalWorld: TEXCOORD4;
			};
			
			// Vertex Function
			vertexOutput vert( vertexInput v ) {
				vertexOutput o;
				
				o.normalWorld = normalize( mul( float4( v.normal, 0.0 ), unity_WorldToObject ).xyz );		
				o.tangentWorld = normalize( mul( unity_ObjectToWorld, v.tangent ).xyz );
				o.binormalWorld = normalize( cross( o.normalWorld, o.tangentWorld ) );
						
				o.posWorld = mul( unity_ObjectToWorld, v.vertex );
				o.pos = UnityObjectToClipPos( v.vertex );
				o.tex = v.texcoord;
				
				return o;
			}
			
			// Fragment Function
			float4 frag( vertexOutput i ): COLOR {
				float3 viewDirection = normalize( _WorldSpaceCameraPos.xyz - i.posWorld.xyz );
				float3 lightDirection;
				float atten;
					
				if( _WorldSpaceLightPos0.w == 0.0 ) { // Directional Light
					atten = 1.0;
					lightDirection = normalize( _WorldSpaceLightPos0.xyz );
				} else {
					float3 fragmentToLightSource = _WorldSpaceLightPos0.xyz - i.posWorld.xyz;
				//	fragmentToLightSource = 30 / dot(fragmentToLightSource, _WorldSpaceLightPos0);
					float distance = length( fragmentToLightSource );
					float atten = 1 / distance;
					lightDirection = normalize( fragmentToLightSource );
				}
				
				// Texture Maps
				float4 tex = tex2D( _MainTex, i.tex.xy * _MainTex_ST.xy + _MainTex_ST.zw );
				float4 texN = tex2D( _BumpMap, i.tex.xy * _BumpMap_ST.xy + _BumpMap_ST.zw );
				
				// unpackNormal Function
				float3 localCoords = float3(2.0 * texN.ag - float2(1.0,1.0), 0.0);
				//localCoords.z = 1.0 - 0.5 * dot( localCoords, localCoords );
				//localCoords.z = 1.0;
				localCoords.z = _BumpDepth;
				
				// Normal Transpose Matrix
				float3x3 local2WorldTranspose = float3x3(
					i.tangentWorld,
					i.binormalWorld,
					i.normalWorld
				);
				
				// Calculate Normal Direction
				float3 normalDirection = normalize( mul( localCoords, local2WorldTranspose ) );
				
				// Lighting
				float3 diffuseReflection = atten * _LightColor0.rgb * saturate( dot( normalDirection, lightDirection ) );
				float3 specularReflection = diffuseReflection * _SpecColor.rgb * pow( saturate( dot( reflect( -lightDirection, normalDirection ), viewDirection ) ), _Shininess);
				
				// Rim Lighting
				float rim = 1 - saturate( dot( viewDirection, normalDirection ) );
				float3 rimLighting = saturate( pow( rim, _RimPower ) * _RimColor.rgb * diffuseReflection);
				float3 lightFinal = diffuseReflection + specularReflection + rimLighting;
				
			
				return float4( tex.xyz * lightFinal * _Color.xyz, 1.0);
			}
			
			ENDCG
		}
		Pass {

			Blend One OneMinusSrcAlpha
			Cull Off
			Lighting Off
			ZWrite Off
			
			Fog {
				Mode Off
			}
			
			CGPROGRAM
				#include "UnityCG.cginc"
				#pragma vertex VShader
				#pragma fragment FShader
				#pragma multi_compile _ PIXELSNAP_ON 
				#pragma multi_compile _ FIXEDSAMPLEPOINT_ON LINESAMPLE_ON

				float4x4 _SFProjection;
				
				sampler2D _MainTex;
				float4 _MainTex_ST;
				
				float4 _SFAmbientLight;
				sampler2D _SFLightMap;
				sampler2D _SFLightMapWithShadows;
				float _SFExposure;
				float2 _SamplePosition;

				float _SoftHardMix;
				float _AmbientOnlyMix;
				float4 _Glow;
				float4 _Tint;
				
				struct VertexInput {
					float3 position : POSITION;
					float2 texCoord : TEXCOORD0;
					float4 color : COLOR;
				};
				
				struct VertexOutput {
					float4 position : SV_POSITION;
					float2 texCoord : TEXCOORD0;
					float4 lightmapCoord : TEXCOORD1;
					float4 color : COLOR;
				};
				
				VertexOutput VShader(VertexInput v){
					float4 position = UnityObjectToClipPos(float4(v.position, 1.0));

					float3 samplePosition = v.position;
#if defined(LINESAMPLE_ON)
					samplePosition = float3(v.position.x, _SamplePosition.y, v.position.z);
#endif

#if defined(FIXEDSAMPLEPOINT_ON)
					samplePosition = float3(_SamplePosition.xy, v.position.z);
#endif

					// Unity applies some magic to the projection matrix on some platforms.
					// Since we are using the projection for texCoords, need to ensure it has no magic by passing our own projection matrix.
					// consider UnityObjectToViewPos?
					float4 lightmapCoord = mul(_SFProjection, mul(unity_ObjectToWorld, mul(UNITY_MATRIX_V, float4(samplePosition, 1.0))));

#if defined(PIXELSNAP_ON)
					position = UnityPixelSnap(position);
					lightmapCoord = UnityPixelSnap(lightmapCoord);
#endif
					
					VertexOutput o = {
						position,
						TRANSFORM_TEX(v.texCoord, _MainTex),
						lightmapCoord + lightmapCoord.w,
						v.color*_Tint,
					};

					return o;
				}

				fixed4 FShader(VertexOutput v) : SV_Target {
					fixed4 color = v.color*tex2D(_MainTex, v.texCoord);

					// lightCoords adjusted for perspective effects					
					fixed3 l0 = tex2Dproj(_SFLightMap, UNITY_PROJ_COORD(v.lightmapCoord)).rgb;
					fixed3 l1 = tex2Dproj(_SFLightMapWithShadows, UNITY_PROJ_COORD(v.lightmapCoord)).rgb;
					fixed3 light = lerp(l0, l1, _SoftHardMix);

					// "light" already has ambient applied, _SFAmbientLight is only the ambient color.
					color.rgb *= (lerp(light, _SFAmbientLight, _AmbientOnlyMix) + _Glow) *_SFExposure*color.a;
	
					return color;
				}
			ENDCG
		}		
	} 
	//FallBack "Specular"
}