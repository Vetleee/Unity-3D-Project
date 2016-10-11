Shader "Custom/Learning/diffuse" {
	// We define Properties in the properties block
	Properties{
		_Color("Color", Color) = (1,1,1,1)
	}
		SubShader{
		Tags{ "RenderType" = "Opaque" }
		LOD 200
		// We need to declare the properties variable type inside of the
		// CGPROGRAM so we can access its value from the properties block.
		CGPROGRAM
#pragma surface surf Standard fullforwardshadows
#pragma target 3.0
		struct Input {
		float2 uv_MainTex;
	};
	fixed4 _Color;
	float4 _AmbientColor;
	float _MySliderValue;
	void surf(Input IN, inout SurfaceOutputStandard o) {
		// We can then use the properties values in our shader
		o.Albedo = _Color.rgb;
	}
	ENDCG
	}
		FallBack "Diffuse"
}
