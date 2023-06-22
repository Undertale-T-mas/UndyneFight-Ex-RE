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

Texture2D SpriteTexture;

uniform float iScale;
uniform float2 iCenter;

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
	float2 local_place = ( iScale * iCenter + (input.TextureCoordinates - iCenter) ) / iScale;
	local_place = frac(1.0 + frac(local_place));
	// local_place *= float2(WIDTH, HEIGHT);
	// local_place = (local_place % float2(WIDTH, HEIGHT)) / float2(WIDTH, HEIGHT);

    return input.Color * tex2D(SpriteTextureSampler, local_place);
}

technique SpriteDrawing
{
	pass P0
	{
		PixelShader = compile PS_SHADERMODEL MainPS();
	}
};