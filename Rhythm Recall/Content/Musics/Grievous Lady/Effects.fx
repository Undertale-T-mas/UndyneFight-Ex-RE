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
float2 distance = (0, 0);
float alpha = 0;
float4 MainPS(VertexShaderOutput input) : COLOR
{
    float r = (tex2D(SpriteTextureSampler, input.TextureCoordinates + distance) * input.Color).x;
	float g = (tex2D(SpriteTextureSampler, input.TextureCoordinates - distance) * input.Color).y;
	float b = (tex2D(SpriteTextureSampler, input.TextureCoordinates ) * input.Color).z;
	return float4(r,g,b,1);
}
technique SpriteDrawing
{
	pass P0
	{
		PixelShader = compile PS_SHADERMODEL MainPS();
	}
};