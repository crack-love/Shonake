Shader "Shotake/Orthographic/SpriteOutlinedRelative"
{
    Properties
    {
        _MainTex("Texture", 2D) = "white" {}
        _MainColor("Main Color", Color) = (1, 1, 1, 1)
        _AlphaClip("Alpha Clip", Float) = 0

        _OLPercent("Outline Percent", Float) = 1
        _OLColor("Outline Color", Color) = (0,0,0,1)        
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }
        LOD 100

        // Outline Pass        
        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #pragma multi_compile_instancing
            #include "UnityCG.cginc"
            
            struct appdata
            {
                float4 vertex : POSITION;
                fixed2 uv : TEXCOORD0;
                UNITY_VERTEX_INPUT_INSTANCE_ID
            };

            struct v2f
            {
                float4 pos : SV_POSITION;
                float2 uv  : TEXCOORD0;
            };
            
            sampler2D _MainTex;
            float4 _MainTex_ST;
            float _OLPercent;
            fixed4 _OLColor;
            half _AlphaClip;

            v2f vert(appdata v)
            {
                v2f o;
                UNITY_SETUP_INSTANCE_ID(v);

                v.vertex.y -= 0.001;
                o.pos = UnityObjectToClipPos(v.vertex * _OLPercent);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                return o;
            }

            half4 frag(v2f i) : COLOR
            {
                half4 c = tex2D(_MainTex, i.uv);
                clip(c.a - _AlphaClip);
                return _OLColor;
            }
            ENDCG
        }
        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag

            #include "UnityCG.cginc"
            
            struct appdata
            {
                float4 vertex : POSITION;
                fixed2 uv : TEXCOORD0;
                UNITY_VERTEX_INPUT_INSTANCE_ID
            };

            struct v2f
            {
                float4 pos : SV_POSITION;
                float2 uv  : TEXCOORD0;
            };
            
            sampler2D _MainTex;
            float4 _MainTex_ST;
            half4 _MainColor;
            half _AlphaClip;

            v2f vert(appdata v)
            {
                v2f o;           
                UNITY_SETUP_INSTANCE_ID(v);    

                o.pos = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);				
                return o;
            }

            half4 frag(v2f i) : COLOR
            {
                half4 c = tex2D(_MainTex, i.uv);
                clip(c.a - _AlphaClip);
                return c * _MainColor;
            }
            ENDCG
        }
    }
}
