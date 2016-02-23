﻿Shader "Custom/MatteShadow"
{
	Properties
	{
		_ShadowStrength("Shadow Strength", Range(0, 1)) = 1
	}
		SubShader
	{
		Tags
	{
		"Queue" = "AlphaTest"
		"IgnoreProjector" = "True"
		"RenderType" = "Transparent"
	}
		Pass
	{
		Blend SrcAlpha OneMinusSrcAlpha
		Name "ShadowPass"
		Tags{ "LightMode" = "ForwardBase" }

		CGPROGRAM
#pragma vertex vert
#pragma fragment frag
#pragma multi_compile_fwdbase

#include "UnityCG.cginc"
#include "AutoLight.cginc"
	struct v2f
	{
		float4 pos : SV_POSITION;
		LIGHTING_COORDS(0,1)
	};

	fixed _ShadowStrength;
	v2f vert(appdata_full v)
	{
		v2f o;
		o.pos = mul(UNITY_MATRIX_MVP, v.vertex);
		TRANSFER_VERTEX_TO_FRAGMENT(o);
		return o;
	}
	fixed4 frag(v2f i) : COLOR
	{
		fixed atten = LIGHT_ATTENUATION(i);
	fixed shadowalpha = (1.0 - atten) * _ShadowStrength;
	return fixed4(0.0, 0.0, 0.0, shadowalpha);
	}
		ENDCG
	}
		UsePass "Legacy Shaders/VertexLit/SHADOWCASTER"
	}
}