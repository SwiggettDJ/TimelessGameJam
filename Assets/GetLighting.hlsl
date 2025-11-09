
void GetLight_float(float3 WorldPos, out float3 Direction, out float3 Color, out float Attenuation)

    #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Lighting.hlsl"
    #if defined(SHADERGRAPH_PREVIEW)
    Direction = float3(0.5,0.5,0);
    Color = 1;
    Attenuation = 1;

    #else
    Light mainLight = GetMainLight();
    Direction = mainLight.direction;
    Color = mainLight.color;
    Attenuation = mainLight.shadowAttenuation;

#endif