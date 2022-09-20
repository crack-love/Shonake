Shader "Shotake/(obsolete)OrthographicOutlineAbs"
{
	// ortho outline (abs pixel)
    Properties
    {
		_Width("Outline Pixel", Int) = 1
		_Color("Outline Color", Color) = (0,0,0,1)
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }
        LOD 100
        
		Pass
		{
            Cull Front

			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
            
			struct appdata
			{
				float4 vertex : POSITION;
                float3 normal : NORMAL;
				

			};

			struct v2f
			{
				float4 pos : SV_POSITION;
			};

			half _Width;
			fixed4 _Color;

			v2f vert(appdata v)
			{
				v2f o;               
				
                float4 clipPosition = UnityObjectToClipPos(v.vertex);
                float3 clipNormal = mul((float3x3) UNITY_MATRIX_VP, mul((float3x3) UNITY_MATRIX_M, v.normal));
				// multiply 2 to correct pixel size because current unit of pixel is half
				// divide _screenSize to fit ratio
                float2 offset = (2 * _Width) * normalize(clipNormal.xy) / _ScreenParams.xy;				
                clipPosition.xy += offset;
                o.pos = clipPosition;
				
				return o;
			}

			half4 frag(v2f i) : COLOR
			{
				return _Color;
			}
			ENDCG
		}
    }
}
