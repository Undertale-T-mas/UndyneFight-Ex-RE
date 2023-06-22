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
#define RADIUS 3

Texture2D SpriteTexture;

//uniform float iRotation;
uniform int iRadius;
uniform float iFactorX;
uniform float iFactorY;

#define SIZESURFACE float2(640.0, 480.0)//
#define SIZEPIXEL 1.0 / SIZESURFACE

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

float4 localToColor(sampler2D samplerTexture, float2 Position)
{
	return tex2D(samplerTexture, SIZEPIXEL * Position);
}

float MappingSmoothstep(float step)
{
    float radius = float(iRadius);
    return smoothstep(0.0f, (1.0f / radius), max((1.0f / radius) - abs(step / pow(radius, 2.0f)), 0.0f));
}

float4 MainPS(VertexShaderOutput input) : COLOR
{
	float4 color = float4(0.0, 0.0, 0.0, 0.0);
	for ( float i = 1.0f; i <= float(RADIUS); i += 1.0f )
	{
		color += localToColor(SpriteTextureSampler, input.TextureCoordinates * SIZESURFACE + (float2(iFactorX, iFactorY) * i)) * ((MappingSmoothstep(i) + MappingSmoothstep(i - 1.0f)) / 2.0f);
		color += localToColor(SpriteTextureSampler, input.TextureCoordinates * SIZESURFACE - (float2(iFactorX, iFactorY) * i)) * ((MappingSmoothstep(i) + MappingSmoothstep(i - 1.0f)) / 2.0f);
	}

    return input.Color * clamp(color, float4(0.0, 0.0, 0.0, 0.0), float4(1.0, 1.0, 1.0, 1.0));
}

technique SpriteDrawing
{
	pass P0
	{
		PixelShader = compile PS_SHADERMODEL MainPS();
	}
};