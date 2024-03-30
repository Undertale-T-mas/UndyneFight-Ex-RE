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
    float4 color = tex2D(Sample,input.pos);
    for (int i = 0; i < 7;i++)
        color += tex2D(Sample, input.pos + float2((i - 3) * Iintensity,0));
    for (int i1 = 0; i1 < 7; i1++)
        color += tex2D(Sample, input.pos + float2(sin(3.14 / 2)*
    (i - 3) * Iintensity, sin(3.14 / 2) * (i - 3) * Iintensity));
    for (int i2 = 0; i2 < 7; i2++)
        color += tex2D(Sample, input.pos + float2(0, (i - 3) * Iintensity));
    color /= 21;
    return color;
}

technique SpriteDrawing
{
    pass P0
    {
        PixelShader = compile PS_SHADERMODEL MainPS();
    }
};