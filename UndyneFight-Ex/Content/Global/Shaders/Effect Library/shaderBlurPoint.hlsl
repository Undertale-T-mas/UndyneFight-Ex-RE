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

Texture2D SpriteTexture;

uniform float iScale;//scale缩放
uniform float iLength;//强度与扩散
uniform float2 iPoint;//坐标（相当于640,480的坐标系）

#define SIZESURFACE float2(640.0, 480.0)//

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

float4 MainPS(VertexShaderOutput input) : COLOR
{
	float2 factor = iScale * normalize(input.TextureCoordinates * SIZESURFACE - iPoint);
	float length_rcp = (1.0f / iLength);
	float length_pow = pow(iLength, 2.0f);
	
	float4 color = float4(0.0, 0.0, 0.0, 0.0);
	color += tex2D(SpriteTextureSampler, input.TextureCoordinates - factor * 1.0f) * (length_rcp - 0.5f / length_pow);
	color += tex2D(SpriteTextureSampler, input.TextureCoordinates - factor * 2.0f) * (length_rcp - 1.5f / length_pow);
	color += tex2D(SpriteTextureSampler, input.TextureCoordinates - factor * 3.0f) * (length_rcp - 2.5f / length_pow);
	color += tex2D(SpriteTextureSampler, input.TextureCoordinates - factor * 4.0f) * (length_rcp - 3.5f / length_pow);
	color += tex2D(SpriteTextureSampler, input.TextureCoordinates - factor * 5.0f) * (length_rcp - 4.5f / length_pow);
	color += tex2D(SpriteTextureSampler, input.TextureCoordinates - factor * 6.0f) * (length_rcp - 5.5f / length_pow);
	color += tex2D(SpriteTextureSampler, input.TextureCoordinates - factor * 7.0f) * (length_rcp - 6.5f / length_pow);
	color += tex2D(SpriteTextureSampler, input.TextureCoordinates - factor * 8.0f) * (length_rcp - 7.5f / length_pow);
	color += tex2D(SpriteTextureSampler, input.TextureCoordinates - factor * 9.0f) * (length_rcp - 8.5f / length_pow);
	color += tex2D(SpriteTextureSampler, input.TextureCoordinates - factor * 11.0f) * (length_rcp - 9.5f / length_pow);
	color += tex2D(SpriteTextureSampler, input.TextureCoordinates - factor * 12.0f) * (length_rcp - 11.5f / length_pow);
	color += tex2D(SpriteTextureSampler, input.TextureCoordinates - factor * 13.0f) * (length_rcp - 12.5f / length_pow);
	color += tex2D(SpriteTextureSampler, input.TextureCoordinates - factor * 14.0f) * (length_rcp - 13.5f / length_pow);
	color += tex2D(SpriteTextureSampler, input.TextureCoordinates - factor * 15.0f) * (length_rcp - 14.5f / length_pow);
	color += tex2D(SpriteTextureSampler, input.TextureCoordinates - factor * 16.0f) * (length_rcp - 15.5f / length_pow);

    return input.Color * color;
}

technique SpriteDrawing
{
	pass P0
	{
		PixelShader = compile PS_SHADERMODEL MainPS();
	}
};