Shader "Custom/Water/Water1"{

	Properties{
		_Color1("Base Color" , Color) = (1,1,1,1)
		_Color2("Highlight Color" , Color) = (1,1,1,1)

	}

	SubShader
	{
		Pass
		{

		Material
			{

				Diffuse [_Color1]
			}
			Lighting On
		}
	}
}