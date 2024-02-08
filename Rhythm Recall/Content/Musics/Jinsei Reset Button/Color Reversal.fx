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

float time;
float4 MainPS(VertexShaderOutput input) : COLOR
{
	//return tex2D(SpriteTextureSampler,input.TextureCoordinates) * input.Color;
    float a = 1 - (tex2D(SpriteTextureSampler, input.TextureCoordinates) * input.Color).x;
    float b = 1 - (tex2D(SpriteTextureSampler, input.TextureCoordinates) * input.Color).y;
    float c = 1 - (tex2D(SpriteTextureSampler, input.TextureCoordinates) * input.Color).z;
    return float4(a, b, c, 1);
}

technique SpriteDrawing
{
	pass P0
	{
		PixelShader = compile PS_SHADERMODEL MainPS();
	}
};