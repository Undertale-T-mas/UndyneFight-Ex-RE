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
float2 distance1;
float2 distance2;
float2 distance3;
float4 MainPS(VertexShaderOutput input) : COLOR
{
	//0,0,0,1为黑色,0.5,0.5为屏幕中央，//Cos(
	float4 A = tex2D(SpriteTextureSampler,input.TextureCoordinates) * input.Color * 0.45;
    float4 B = tex2D(SpriteTextureSampler, input.TextureCoordinates + distance1) * input.Color * 0.25;
	float4 C = tex2D(SpriteTextureSampler, input.TextureCoordinates + distance2) * input.Color * 0.15;
    float4 D = tex2D(SpriteTextureSampler, input.TextureCoordinates + distance3) * input.Color * 0.15;
	return A + B + C + D;
}

technique SpriteDrawing
{
	pass P0
	{
		PixelShader = compile PS_SHADERMODEL MainPS();
	}
};