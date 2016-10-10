Shader "Custom/Water/Water1"{

	Properties{
		_Color1("Base Color" , Color) = (1,1,1,1)
		_Color2("Highlight Color" , Color) = (1,1,1,1)

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

	v2f vert(appdata_base v) {
		v2f o;
		o.pos = UnityObjectToClipPos(v.vertex);
		o.color.xyz = v.normal * 0.5 + 0.5;
		o.color.w = 1;
		return o;
	}

	fixed4 frag(v2f i) : SV_Target{ return i.color; }
		ENDCG

		Material
			{

				Diffuse [_Color1]
			}
			Lighting On
		}
	}
}