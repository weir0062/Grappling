Shader "Custom/WaveShader" {
    Properties{
        _MainTex("Texture", 2D) = "white" {}
        _WaveAmplitude("Wave Amplitude", Range(0,1)) = 0.1
        _WaveFrequency("Wave Frequency", Range(0,10)) = 1
    }
        SubShader{
            Tags {"Queue" = "Transparent" "RenderType" = "Opaque"}
            Pass {
                CGPROGRAM
                #pragma vertex vert
                #pragma fragment frag
                #include "UnityCG.cginc"

                struct appdata {
                    float4 vertex : POSITION;
                    float2 uv : TEXCOORD0;
                };
                struct v2f {
                    float2 uv : TEXCOORD0;
                    float4 vertex : SV_POSITION;
                };

                sampler2D _MainTex;
                float _WaveAmplitude;
                float _WaveFrequency;

                v2f vert(appdata v) {
                    v2f o;
                    o.vertex = UnityObjectToClipPos(v.vertex);
                    o.uv = v.uv;
                    return o;
                }

                fixed4 frag(v2f i) : SV_Target {
                    float2 uv = i.uv;
                    uv.y += _Time.y * _WaveFrequency;
                    float wave = sin(uv.y * 10 * _WaveFrequency) * _WaveAmplitude;
                    fixed4 col = tex2D(_MainTex, uv);
                    col.a = 1.0;
                    col.rgb += wave;
                    return col;
                }
                ENDCG
            }
        }
            FallBack "Diffuse"
}