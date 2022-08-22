Shader "Shotake/WorldPlane"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
		_OutlineWidth("Outline", Float) = 0.1
		_OutlineColor("Outline Color", Color) = (0,0,0,1)
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }
        LOD 100
        
		// outline pass
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

			half _OutlineWidth;
			fixed4 _OutlineColor;

			v2f vert(appdata v)
			{
				v2f o;               
                float4 clipPosition = UnityObjectToClipPos(v.vertex);
                float3 clipNormal = mul((float3x3) UNITY_MATRIX_VP, mul((float3x3) UNITY_MATRIX_M, v.normal));
                float2 offset = (2 * _OutlineWidth) * normalize(clipNormal.xy) / _ScreenParams.xy;				
                clipPosition.xy += offset;
                o.pos = clipPosition;
				return o;
			}

			half4 frag(v2f i) : COLOR
			{
				return _OutlineColor;
			}
			ENDCG
		}
		// sample pass
        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag

            #include "UnityCG.cginc"

            struct appdata
            {
                float4 vertex : POSITION;
            };

            struct v2f
            {
                float4 vertex : SV_POSITION;
                float2 uv : TEXCOORD0;
            };

            sampler2D _MainTex;
            float4 _MainTex_ST;

            v2f vert (appdata v)
            {
                v2f o;
                float4 wpos = mul(unity_ObjectToWorld, v.vertex);
                o.vertex = mul(UNITY_MATRIX_VP, wpos);
                o.uv = TRANSFORM_TEX(wpos.xz, _MainTex);
                o.uv.x += sin(o.uv.y);
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                fixed4 col = tex2D(_MainTex, i.uv);
                return col;
            }
            ENDCG
        }
    }
}
