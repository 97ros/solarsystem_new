Shader "Custom/UIBlur"
{
    Properties
    {
        _MainTex ("Base (RGB)", 2D) = "white" { }
        _BlurSize ("Blur Size", Float) = 1.0
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }
        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag

            #include "UnityCG.cginc"

            sampler2D _MainTex;
            float _BlurSize;

            struct appdata_t {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct v2f {
                float2 uv : TEXCOORD0;
                float4 vertex : SV_POSITION;
            };

            v2f vert(appdata_t v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
                return o;
            }

            half4 frag(v2f i) : SV_Target
            {
                half4 color = half4(0,0,0,0);
                float2 uv = i.uv;
                
                // Sample the main texture with an offset to create a blur
                float2 offsets[9] = {
                    float2(-_BlurSize, 0),
                    float2(_BlurSize, 0),
                    float2(0, -_BlurSize),
                    float2(0, _BlurSize),
                    float2(-_BlurSize, -_BlurSize),
                    float2(_BlurSize, -_BlurSize),
                    float2(-_BlurSize, _BlurSize),
                    float2(_BlurSize, _BlurSize),
                    float2(0, 0)
                };

                // Accumulate colors from neighboring pixels
                for (int j = 0; j < 9; j++)
                {
                    color += tex2D(_MainTex, uv + offsets[j]);
                }
                color /= 9;

                return color;
            }
            ENDCG
        }
    }
    Fallback "Diffuse"
}
