Shader "Custom/Water/Water1"{

	Properties{
		_Color1("Base Color" , Color) = (1,1,1,1)
		_Color2("Highlight Color" , Color) = (1,1,1,1)

		_Amount("Wave Amount" , Range(0,100)) = 1.0

	}

	SubShader
	{
		Pass
		{
		CGPROGRAM
#pragma vertex vert
#pragma fragment frag
#include "UnityCG.cginc"

		struct v2f {

		float4 pos : SV_POSITION;
		fixed4 color : COLOR;
};
	float _Amount;
	float4 _Color;

	void vert (inout appdata_full v) {

		v.vertex.xyz += v.normal * _Amount * abs(sin(_Time * 3)) * v.color.x;
		v.vertex.y += _Amount * abs(sin(_Time * 200)) * v.color.y;
		v.vertex.color = _Color;
	}


		ENDCG

		
			Lighting On
		}
	}
}