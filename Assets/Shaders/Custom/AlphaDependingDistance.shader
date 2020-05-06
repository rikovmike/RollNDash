// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "Custom/AlphaDependingDistance"
{
  Properties
  {
    _Distance ("Distance", Float) = 0
    _FadeStartDistance ("FadeStartDistance", Float) = 8
    _FadeCompleteDistance ("FadeCompleteDistance", Float) = 3
    _Color ("Color", Color) = (1,1,1,1)
        _Glossiness ("Smoothness", Range(0,1)) = 0.5
        _Metallic ("Metallic", Range(0,1)) = 0.0

  }

  SubShader
  {
        Tags {"LightMode"="ForwardBase" "Queue"="Transparent" "RenderType"="Transparent" "IgnoreProjector"="True" }

        
    Pass
    {

ZWrite Off
Blend SrcAlpha OneMinusSrcAlpha
     //AlphaToMask On

      CGPROGRAM
      #pragma vertex vert
      #pragma fragment frag
      #pragma multi_compile_fog    

      #include "UnityCG.cginc"
                 #include "Lighting.cginc"
            // compile shader into multiple variants, with and without shadows
            // (we don't care about any lightmaps yet, so skip these variants)
            #pragma multi_compile_fwdbase nolightmap nodirlightmap nodynlightmap novertexlight
            // shadow helper functions and macros
            #include "AutoLight.cginc"

            
      float _Distance;
      float _FadeStartDistance;
      float _FadeCompleteDistance;
      fixed4 _Color;
        half _Glossiness;
        half _Metallic;


      struct appdata
      {
        float4 vertex : POSITION;
        float2 uv : TEXCOORD0;
        float3 normal : NORMAL;
      };

      struct v2f
      {
        float2 uv : TEXCOORD0;
        float4 vertex : SV_POSITION;
        float4 worldPos : TEXCOORD1;

                SHADOW_COORDS(2) // put shadows data into TEXCOORD2
                UNITY_FOG_COORDS(3)
                fixed3 diff : COLOR0;
                fixed3 ambient : COLOR1;
        

      };

      v2f vert (appdata v)
      {
        v2f o;
        o.vertex = UnityObjectToClipPos(v.vertex);
        o.uv = v.uv;
        o.worldPos = mul(unity_ObjectToWorld, v.vertex);

                o.vertex = UnityObjectToClipPos(v.vertex);
                half3 worldNormal = UnityObjectToWorldNormal(v.normal);
                half nl = max(0, dot(worldNormal, _WorldSpaceLightPos0.xyz));
                o.diff = nl * _LightColor0.rgb;
                o.ambient = ShadeSH9(half4(worldNormal,1));
                // compute shadows data
                TRANSFER_SHADOW(o);
                UNITY_TRANSFER_FOG(o,o.vertex);

        return o;
      }

      fixed4 frag (v2f i) : SV_Target
      {

           fixed4 col = _Color;

          _Distance = distance(i.worldPos, _WorldSpaceCameraPos);
        if (_Distance > _FadeStartDistance) {
          col.a=1;
        }else{
             if (_Distance > _FadeCompleteDistance) {
                col.a = saturate( (_Distance - _FadeCompleteDistance) / (_FadeStartDistance - _FadeCompleteDistance)); // fading out
            }else{
                col.a=0; // faded out. Opacity = 0
            }
        }
               
                fixed shadow = SHADOW_ATTENUATION(i);
                // darken light's illumination with shadow, keep ambient intact
                fixed3 lighting = i.diff * shadow + i.ambient;
                col.rgb *= lighting;
           UNITY_APPLY_FOG(i.fogCoord, col);

        return col;

      }









      ENDCG
    }

        UsePass "Legacy Shaders/VertexLit/SHADOWCASTER"

  }
}