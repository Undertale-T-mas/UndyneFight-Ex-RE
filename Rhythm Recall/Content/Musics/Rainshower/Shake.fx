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
float frequency;
float time;
float range;
float2 distance;
float frequency2;
float range2;
float time2;
float4 MainPS(VertexShaderOutput input) : COLOR
{
	float j = range / 640;
	float j2 = range2 / 640;
	float2 i = float2(cos(input.TextureCoordinates.y * frequency + time) * j + cos(input.TextureCoordinates.y * frequency2 + time2) * j2, 0);
	float a = (tex2D(SpriteTextureSampler, input.TextureCoordinates + i + distance) * input.Color).x;
	float d = (tex2D(SpriteTextureSampler, input.TextureCoordinates + i - distance) * input.Color).y;
	float c = (tex2D(SpriteTextureSampler, input.TextureCoordinates + i) * input.Color).z;
	return float4(a,d,c, 1);
}
technique SpriteDrawing
{
	pass P0
	{
		PixelShader = compile PS_SHADERMODEL MainPS();
	}
};

