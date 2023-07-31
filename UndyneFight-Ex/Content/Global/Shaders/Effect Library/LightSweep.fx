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
// #define TORAD 0.01745329251994329576923690768489

uniform float2 iCenter;
uniform float iDirection;
uniform float iWidth;
uniform float iSweepIntensity;

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

float point_distance(float2 center, float angle, float2 position)
{/*
	float line_k = tan(angle);
	return abs(line_k * position.x - position.y + (center.y - line_k * center.x)) / sqrt(line_k * line_k + 1);*/
    float2 unit = float2(-sin(angle), cos(angle));
    return abs(dot(unit, position - center));
}

//return tex2D(samplerTexture, SIZEPIXEL * Position);

float4 MainPS(VertexShaderOutput input) : COLOR
{
	float2 position = input.TextureCoordinates * float2(WIDTH, HEIGHT);
	//float2 vector_position = position - iCenter;
	float line_distance = point_distance(iCenter, iDirection, position);
	float degree = saturate(iSweepIntensity * exp(-line_distance / iWidth * 8.63378723));

	// degree += smoothstep(1.0, 0.0, position.x / iEdgeThickness) + smoothstep(1.0, 0.0, (WIDTH - position.x) / iEdgeThickness);
	// degree += smoothstep(1.0, 0.0, position.y / iEdgeThickness) + smoothstep(1.0, 0.0, (HEIGHT - position.y) / iEdgeThickness);

	float4 color = float4(degree, degree, degree, 1.0);
    return input.Color * color;
}

technique SpriteDrawing
{
	pass P0
	{
		PixelShader = compile PS_SHADERMODEL MainPS();
	}
};