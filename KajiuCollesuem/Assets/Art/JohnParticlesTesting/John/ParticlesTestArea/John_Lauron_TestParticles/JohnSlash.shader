// Made with Amplify Shader Editor
// Available at the Unity Asset Store - http://u3d.as/y3X 
Shader "John/Particles/Slash"
{
	Properties
	{
		_TintColor ("Tint Color", Color) = (0.5,0.5,0.5,0.5)
		_MainTex ("Particle Texture", 2D) = "white" {}
		_InvFade ("Soft Particles Factor", Range(0.01,3.0)) = 1.0
		_Color0("Color 0", Color) = (1,1,1,1)
		[Toggle]_Useblack("Use black", Float) = 0
		_Emission("Emission", Float) = 2
		_MainTEx("MainTEx", 2D) = "white" {}
	}


	Category 
	{
		SubShader
		{
			Tags { "Queue"="Transparent" "IgnoreProjector"="True" "RenderType"="Transparent" "PreviewType"="Plane" }
			Blend SrcAlpha OneMinusSrcAlpha
			ColorMask RGB
			Cull Off
			Lighting Off 
			ZWrite Off
			ZTest LEqual
			
			Pass {
			
				CGPROGRAM
				#pragma vertex vert
				#pragma fragment frag
				#pragma target 2.0
				#pragma multi_compile_particles
				#pragma multi_compile_fog
				

				#include "UnityCG.cginc"

				struct appdata_t 
				{
					float4 vertex : POSITION;
					fixed4 color : COLOR;
					float4 texcoord : TEXCOORD0;
					UNITY_VERTEX_INPUT_INSTANCE_ID
					float4 ase_texcoord1 : TEXCOORD1;
				};

				struct v2f 
				{
					float4 vertex : SV_POSITION;
					fixed4 color : COLOR;
					float4 texcoord : TEXCOORD0;
					UNITY_FOG_COORDS(1)
					#ifdef SOFTPARTICLES_ON
					float4 projPos : TEXCOORD2;
					#endif
					UNITY_VERTEX_INPUT_INSTANCE_ID
					UNITY_VERTEX_OUTPUT_STEREO
					float4 ase_texcoord3 : TEXCOORD3;
				};
				
				
				#if UNITY_VERSION >= 560
				UNITY_DECLARE_DEPTH_TEXTURE( _CameraDepthTexture );
				#else
				uniform sampler2D_float _CameraDepthTexture;
				#endif

				//Don't delete this comment
				// uniform sampler2D_float _CameraDepthTexture;

				uniform sampler2D _MainTex;
				uniform fixed4 _TintColor;
				uniform float4 _MainTex_ST;
				uniform float _InvFade;
				uniform float4 _Color0;
				uniform float _Useblack;
				uniform sampler2D _MainTEx;
				uniform float _Emission;

				v2f vert ( appdata_t v  )
				{
					v2f o;
					UNITY_SETUP_INSTANCE_ID(v);
					UNITY_INITIALIZE_VERTEX_OUTPUT_STEREO(o);
					UNITY_TRANSFER_INSTANCE_ID(v, o);
					o.ase_texcoord3 = v.ase_texcoord1;

					v.vertex.xyz +=  float3( 0, 0, 0 ) ;
					o.vertex = UnityObjectToClipPos(v.vertex);
					#ifdef SOFTPARTICLES_ON
						o.projPos = ComputeScreenPos (o.vertex);
						COMPUTE_EYEDEPTH(o.projPos.z);
					#endif
					o.color = v.color;
					o.texcoord = v.texcoord;
					UNITY_TRANSFER_FOG(o,o.vertex);
					return o;
				}

				fixed4 frag ( v2f i  ) : SV_Target
				{
					#ifdef SOFTPARTICLES_ON
						float sceneZ = LinearEyeDepth (SAMPLE_DEPTH_TEXTURE_PROJ(_CameraDepthTexture, UNITY_PROJ_COORD(i.projPos)));
						float partZ = i.projPos.z;
						float fade = saturate (_InvFade * (sceneZ-partZ));
						i.color.a *= fade;
					#endif

					float4 temp_cast_0 = (1.0).xxxx;
					float4 uv132 = i.ase_texcoord3;
					uv132.xy = i.ase_texcoord3.xy * float2( 1,1 ) + float2( 0,0 );
					float4 temp_cast_1 = (uv132.z).xxxx;
					float4 temp_cast_2 = (( uv132.z + uv132.w )).xxxx;
					float temp_output_26_0 = (2.5 + (( 1.0 + uv132.x ) - 0.0) * (1.0 - 2.5) / (1.0 - 0.0));
					float2 uv031 = i.texcoord.xy * float2( 1,1 ) + float2( 0,0 );
					float U29 = uv031.x;
					float temp_output_24_0 = (1.0 + (saturate( uv132.y ) - 0.0) * (0.0 - 1.0) / (1.0 - 0.0));
					float V30 = uv031.y;
					float2 appendResult17 = (float2(( saturate( ( ( ( temp_output_26_0 * temp_output_26_0 * temp_output_26_0 * temp_output_26_0 * temp_output_26_0 ) * U29 ) - temp_output_24_0 ) ) * ( 1.0 / (1.0 + (temp_output_24_0 - 0.0) * (0.001 - 1.0) / (1.0 - 0.0)) ) ) , V30));
					float4 smoothstepResult13 = smoothstep( temp_cast_1 , temp_cast_2 , tex2D( _MainTEx, saturate( appendResult17 ) ));
					float4 temp_output_12_0 = saturate( smoothstepResult13 );
					float4 appendResult2 = (float4((( _Color0 * lerp(temp_cast_0,temp_output_12_0,_Useblack) * _Emission * i.color )).rgb , ( _Color0.a * (temp_output_12_0).r * i.color.a )));
					

					fixed4 col = appendResult2;
					UNITY_APPLY_FOG(i.fogCoord, col);
					return col;
				}
				ENDCG 
			}
		}	
	}
	CustomEditor "ASEMaterialInspector"
	
	
}
/*ASEBEGIN
Version=16400
-1920;0;1920;1019;9612.066;3733.62;6.461504;True;True
Node;AmplifyShaderEditor.TextureCoordinatesNode;32;-4865.89,378.2302;Float;False;1;-1;4;3;2;SAMPLER2D;;False;0;FLOAT2;1,1;False;1;FLOAT2;0,0;False;5;FLOAT4;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleAddOpNode;27;-4541.732,-2.690399;Float;False;2;2;0;FLOAT;1;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.TextureCoordinatesNode;31;-4511.763,336.6747;Float;False;0;-1;2;3;2;SAMPLER2D;;False;0;FLOAT2;1,1;False;1;FLOAT2;0,0;False;5;FLOAT2;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.TFHCRemapNode;26;-4330.732,-17.6904;Float;False;5;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;1;False;3;FLOAT;2.5;False;4;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.SaturateNode;28;-4527.609,231.0879;Float;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;25;-4075.732,-17.6904;Float;False;5;5;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;3;FLOAT;0;False;4;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RegisterLocalVarNode;29;-4123.107,227.0936;Float;False;U;-1;True;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.TFHCRemapNode;24;-3854.153,121.0289;Float;False;5;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;1;False;3;FLOAT;1;False;4;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;23;-3832.153,-17.97113;Float;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleSubtractOpNode;21;-3570.153,-22.97113;Float;False;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.TFHCRemapNode;22;-3599.153,89.22919;Float;False;5;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;1;False;3;FLOAT;1;False;4;FLOAT;0.001;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleDivideOpNode;20;-3351.153,81.02887;Float;False;2;0;FLOAT;1;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SaturateNode;19;-3366.153,-22.97113;Float;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;18;-3109.717,-23.19336;Float;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RegisterLocalVarNode;30;-4124.683,305.6416;Float;False;V;-1;True;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.DynamicAppendNode;17;-2928.438,93.05399;Float;False;FLOAT2;4;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;3;FLOAT;0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.SaturateNode;16;-2682.264,93.03383;Float;False;1;0;FLOAT2;0,0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.SimpleAddOpNode;15;-2042.697,211.1212;Float;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SamplerNode;14;-2191.697,-79.87885;Float;True;Property;_MainTEx;MainTEx;3;0;Create;True;0;0;False;0;None;None;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SmoothstepOpNode;13;-1817.697,91.12115;Float;False;3;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;2;COLOR;1,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.SaturateNode;12;-1559.015,143.946;Float;False;1;0;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.RangedFloatNode;11;-1503.73,-62.20142;Float;False;Constant;_Float2;Float 2;3;0;Create;True;0;0;False;0;1;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.VertexColorNode;10;-1280.56,282.1514;Float;False;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.RangedFloatNode;8;-1251.833,101.2595;Float;False;Property;_Emission;Emission;2;0;Create;True;0;0;False;0;2;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.ToggleSwitchNode;7;-1303.07,-7.489665;Float;False;Property;_Useblack;Use black;1;0;Create;True;0;0;False;0;0;2;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.ColorNode;6;-1308.299,-201.9834;Float;False;Property;_Color0;Color 0;0;0;Create;True;0;0;False;0;1,1,1,1;0,0,0,0;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.ComponentMaskNode;9;-1314.56,193.1514;Float;False;True;False;False;False;1;0;COLOR;0,0,0,0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;5;-963.4611,-22.60342;Float;False;4;4;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;2;FLOAT;0;False;3;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.ComponentMaskNode;3;-432.3027,-43.05058;Float;False;True;True;True;False;1;0;COLOR;0,0,0,0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;4;-962.1151,257.2944;Float;False;3;3;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;34;-4940.883,124.2294;Float;False;Property;_LenghtSet1ifyouuseinPS;Lenght(Set 1 if you use in PS);5;0;Create;True;0;0;False;0;1;0;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.DynamicAppendNode;2;-192.9885,146.5188;Float;False;FLOAT4;4;0;FLOAT3;0,0,0;False;1;FLOAT;0;False;2;FLOAT;0;False;3;FLOAT;0;False;1;FLOAT4;0
Node;AmplifyShaderEditor.RangedFloatNode;33;-4935.383,-5.03941;Float;False;Property;_PathSet0ifyouuseinPS;Path(Set 0 if you use in PS);4;0;Create;True;0;0;False;0;0;0;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.TemplateMultiPassMasterNode;0;0,0;Float;False;True;2;Float;ASEMaterialInspector;0;7;John/Particles/Slash;0b6a9f8b4f707c74ca64c0be8e590de0;True;SubShader 0 Pass 0;0;0;SubShader 0 Pass 0;2;True;2;5;False;-1;10;False;-1;0;1;False;-1;0;False;-1;False;False;True;2;False;-1;True;True;True;True;False;0;False;-1;False;True;2;False;-1;True;3;False;-1;False;True;4;Queue=Transparent=Queue=0;IgnoreProjector=True;RenderType=Transparent=RenderType;PreviewType=Plane;False;0;False;False;False;False;False;False;False;False;False;False;True;0;0;;0;0;Standard;0;0;1;True;False;2;0;FLOAT4;0,0,0,0;False;1;FLOAT3;0,0,0;False;0
WireConnection;27;1;32;1
WireConnection;26;0;27;0
WireConnection;28;0;32;2
WireConnection;25;0;26;0
WireConnection;25;1;26;0
WireConnection;25;2;26;0
WireConnection;25;3;26;0
WireConnection;25;4;26;0
WireConnection;29;0;31;1
WireConnection;24;0;28;0
WireConnection;23;0;25;0
WireConnection;23;1;29;0
WireConnection;21;0;23;0
WireConnection;21;1;24;0
WireConnection;22;0;24;0
WireConnection;20;1;22;0
WireConnection;19;0;21;0
WireConnection;18;0;19;0
WireConnection;18;1;20;0
WireConnection;30;0;31;2
WireConnection;17;0;18;0
WireConnection;17;1;30;0
WireConnection;16;0;17;0
WireConnection;15;0;32;3
WireConnection;15;1;32;4
WireConnection;14;1;16;0
WireConnection;13;0;14;0
WireConnection;13;1;32;3
WireConnection;13;2;15;0
WireConnection;12;0;13;0
WireConnection;7;0;11;0
WireConnection;7;1;12;0
WireConnection;9;0;12;0
WireConnection;5;0;6;0
WireConnection;5;1;7;0
WireConnection;5;2;8;0
WireConnection;5;3;10;0
WireConnection;3;0;5;0
WireConnection;4;0;6;4
WireConnection;4;1;9;0
WireConnection;4;2;10;4
WireConnection;2;0;3;0
WireConnection;2;3;4;0
WireConnection;0;0;2;0
ASEEND*/
//CHKSM=711A024918B4E5F983D480466CDB281501BA0F82