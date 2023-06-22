#if OPENGL
#define SV_POSITION POSITION
#define VS_SHADERMODEL vs_3_0
#define PS_SHADERMODEL ps_3_0
#else
#define VS_SHADERMODEL vs_4_0_level_9_1
#define PS_SHADERMODEL ps_4_0_level_9_1
#endif

#define WIDTH 640.0
#define HEIGHT 480.0
#define PI 3.1415926

uniform float2 iCenter;
uniform float iRadius;
uniform float iCongergence;

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

float4 tex2D_edge(sampler2D sampler_index, float2 position)
{
    if ((position.x < 0.0 || position.y < 0.0) || (position.x > 1.0 || position.y > 1.0))
        return float4(0.0, 0.0, 0.0, 0.0);

    return tex2D(SpriteTextureSampler, position);
}

//return tex2D(samplerTexture, SIZEPIXEL * Position);

float4 MainPS(VertexShaderOutput input) : COLOR
{
    float4 color;
    float2 delta = input.TextureCoordinates * float2(640.0, 480.0) - iCenter;
    float dist = length(delta);
    float offset = sin(dist / iRadius * 1.57) * iCongergence;
    if (dist > iRadius)
        color = float4(0.0, 0.0, 0.0, 0.0);
    else
        color = tex2D_edge(SpriteTextureSampler, (iCenter + delta + normalize(delta) * offset) / float2(640.0, 480.0));

    return input.Color * color;
}

technique SpriteDrawing
{
    pass P0
    {
        PixelShader = compile PS_SHADERMODEL MainPS();
    }
};