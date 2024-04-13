Shader "Aimk/AimkUVScroll"{
	Properties{
		_DistortionTex("Distortion Texture(RG)", 2D) = "grey" {}
		_DistortionPower("Distortion Power", Range(0, 1)) = 0
		_DistortionSpeed("Distortion Speed", Range(0, 1)) = 0

        _XShift("Xuv Shift", Range(-1.0, 1.0)) = 0.1
        _XSpeed("X Scroll Speed", Range(0, 1.0)) = 0.5
        _YShift("Yuv Shift", Range(-1.0, 1.0)) = 0.1
        _YSpeed("Y Scroll Speed", Range(0, 1.0)) = 0.5
	}

	SubShader{
		Tags {"Queue" = "Transparent" "RenderType" = "Transparent" }

		Cull Back
		ZWrite On
		ZTest LEqual
		ColorMask RGB

		GrabPass { "_GrabPassTexture" }

		Pass {
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			#include "UnityCG.cginc"

			struct appdata {
				half4 vertex  : POSITION;
				half4 texcoord  : TEXCOORD0;
			};

			struct v2f {
				half4 vertex  : SV_POSITION;
				half2 uv  : TEXCOORD0;
				half4 grabPos : TEXCOORD1;
			};

			sampler2D _DistortionTex;
			half4 _DistortionTex_ST;
			sampler2D _GrabPassTexture;
			half _DistortionPower;
			half _DistortionSpeed;
			float _XShift;
			float _YShift;
			float _XSpeed;
			float _YSpeed;

			v2f vert(appdata v)	{
				v2f o = (v2f)0;
				o.vertex = UnityObjectToClipPos(v.vertex);
				o.uv = TRANSFORM_TEX(v.texcoord, _DistortionTex);
				o.grabPos = ComputeGrabScreenPos(o.vertex);
				return o;
			}

			fixed4 frag(v2f i) : SV_Target	{

				i.uv.x = i.uv.x + _XShift * _XSpeed * _Time;
				i.uv.y = i.uv.y + _YShift * _YSpeed * _Time;

				half2 uv = half2(i.grabPos.x / i.grabPos.w, i.grabPos.y / i.grabPos.w);
				//half2 uv = half2(110.5f, 0.3f);
				//uv.x = uv.x + 1.0f * 100.0f * _Time;
				half2 distortion = tex2D(_DistortionTex, i.uv + _Time.x * _DistortionSpeed).rg - 0.1f;
				//distortion *= _DistortionPower;

				uv = uv + distortion;
				return tex2D(_GrabPassTexture, uv);
			}
			ENDCG
		}
	}
}