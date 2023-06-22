#if OPENGL
	#define SV_POSITION POSITION
	#define VS_SHADERMODEL vs_3_0
	#define PS_SHADERMODEL ps_3_0
#else
	#define VS_SHADERMODEL vs_4_0_level_9_1
	#define PS_SHADERMODEL ps_4_0_level_9_1
#endif

uniform float2 iCenter;
uniform float iRadius;
uniform float iProgress;

#define PI 3.1415926

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

float4 MainPS(VertexShaderOutput input) : COLOR
{
	float2 center_vector = input.TextureCoordinates * float2(640.0, 480.0) - iCenter;
	float vector_length = length(center_vector);
	float sus_radius = (vector_length - (iProgress * iRadius)) / 84.9294;
	float rate = max(0.0, 28.6983 * log(sus_radius * (5.4373 - iProgress * 5.4372)) / (sus_radius / (smoothstep(0.0001, 1.0, iProgress) * 1.5966)));
	float2 place = input.TextureCoordinates * float2(640.0, 480.0) + normalize(center_vector) * rate * smoothstep(1.0, 0.0, iProgress);

	return input.Color * tex2D(SpriteTextureSampler, place / float2(640.0, 480.0));
}

technique SpriteDrawing
{
	pass P0
	{
		PixelShader = compile PS_SHADERMODEL MainPS();
	}
};