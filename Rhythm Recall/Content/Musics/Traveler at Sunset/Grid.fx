#if OPENGL
#define SV_POSITION POSITION
#define VS_SHADERMODEL vs_3_0
#define PS_SHADERMODEL ps_3_0
#else
#define VS_SHADERMODEL vs_4_0_level_9_3
#define PS_SHADERMODEL ps_4_0_level_9_3
#endif   

#define PI 3.1415926

float iTime;
float iIntensity;
float iIntensity2;
float iIntensity3;
float iGlowDistance = 0.0f;
float iGlowIntensity = 0.0f;
float3 iColor;
float3 iSide;
sampler2D SpriteTextureSampler : register(s0);
sampler2D hash : register(s0);
  
struct VertexShaderOutput
{
    float4 Position : SV_POSITION;
    float4 Color : COLOR0;
    float2 TextureCoordinates : TEXCOORD0;
};  

float pat(float2 uv, float p, float q, float s, float glow)
{
    float z = cos(q * PI * uv.x) * cos(p * PI * uv.y) + cos(q * PI * uv.y) * cos(p * PI * uv.x);

    z += sin(iTime * 4.0 + uv.x + uv.y * s) * 0.035; 
    float dist = abs(z) * (1.0 / glow);
    return dist;
}
 
float3 side(float2 uv)
{ 
    float3 theme = iSide;
    
    float i = abs(uv.x - 0.5);
    
    float2 pos = float2(sin(uv.y + iTime * 0.3 + uv.x * 0.1) * 0.5 + 0.5, frac(iTime * 0.03) + sin(iTime * 0.024));
    
    float del = tex2D(hash, pos).r * 0.52 
                + sin(uv.y * 52.0 * 1.5 + iTime * 3) * 0.3 + 0.3
                + sin(uv.y * 74.0 * 1.5 + iTime * -4.5) * 0.2 + 0.2
                + sin(uv.y * 104.0 * 1.5 + iTime * 7.5) * 0.07 + 0.12
                + sin(uv.y * 164.0 * 1.5 + iTime * -10.1) * 0.025;
    
    i += del * (uv.x - 0.5) * (uv.x - 0.5) * abs(uv.x - 0.5);
    
    i += iIntensity2;
    if(i > 0)
        i = pow(i - 0.45, 4.0);
    else
        i = 0; 
    
    float3 col = theme * i;
    col = min(col, float3(1, 1, 1) * iIntensity3);
    return col;
}

float4 MainPS(VertexShaderOutput input) : COLOR
{
    float2 uv = input.TextureCoordinates;
    float ud = distance(float2(0.5, 0.5), uv) - iGlowDistance;
    float3 col0 = side(uv);
    uv.x = (uv.x - 0.5) * 4 / 3;
    uv.y = uv.y - 0.5;
    float d = pat(uv, 6.0, 2.2, 35.0, 0.35);
    d *= pat(uv, 1.2, 5.0, 55.0, 0.51);
    float3 col = iColor / d;
    col = min(col, float3(1, 1, 1));
    float i2 = iGlowIntensity * pow(2.71828, -ud * ud * 45);
    float i = max(i2, iIntensity);
    col *= float3(i, i, i);
    col += col0;
    return input.Color * float4(col.x, col.y, col.z, 1);
}

technique SpriteDrawing
{
    pass P0
    {
        PixelShader = compile PS_SHADERMODEL MainPS();
    }
};