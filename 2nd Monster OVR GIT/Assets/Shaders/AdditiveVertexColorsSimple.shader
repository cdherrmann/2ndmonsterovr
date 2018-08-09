// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "Unlit/AdditiveVertexSimple" {
	Properties{
	}



	SubShader{
		Tags{
			"Queue" = "Transparent"
			"RenderType" = "Transparent"
		}

		Pass{
			Blend SrcAlpha OneMinusSrcAlpha

			Cull Back
			Lighting Off
			ZWrite Off

			CGPROGRAM
				#pragma vertex vert alpha:fade
				#pragma fragment frag


				struct vertexInput {
					float4 pos : POSITION;
					float4 color : COLOR;
				};
				struct vertexOutput {
					float4 pos : SV_POSITION;
					float4 vertCol : COLOR;
				};

				vertexOutput vert(vertexInput input) {
					vertexOutput o;
					o.pos = UnityObjectToClipPos(input.pos);
					o.vertCol = input.color;
					return o;
				}

				float4 frag(vertexOutput output) : COLOR{
					//return output.vertCol;
					return half4(1.0, 1.0, 1.0, output.vertCol.x);
				}
			ENDCG
		}
	}
}