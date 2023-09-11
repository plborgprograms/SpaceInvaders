Shader "Custom/Glow" {
    Properties{
        _MainTex("Texture", 2D) = "white" {}
        _Color("Color", Color) = (1, 1, 1, 1)
        _RimColor("Rim Color", Color) = (0, 0.5, 1, 1)
        _RimPower("Rim Power", Range(0.5, 8)) = 3
    }
        SubShader{
            Tags { "RenderType" = "Opaque" }
            LOD 100

            CGPROGRAM
            #pragma surface surf Standard

            sampler2D _MainTex;
            float4 _Color;
            float4 _RimColor;
            float _RimPower;

            struct Input {
                float2 uv_MainTex;
                float3 worldPos;
                float3 worldNormal;
                float3 worldViewDir;
            };

            void surf(Input IN, inout SurfaceOutputStandard o) {
                o.Albedo = _Color.rgb;
                o.Alpha = 0;

                float rim = 1 - saturate(dot(IN.worldNormal, IN.worldViewDir));
                rim = pow(rim, _RimPower);
                o.Emission = _RimColor.rgb * rim;
            }
            ENDCG
        }
            FallBack "Diffuse"
}
