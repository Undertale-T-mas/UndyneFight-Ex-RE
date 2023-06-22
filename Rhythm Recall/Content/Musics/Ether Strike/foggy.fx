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

float distance(float2 a)
{
	return sqrt(a.x * a.x + a.y * a.y);
}
float hp;
float4 MainPS(VertexShaderOutput input) : COLOR
{
	float dist = distance(input.TextureCoordinates - float2(0.5, 0.5));
	float x = dist + (1 - hp) * 0.5;
	float4 add = float4(x, x, x, 0);
	float4 color = tex2D(SpriteTextureSampler, input.TextureCoordinates);
	// color.x = color.x * 0.5;
	return add + color;
	// return color * input.Color;
	// return tex2D(SpriteTextureSampler,input.TextureCoordinates) * input.Color;
}

technique SpriteDrawing
{
	pass P0
	{
		PixelShader = compile PS_SHADERMODEL MainPS();
	}
};