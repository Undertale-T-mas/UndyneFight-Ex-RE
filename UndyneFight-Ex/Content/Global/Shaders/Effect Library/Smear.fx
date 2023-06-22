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

uniform float2 iVectorStart;//
uniform float2 iVectorEnd;//
uniform float iReach;//进行长度
uniform float iRadius;//影响半径

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

float4 tex2D_edge(sampler2D sampler_index, float2 position)
{
	if ( ( position.x < 0.0 || position.y < 0.0 ) || ( position.x > 1.0 || position.y > 1.0 ) )
		return float4(0.0, 0.0, 0.0, 0.0);

	return tex2D(SpriteTextureSampler, position);
}

//return tex2D(samplerTexture, SIZEPIXEL * Position);

float4 MainPS(VertexShaderOutput input) : COLOR
{
	float2 Position = input.TextureCoordinates * float2(WIDTH, HEIGHT);
	float2 VectorMainNormal = normalize(iVectorEnd - iVectorStart);
	float2 ReallyPoint = iVectorStart + VectorMainNormal * iReach;
	
	float offset_length = iReach * exp(-(pow(length(Position - ReallyPoint), 2.0) / ( 2.0 * pow(iRadius, 2.0) )));

	Position -= offset_length * VectorMainNormal;

	//return input.Color * tex2D(SpriteTextureSampler, Position / float2(WIDTH, HEIGHT));边缘限制 -> 优化
    return input.Color * tex2D_edge(SpriteTextureSampler, Position / float2(WIDTH, HEIGHT));
}

technique SpriteDrawing
{
	pass P0
	{
		PixelShader = compile PS_SHADERMODEL MainPS();
	}
};