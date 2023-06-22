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

uniform float itype;
uniform float idegree;//程度

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

float4 localToColor(sampler2D samplerTexture, float2 Position)
{
	return tex2D(samplerTexture, Position);
}

float vectorToAngle(float2 vec)
{
    return ((-atan2(vec.y, vec.x) + 2.0 * PI) % 2.0 * PI) - PI;
}

float2 coordinates_to_polar(float2 location, float2 center)
{
	return float2(center.x * 2.0 - length(location), center.y + vectorToAngle(location) / PI * center.y);
}

float2 polar_to_coordinates(float2 polar, float2 center)
{
	float angle = polar.x / center.x * PI;
	return center + float2(cos(angle), sin(angle)) * (polar.y + center.y);
}

float4 MainPS(VertexShaderOutput input) : COLOR
{
	float2 position;
	if ( bool(itype) )
		position = coordinates_to_polar(input.TextureCoordinates * float2(640, 480) - float2(320, 240), float2(320, 240)) / float2(640, 480);
	else
		position = polar_to_coordinates(input.TextureCoordinates * float2(640, 480) - float2(320, 240), float2(320, 240)) / float2(640, 480);
	return input.Color * localToColor(SpriteTextureSampler, lerp(input.TextureCoordinates, position, idegree));
}

technique SpriteDrawing
{
	pass P0
	{
		PixelShader = compile PS_SHADERMODEL MainPS();
	}
};