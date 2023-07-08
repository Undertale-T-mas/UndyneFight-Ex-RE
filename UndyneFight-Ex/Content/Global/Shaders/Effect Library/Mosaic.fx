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

uniform float2 iBlockSize;//马赛克方格大小（单位：pixel）
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

//return tex2D(samplerTexture, SIZEPIXEL * Position);

float4 MainPS(VertexShaderOutput input) : COLOR
{
	float2 screen_size = float2(WIDTH, HEIGHT);
	float2 position = input.TextureCoordinates * screen_size;
	float2 mosaic_position = floor(position / iBlockSize) * iBlockSize;
	float4 color = tex2D(SpriteTextureSampler, mosaic_position / screen_size);
	
    return input.Color * color;
}

technique SpriteDrawing
{
	pass P0
	{
		PixelShader = compile PS_SHADERMODEL MainPS();
	}
};