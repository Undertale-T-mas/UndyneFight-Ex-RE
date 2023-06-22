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

float sin1, sin2, time1, time2;
float time3, sin3;

float4 MainPS(VertexShaderOutput input) : COLOR
{
    float v = input.TextureCoordinates.y + input.TextureCoordinates.x * 0.8;
    float del = sin(v * 260 + time1) * sin1 
			  + sin(v * 165 + time2) * sin2
			  + sin(v * 115 + time3) * sin3;
    return tex2D(SpriteTextureSampler, input.TextureCoordinates + float2(del, del * -0.8)) * input.Color;
}

technique SpriteDrawing
{
	pass P0
	{
		PixelShader = compile PS_SHADERMODEL MainPS();
	}
};