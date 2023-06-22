#if OPENGL
	#define SV_POSITION POSITION
	#define VS_SHADERMODEL vs_3_0
	#define PS_SHADERMODEL ps_3_0
#else
	#define VS_SHADERMODEL vs_4_0_level_9_1
	#define PS_SHADERMODEL ps_4_0_level_9_1
#endif
//x=down, y=left, z=up, w=right
float4 boundDistance;
float4 mixColor;
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
    float scale = 0;
    float2 pos = input.TextureCoordinates * float2(640, 480);
    scale = max(scale, max(max(
    (boundDistance.x - (480 - pos.y)) / boundDistance.x,
    (boundDistance.y - pos.x) / boundDistance.y), max(
    (boundDistance.z - pos.y) / boundDistance.z,
    (boundDistance.w - (640 - pos.x)) / boundDistance.w)));
    return tex2D(SpriteTextureSampler, input.TextureCoordinates) * input.Color + mixColor * float4(scale, scale, scale, scale);
}

technique SpriteDrawing
{
	pass P0
	{
		PixelShader = compile PS_SHADERMODEL MainPS();
	}
};