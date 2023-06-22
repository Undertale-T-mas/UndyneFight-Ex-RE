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

#define PI 3.141592653589793240

float r1;
float2 r2;

float Random2DTo1D(float2 value, float a, float2 b)
{
	//avaoid artifacts
    float2 smallValue = sin(value);
	//get scalar value from 2d vector	
    float random = dot(smallValue, b);
    random = frac(sin(random) * a);
    return random;
}

float4 MainPS(VertexShaderOutput input) : COLOR
{
    float v = Random2DTo1D(input.TextureCoordinates, r1, r2) * 0.24f - 0.12f;
    return float4(v, v, v, v) + input.Color * tex2D(SpriteTextureSampler, input.TextureCoordinates);
}

technique SpriteDrawing
{
	pass P0
	{
		PixelShader = compile PS_SHADERMODEL MainPS();
	}
};