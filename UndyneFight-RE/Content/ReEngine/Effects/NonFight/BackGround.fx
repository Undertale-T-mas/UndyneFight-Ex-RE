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

uniform float iTime;
uniform float2 iDelta;

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

float noise_create(float2 Coordinates, float time)
{
    float2 pos = Coordinates.xy - iDelta;
	float2 sinVals = sin(float2(time, time * 1.2)) / 16.0;
	float2 cir = ((pos * pos + sin(Coordinates * float2(18.0, 7.0)) / 25.0) + Coordinates * sinVals);
	float circles = sqrt(abs(cir.x + cir.y * 0.5) * 25.0) * 5.0;
	return abs(sin(circles - 1.0) - sin(circles));
}

float4 MainPS(VertexShaderOutput input) : COLOR
{
	//float4 color = tex2D(SpriteTextureSampler, input.TextureCoordinates);
    float noise = input.Color.a > 0.95 ? 1 :
    noise_create(input.TextureCoordinates, iTime);
    return tex2D(SpriteTextureSampler, input.TextureCoordinates) * float4(noise, noise, noise, 1.0) * input.Color;
}

technique SpriteDrawing
{
	pass P0
	{
		PixelShader = compile PS_SHADERMODEL MainPS();
	}
};