#if OPENGL
#define SV_POSITION POSITION
#define VS_SHADERMODEL vs_3_0
#define PS_SHADERMODEL ps_3_0
#else
#define VS_SHADERMODEL vs_4_0_level_9_1
#define PS_SHADERMODEL ps_4_0_level_9_1
#endif

//#define CAMERAHIGH 400.0
#define PI 3.1415926
#define RADIUS 4

Texture2D SpriteTexture;

//uniform float iRotation;
uniform float iSigma2;
uniform float light;
uniform float factor;
#define SIZESURFACE float2(640.0, 480.0)//
#define SIZEPIXEL 1.0 / SIZESURFACE

sampler2D SpriteTextureSampler = sampler_state
{
    Texture = <SpriteTexture>;
};
sampler2D temp : register(s1);

struct VertexShaderOutput
{
    float4 Position : SV_POSITION;
    float4 Color : COLOR0;
    float2 TextureCoordinates : TEXCOORD0;
};

float4 localToColor(sampler2D samplerTexture, float2 Position)
{
    return tex2D(samplerTexture, SIZEPIXEL * Position);
}

float MappingToDistribution(float len)
{
    float sig = max(iSigma2, 0.001f);
    return (exp(-pow(len, 2.0) / (2.0 * sig))) / (2.0 * PI * sig);
}

float4 MainPS(VertexShaderOutput input) : COLOR
{
    float InitialValueSum = MappingToDistribution(0.0f);
    for (int i = 1; i <= RADIUS; i++)
        InitialValueSum += MappingToDistribution(float(i)) * 2.0f;
	
    float4 color = localToColor(SpriteTextureSampler, input.TextureCoordinates * SIZESURFACE) * MappingToDistribution(0.0f) / InitialValueSum;
    for (i = 1; i <= RADIUS; i++)
    {
        color += localToColor(SpriteTextureSampler, input.TextureCoordinates * SIZESURFACE + (float2(factor, 0) * float(i))) * (MappingToDistribution(float(i)) / InitialValueSum);
        color += localToColor(SpriteTextureSampler, input.TextureCoordinates * SIZESURFACE - (float2(factor, 0) * float(i))) * (MappingToDistribution(float(i)) / InitialValueSum);
    }
	
	// float4 color = localToColor(SpriteTextureSampler, input.TextureCoordinates) * (1.0f / 9.0f);
	// for ( int i = 1; i <= 4; i ++ )
	// {
	// 	color += localToColor(SpriteTextureSampler, input.TextureCoordinates * SIZESURFACE + (float2(iFactorX, iFactorY) * float(i))) * (1.0f / 9.0f);
	// 	color += localToColor(SpriteTextureSampler, input.TextureCoordinates * SIZESURFACE - (float2(iFactorX, iFactorY) * float(i))) * (1.0f / 9.0f);
	// }

    color = clamp(color, float4(0.0, 0.0, 0.0, 0.0), float4(1.0, 1.0, 1.0, 1.0));

    return input.Color * color*light;
}

technique SpriteDrawing
{
    pass P0
    {
        PixelShader = compile PS_SHADERMODEL MainPS();
    }
};