Shader "Custom/Toon"
{
	Properties
	{
		_Color("Color", Color) = (0.5, 0.65, 1, 1)
		_MainTex("Main Texture", 2D) = "white" {}	

		//////// Shadows
		[HDR]
		_AmbientColor("Ambient Color", Color) = (0.4, 0.4, 0.4, 1)

		//////// Specular Light
		[HDR]
		_SpecularColor("Specular Color", Color) = (0.9, 0.9, 0.9, 1)
		_Glossiness("Glossiness", Float) = 32

		//////// Rim Light
		_RimColor("Rim Color", Color) = (1, 1, 1, 1)
		_RimAmount("Rim Amount", Range(0, 1)) = 0.716
		_RimThreshold("Rim Threshold", Range(0, 1)) = 0.1
	}
	SubShader
	{
		Tags
		{
			"LightMode" = "ForwardBase"
			"PassFlags" = "OnlyDirectional"
		}

		Pass
		{
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			#pragma multi_compile_fwdbase
			
			#include "UnityCG.cginc"
			#include "Lighting.cginc"
			#include "AutoLight.cginc"

			struct appdata
			{
				float4 vertex : POSITION;				
				float4 uv : TEXCOORD0;

				float3 normal : NORMAL;
			};

			struct v2f
			{
				float4 pos : SV_POSITION;
				float2 uv : TEXCOORD0;

				float3 worldNormal : NORMAL;
				float3 viewDir : TEXCOORD1;

				SHADOW_COORDS(2)
			};

			sampler2D _MainTex;
			float4 _MainTex_ST;

			//////// Shadows
			float4 _AmbientColor;

			//////// Specular Light
			float _Glossiness;
			float4 _SpecularColor;

			//////// Rim Light
			float4 _RimColor;
			float _RimAmount;
			float _RimThreshold;

			v2f vert (appdata v)
			{
				v2f o;
				o.pos = UnityObjectToClipPos(v.vertex);
				o.uv = TRANSFORM_TEX(v.uv, _MainTex);
				o.worldNormal = UnityObjectToWorldNormal(v.normal);
				o.viewDir = WorldSpaceViewDir(v.vertex);
				TRANSFER_SHADOW(o)
				return o;
			}
			
			float4 _Color;

			float4 frag(v2f i) : SV_Target
			{
				float shadow = SHADOW_ATTENUATION(i);

				//////// Shadows
				// Get well defined shadow based off of surface normal and directional light direction
				float3 normal = normalize(i.worldNormal);
				float NdotL = dot(_WorldSpaceLightPos0, normal);
				// Smooth shadow
				float lightIntensity = smoothstep(0, 0.01, NdotL * shadow);

				// Use color of directional light
				float4 light = lightIntensity * _LightColor0;

				//////// Specular Light
				float3 viewDir = normalize(i.viewDir);

				// Get average vector between the light dir and the view dir
				float3 halfVector = normalize(_WorldSpaceLightPos0 + viewDir);
				float NdotH = dot(normal, halfVector);

				// Multiply NdotH by lightIntensity so the reflection is only show when the surface is lit
				float specularIntensity = pow(NdotH * lightIntensity, _Glossiness * _Glossiness);

				// Smooth reflection
				float specularIntensitySmooth = smoothstep(0.005, 0.01, specularIntensity);
				float4 specular = specularIntensitySmooth * _SpecularColor;

				//////// Rim Light
				float4 rimDot = 1 - dot(viewDir, normal);

				// Rim light brighter the further from the shadow it is
				float rimIntensity = rimDot * pow(NdotL, _RimThreshold);
				// Smooth Rim
				rimIntensity = smoothstep(_RimAmount - 0.01, _RimAmount + 0.01, rimIntensity);
				float4 rim = rimIntensity * _RimColor;

				float4 sample = tex2D(_MainTex, i.uv);


				return _Color * sample * (_AmbientColor + light + specular + rim);
			}
			ENDCG
		}

		UsePass "Legacy Shaders/VertexLit/SHADOWCASTER"
	}
}