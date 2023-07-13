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
#define DISTORT_SAMPLER float2(12.9898,78.233)
#define NOISE_SAMPLER float2(76.9898,88.463)

uniform float2 iOffset;
uniform float2 iWidth;
uniform float2 iScatter;
uniform float2 iSize;

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

float curve_get_value(in float value, in float size, in float scatter, in float width, in float time)
{
    return smoothstep(min(1.0, 1.0 - size), scatter, sin(value * PI / width + time));
}

//return tex2D(samplerTexture, SIZEPIXEL * Position);

float4 MainPS(VertexShaderOutput input) : COLOR
{
	float2 coord_position = input.TextureCoordinates * float2(WIDTH, HEIGHT);
	float value = curve_get_value(coord_position.x, iSize.x, iScatter.x, iWidth.x, iOffset.x) * curve_get_value(coord_position.y, iSize.y, iScatter.y, iWidth.y, iOffset.y);
	
    return input.Color * float4(value, value, value, value);
}

technique SpriteDrawing
{
	pass P0
	{
		PixelShader = compile PS_SHADERMODEL MainPS();
	}
};