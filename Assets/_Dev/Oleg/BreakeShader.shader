Shader "Unlit/BreakeShader"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _Color  ("Tint Color", Color) = (1,1,1,1)
        _CrackTex1 ("Cracks 1", 2D) = "white" {}
        _CrackTex2 ("Cracks 2", 2D) = "white" {}
        _CrackTex3 ("Cracks 3", 2D) = "white" {}

        _Damage ("Damage", Range(0, 1)) = 0.5

    }
    SubShader
    {
        Tags {"Queue"="Transparent" "RenderType"="Transparent"}
        LOD 100

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag

            #include "UnityCG.cginc"

            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                float4 vertex : SV_POSITION;
            };

            sampler2D _MainTex;
            sampler2D _CrackTex1;
            sampler2D _CrackTex2;
            sampler2D _CrackTex3;

            float4 _MainTex_ST;
            float4 _Color;
            half _Damage;

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                fixed4 col = tex2D(_MainTex, i.uv);
                fixed4 crackTex;

                if (_Damage > 0.75)
                {
                    crackTex = tex2D(_CrackTex3, i.uv);
                }
                else if (_Damage > 0.50)
                {
                    crackTex = tex2D(_CrackTex2, i.uv);
                }
                else if (_Damage > 0.25) 
                {
                    crackTex = tex2D(_CrackTex1, i.uv);
                }

                fixed4 result;

                if (_Damage > 0.25) 
                {
                    result = lerp(col, crackTex, crackTex.a);
                }
                else 
                {
                    result = col;
                }
                
                
                return result * _Color;
            }
            ENDCG
        }
    }
}
