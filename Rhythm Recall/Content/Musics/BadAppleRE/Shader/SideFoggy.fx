#if OPENGL
#define SV_POSITION POSITION
#define VS_SHADERMODEL vs_3_0
#define PS_SHADERMODEL ps_3_0
#else
#define VS_SHADERMODEL vs_4_0_level_9_1
#define PS_SHADERMODEL ps_4_0_level_9_1
#endif

Texture2D SpriteTexture;
float Iintensity;
float itime;
sampler2D Sample = sampler_state
{
    Texture = <SpriteTexture>;
};
struct VertexShaderOutput
{
    float4 Position : SV_POSITION;
    float4 Color : COLOR0;
    float2 pos : TEXCOORD0;
};

float4 MainPS(VertexShaderOutput input) : COLOR
{
    float2 pos = input.pos-0.5;
    float4 color = tex2D(Sample, input.pos);
    color -= pow(pow(pos.x, 2) + pow(pos.y , 2), 0.5) * Iintensity*3;
    if (pos.x > 0)
        pos.y = 0.5 - pos.y;
    color -= max(0, abs(
    sin(itime + 147.525 * pos.y) +
    cos(itime - 93.526 * pos.y) +
    sin(itime + 76.91 * pos.y) +
    cos(itime - 116.462 * pos.y)) / 4 * abs(pos.x) * (Iintensity/4 * (abs(pos.y) + 0.1)*4));
    return color;
}

technique SpriteDrawing
{
    pass P0
    {
        PixelShader = compile PS_SHADERMODEL MainPS();
    }
};