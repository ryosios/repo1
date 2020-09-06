Shader "Unlit/dissolve"
{
	Properties{
		_MainTex("Texture", 2D) = "white" {}
		_NoiseTex("Noise", 2D) = "white" {}
		_Gradient("Gradient", Range(0.0, 1.0)) = 0.1
		[KeywordEnum(None, Front, Back)] _Cull("Culling", Int) = 2
	}

		SubShader
		{
			Tags { "Queue" = "Transparent" "RenderType" = "Transparent" }
			Blend SrcAlpha One
			Cull[_Cull]
			Lighting Off ZWrite Off Fog { Mode Off }
			Pass
			{
				CGPROGRAM
				#pragma vertex vert
				#pragma fragment frag
				#pragma fragmentoption ARB_precision_hint_fastest
				#pragma multi_compile_particles

				#include "UnityCG.cginc"

				uniform sampler2D _MainTex;
				uniform fixed4 _TintColor;

				struct appdata_t
				{
					float4 vertex : POSITION;
					fixed4 color : COLOR;
					float2 texcoord : TEXCOORD0;
					float4 texcoord1 : TEXCOORD1;
				};

				struct v2f
				{
					float4 vertex : POSITION;
					fixed4 color : COLOR;
					float4 texcoord : TEXCOORD0;
					float4 texcoord1 : TEXCOORD1;
				};

				float4 _MainTex_ST;

				sampler2D _NoiseTex;
				float4 _NoiseTex_ST;
				float _Gradient;

				v2f vert(appdata_t v)
				{
					v2f o;
					o.vertex = UnityObjectToClipPos(v.vertex);
					o.color = v.color;
					o.texcoord.xy = TRANSFORM_TEX(v.texcoord, _MainTex);
					o.texcoord.x += v.texcoord1.x; 
					o.texcoord.y += v.texcoord1.y;

					o.texcoord.zw = TRANSFORM_TEX(v.texcoord, _NoiseTex);
					o.texcoord.z *= v.texcoord1.z;
					o.texcoord.w *= v.texcoord1.z;//ノイズ細かさ
					
					o.texcoord1.w = v.texcoord1.w;//フラグメントにtexcoord1.w受け渡し

					return o;
				}

				fixed4 frag(v2f i) : COLOR
				{
					fixed4 col = tex2D(_MainTex, i.texcoord.xy);
					fixed texAlpha = col.a;
					fixed maskAlpha = tex2D(_NoiseTex, i.texcoord.zw).r;
					fixed vtxAlpha = 1.0 - i.color.a;

					col.rgb *= i.color.rgb;
				    _Gradient = i.texcoord1.w;
					maskAlpha = maskAlpha * max(1.0 - _Gradient, 0.0) + _Gradient;
					clip(texAlpha * maskAlpha - vtxAlpha - 0.01);
					col.a = smoothstep(vtxAlpha, vtxAlpha + _Gradient, maskAlpha);

					return col;
				}
				ENDCG
			}
		}
			FallBack off
}
