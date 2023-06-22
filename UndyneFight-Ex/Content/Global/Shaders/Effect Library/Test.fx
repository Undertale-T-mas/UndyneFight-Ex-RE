#if OPENGL
#define SV_POSITION POSITION
#define VS_SHADERMODEL vs_3_0
#define PS_SHADERMODEL ps_3_0
#else
#define VS_SHADERMODEL vs_4_0_level_9_1
#define PS_SHADERMODEL ps_4_0_level_9_1
#endif

Texture2D SpriteTexture;

sampler2D SpriteTextureSampler = sampler_state
{
    Texture = <SpriteTexture>;
};

struct VertexShaderOutput
{
    float4 Position : SV_POSITION;
    float4 Color : COLOR0;
    float2 TextureCoordinates : TEXCOORD0;
};
float2 distance1;
float2 distance2;
float2 distance3;
float4 GetColor(float2 pos)
{
    float4 a;
    if (pos.y > 1.0)
        a = float4(0, 0, 0, 0);
    else
        a = tex2D(SpriteTextureSampler, pos);
    return a;
}


float4 MainPS(VertexShaderOutput input) : COLOR
{
    float4 A = GetColor(input.TextureCoordinates) * input.Color * 0.6;
    float4 B = GetColor(input.TextureCoordinates + distance1) * input.Color * 0.4 / 3;
    float4 C = GetColor(input.TextureCoordinates + distance2) * input.Color * 0.4 / 3;
    float4 D = GetColor(input.TextureCoordinates + distance3) * input.Color * 0.4 / 3;
    return A + B + C + D;
}
technique SpriteDrawing
{
    pass P0
    {
        PixelShader = compile PS_SHADERMODEL MainPS();
    }
};