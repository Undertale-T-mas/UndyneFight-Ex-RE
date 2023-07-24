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

uniform float iDistort;//扭曲程度
uniform float iTime;//时间
uniform float iHeight;//火焰高度（240）
uniform float iPieceRate;//残渣量（0.1）
uniform float3 iBlend;//内火焰颜色RGB
uniform float3 iBlendEdge;//外火焰颜色RGB
 
sampler2D Sampler0 : register(s0);
sampler2D Sampler1 : register(s1);

struct VertexShaderOutput
{
	float4 Position : SV_POSITION;
	float4 Color : COLOR0;
	float2 TextureCoordinates : TEXCOORD0;
};

float4 localToColor(sampler2D textureSampler, float2 location)//获取表面上一点的颜色值
{
	location = fmod(location, float2(WIDTH, HEIGHT));
	return tex2D(textureSampler, location / float2(WIDTH, HEIGHT));
}

float position_get_noise(float2 Position)
{
    return frac(sin(dot(Position, float2(12.9898, 78.233))) * 43758.5453123);
}

float4 MainPS(VertexShaderOutput input) : COLOR
{
	float2 v_vPosition = input.TextureCoordinates * float2(WIDTH, HEIGHT);
	
    float distort = localToColor(Sampler0, v_vPosition * 0.3149657 + iTime * 0.1) * iDistort;
    float color = localToColor(Sampler1, float2((v_vPosition.x - iTime * 0.21335) + distort, (v_vPosition.y + iTime) + distort));

	float grand = smoothstep(iHeight, HEIGHT, v_vPosition.y);
	color += grand * 0.5;
 
	float alpha = min(1.0, color * grand);
	
	float color_edge = (min(color + iPieceRate, 1.0) - color);
	
	float color_value = color * alpha;
	float color_edge_value = 1.0 - color_edge * (1.0 / iPieceRate);
	
    return input.Color * float4(float3(color_value, color_value, color_value) * iBlend + float3(color_edge_value, color_edge_value, color_edge_value) * iBlendEdge, 1.0);
}

technique SpriteDrawing
{
	pass P0
	{
		PixelShader = compile PS_SHADERMODEL MainPS();
	}
};


