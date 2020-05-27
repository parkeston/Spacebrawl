Shader "Custom/Diffuse"
{
    Properties
    {
        _Color ("Color", Color) = (1,1,1,1)
		_Emission("Emission", Float) = 0
        _MainTex ("Albedo (RGB)", 2D) = "white" {}

		_ShadowColor("Shadow Color", Color) = (1,1,1,1)
		_DiffuseVal("Diffuze Val", Color) = (1,1,1,1)
    }
    SubShader
    {
        Tags { "RenderType"="Opaque"}
        LOD 200
        
        Cull Off

        CGPROGRAM
        // Physically based Standard lighting model, and enable shadows on all light types
		#pragma surface surf CSLambert

        // Use shader model 3.0 target, to get nicer looking lighting
        #pragma target 3.0

        sampler2D _MainTex;

        struct Input
        {
            float2 uv_MainTex;
        };

        fixed4 _Color;
		float _Emission;
		float _isTransparent;

		fixed4 _ShadowColor;
		fixed4 _DiffuseVal;

		half4 LightingCSLambert(SurfaceOutput s, half3 lightDir, half atten)
		{
			fixed diff = max(0, dot(s.Normal, lightDir));

			fixed4 c;
			c.rgb = s.Albedo * _LightColor0.rgb * (diff * atten * 2);

			//shadow colorization
			c.rgb += _ShadowColor.xyz * max(0.0, (1.0 - (diff*atten * 2))) * _DiffuseVal;
			c.a = s.Alpha;
			return c;
		}

        // Add instancing support for this shader. You need to check 'Enable Instancing' on materials that use the shader.
        // See https://docs.unity3d.com/Manual/GPUInstancing.html for more information about instancing.
        // #pragma instancing_options assumeuniformscaling
        UNITY_INSTANCING_BUFFER_START(Props)
            // put more per-instance properties here
        UNITY_INSTANCING_BUFFER_END(Props)

        void surf (Input IN, inout SurfaceOutput o)
        {
            // Albedo comes from a texture tinted by color
            fixed4 c = tex2D (_MainTex, IN.uv_MainTex) * _Color;
            o.Albedo = c.rgb;
			o.Emission = c.rgb * tex2D(_MainTex, IN.uv_MainTex).a * _Emission;
			o.Alpha = 1;
        }
        ENDCG
    }
    FallBack "Diffuse"
}
