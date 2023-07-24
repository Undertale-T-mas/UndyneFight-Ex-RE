#if OPENGL
#define SV_POSITION POSITION
#define VS_SHADERMODEL vs_3_0
#define PS_SHADERMODEL ps_3_0
#else
#define VS_SHADERMODEL vs_4_0_level_9_3
#define PS_SHADERMODEL ps_4_0_level_9_3
#endif  

//uniform float iRotation;
uniform float3 iRGB1, iRGB2;
uniform float iTime; 
uniform float iSlope, iAddition; 


sampler2D SpriteTextureSampler : register(s0);

sampler2D hashSample : register(s1);

struct VertexShaderOutput
{
    float4 Position : SV_POSITION;
    float4 Color : COLOR0;
    float2 TextureCoordinates : TEXCOORD0;
};  

float4 MainPS(VertexShaderOutput input) : COLOR
{ 
    float2 uv = input.TextureCoordinates;
    float o = tex2D(SpriteTextureSampler, uv * 0.25 + float2(0, iTime * 0.025)).r;
    float d = tex2D(hashSample, uv * 0.25 - float2(0, iTime * 0.02 + o * 0.02)).r * 2.0 - 1.0;
    float v = uv.y + d * 0.1;
    v = 1.0 - abs(v * 2.0 - 1.0);
    v = pow(v, 2.0 + sin((iTime * 0.2 + d * 0.25) * 6.28) * 0.5);
    
    float3 col = float3(0, 0, 0); 
    float x1 = (1.0 - uv.x * 0.75);
    float y1 = 1.0 - abs(uv.y * iSlope - iAddition);
    col += (x1 * iRGB1 + y1 * iRGB2) * v;
    
    // stars:
  /*  float2 seed = uv * float2(640, 480); 
    float2 r;
    r.x = frac(sin((seed.x * 12.9898) + (seed.y * 78.2330)) * 43758.5453);
    r.y = frac(sin((seed.x * 53.7842) + (seed.y * 47.5134)) * 43758.5453);
    
    float s = lerp(r.x, (sin((iTime * 2.5 + 60.0) * r.y) * 0.5 + 0.5) * ((r.y * r.y) * (r.y * r.y)), 0.04);
    col += pow(s, 45.0) * (1.0 - v);*/
    
    return input.Color * float4(col.x, col.y, col.z, 1);
}

technique SpriteDrawing
{
    pass P0
    {
        PixelShader = compile PS_SHADERMODEL MainPS();
    }
};