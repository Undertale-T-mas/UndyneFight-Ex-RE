#if OPENGL
#define SV_POSITION POSITION
#define VS_SHADERMODEL vs_3_0
#define PS_SHADERMODEL ps_3_0
#else
#define VS_SHADERMODEL vs_4_0_level_9_3
#define PS_SHADERMODEL ps_4_0_level_9_3
#endif

#define WIDTH 640.0
#define HEIGHT 640
#define PI 3.1415926


Texture2D SpriteTexture;
float time = 0;
float intensity;
float length;
float blocksize;
sampler2D SpriteTextureSampler = sampler_state
{
    Texture = <SpriteTexture>;
};
struct VertexShaderOutput
{
    float4 Position : SV_POSITION;
    float4 Color : COLOR0;
    float2 pos : TEXCOORD;
};
float random(float2 pos)
{
    return frac(sin(dot(pos.xy, float2(12.9898, 78.233))) * 43758.5453);
}
float2 GV(float length, float rot)
{
    return float2(cos(rot) * length, sin(rot)*length);
}

float4 MainPS(VertexShaderOutput input) : COLOR
{
    float2 me = float2(input.pos.x+input.pos.y/4,input.pos.y);
    float2 pos = floor((me + 0.5) * blocksize * random(float2(time,time)));
    if (intensity * random(pos) < length)
        return tex2D(SpriteTextureSampler, input.pos);
    return tex2D(SpriteTextureSampler, input.pos + GV(intensity * random(pos)/5, random(pos + time) * 6.28));
}

technique BasicColorDrawing
{
	pass P0
	{
		PixelShader = compile PS_SHADERMODEL MainPS();
	}
};