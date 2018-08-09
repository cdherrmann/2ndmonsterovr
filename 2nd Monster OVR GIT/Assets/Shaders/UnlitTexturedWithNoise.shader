// Made with Amplify Shader Editor
// Available at the Unity Asset Store - http://u3d.as/y3X 
Shader "Unlit/UnlitTexturedWithNoise"
{
	Properties
	{
		_ColorTexture("ColorTexture", 2D) = "white" {}
		_NoiseWeight("NoiseWeight", Range( 0 , 1)) = 0.2470588
		_NoiseTexture("NoiseTexture", 2D) = "white" {}
		[HideInInspector] _texcoord( "", 2D ) = "white" {}
		[HideInInspector] __dirty( "", Int ) = 1
	}

	SubShader
	{
		Tags{ "RenderType" = "Opaque"  "Queue" = "Geometry+0" "IsEmissive" = "true"  }
		Cull Back
		CGPROGRAM
		#pragma target 3.0
		#pragma surface surf Unlit keepalpha addshadow fullforwardshadows 
		struct Input
		{
			float2 uv_texcoord;
		};

		uniform sampler2D _ColorTexture;
		uniform float4 _ColorTexture_ST;
		uniform float _NoiseWeight;
		uniform sampler2D _NoiseTexture;
		uniform float4 _NoiseTexture_ST;

		inline fixed4 LightingUnlit( SurfaceOutput s, half3 lightDir, half atten )
		{
			return fixed4 ( 0, 0, 0, s.Alpha );
		}

		void surf( Input i , inout SurfaceOutput o )
		{
			float2 uv_ColorTexture = i.uv_texcoord * _ColorTexture_ST.xy + _ColorTexture_ST.zw;
			float2 uv_NoiseTexture = i.uv_texcoord * _NoiseTexture_ST.xy + _NoiseTexture_ST.zw;
			float layeredBlendVar18 = _NoiseWeight;
			float4 layeredBlend18 = ( lerp( float4(1,1,1,0),tex2D( _NoiseTexture, uv_NoiseTexture ) , layeredBlendVar18 ) );
			o.Emission = ( ( tex2D( _ColorTexture, uv_ColorTexture ) + float4(0.01960784,0.01960784,0.01960784,0) ) * layeredBlend18 ).rgb;
			o.Alpha = 1;
		}

		ENDCG
	}
	Fallback "Diffuse"
	CustomEditor "ASEMaterialInspector"
}
/*ASEBEGIN
Version=15301
347;1678;2077;1253;1128.928;770.1878;1;True;True
Node;AmplifyShaderEditor.TexturePropertyNode;12;-1099,434.5;Float;True;Property;_NoiseTexture;NoiseTexture;2;0;Create;True;0;0;False;0;ffa9c02760c2b4e8eb9814ec06c4b05b;572dde9e7c264ac42a4779d06c8c1ec9;False;white;Auto;0;1;SAMPLER2D;0
Node;AmplifyShaderEditor.TexturePropertyNode;19;-1103.727,-521.1595;Float;True;Property;_ColorTexture;ColorTexture;0;0;Create;True;0;0;False;0;None;2730cb30c4eed3a46b50e4c480c8e214;False;white;Auto;0;1;SAMPLER2D;0
Node;AmplifyShaderEditor.TextureCoordinatesNode;14;-791,594.5;Float;False;0;-1;2;3;2;SAMPLER2D;;False;0;FLOAT2;1,1;False;1;FLOAT2;0,0;False;5;FLOAT2;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.TextureCoordinatesNode;20;-764.8975,-302.383;Float;False;0;-1;2;3;2;SAMPLER2D;;False;0;FLOAT2;1,1;False;1;FLOAT2;0,0;False;5;FLOAT2;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.ColorNode;16;-448,671.5;Float;False;Constant;_Color1;Color 1;0;0;Create;True;0;0;False;0;1,1,1,0;0,0,0,0;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.RangedFloatNode;17;-510.4236,163.6553;Float;False;Property;_NoiseWeight;NoiseWeight;1;0;Create;True;0;0;False;0;0.2470588;0.334;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.SamplerNode;13;-532,432.5;Float;True;Property;_TextureSample0;Texture Sample 0;1;0;Create;True;0;0;False;0;None;None;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SamplerNode;21;-505.8974,-464.383;Float;True;Property;_TextureSample1;Texture Sample 1;1;0;Create;True;0;0;False;0;None;None;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.ColorNode;22;-506.557,-138.5272;Float;False;Constant;_Color0;Color 0;2;0;Create;True;0;0;False;0;0.01960784,0.01960784,0.01960784,0;0,0,0,0;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.LayeredBlendNode;18;-100,431.5;Float;False;6;0;FLOAT;0;False;1;COLOR;0,0,0,0;False;2;COLOR;0,0,0,0;False;3;COLOR;0,0,0,0;False;4;COLOR;0,0,0,0;False;5;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.SimpleAddOpNode;23;-132.557,-127.5272;Float;False;2;2;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;10;141,-117.5;Float;False;2;2;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.StandardSurfaceOutputNode;11;493,-174;Float;False;True;2;Float;ASEMaterialInspector;0;0;Unlit;Unlit/UnlitTexturedWithNoise;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;Back;0;False;-1;0;False;-1;False;0;0;False;0;Opaque;0.5;True;True;0;False;Opaque;;Geometry;All;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;0;False;-1;False;0;False;-1;255;False;-1;255;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;False;2;15;10;25;False;0.5;True;0;0;False;-1;0;False;-1;0;0;False;-1;0;False;-1;-1;False;-1;-1;False;-1;0;False;0;0,0,0,0;VertexOffset;True;False;Cylindrical;False;Relative;0;;-1;-1;-1;-1;0;0;0;False;0;0;0;False;-1;-1;0;False;-1;15;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;2;FLOAT3;0,0,0;False;3;FLOAT;0;False;4;FLOAT;0;False;6;FLOAT3;0,0,0;False;7;FLOAT3;0,0,0;False;8;FLOAT;0;False;9;FLOAT;0;False;10;FLOAT;0;False;13;FLOAT3;0,0,0;False;11;FLOAT3;0,0,0;False;12;FLOAT3;0,0,0;False;14;FLOAT4;0,0,0,0;False;15;FLOAT3;0,0,0;False;0
WireConnection;14;2;12;0
WireConnection;20;2;19;0
WireConnection;13;0;12;0
WireConnection;13;1;14;0
WireConnection;21;0;19;0
WireConnection;21;1;20;0
WireConnection;18;0;17;0
WireConnection;18;1;16;0
WireConnection;18;2;13;0
WireConnection;23;0;21;0
WireConnection;23;1;22;0
WireConnection;10;0;23;0
WireConnection;10;1;18;0
WireConnection;11;2;10;0
ASEEND*/
//CHKSM=C8A0236571CC13E3980973B84FD7AFCFEED4084F