#if OPENGL
	#define SV_POSITION POSITION
	#define VS_SHADERMODEL vs_3_0
	#define PS_SHADERMODEL ps_3_0
#else
	#define VS_SHADERMODEL vs_4_0_level_9_1
	#define PS_SHADERMODEL ps_4_0_level_9_1
#endif

matrix WorldViewProjection;
sampler2D Sample = sampler_state
{
    Texture = <SpriteTexture>;
};

float time;
float intensity;
float flush;
float flash2;
float count;
struct VertexShaderOutput
{
	float4 Position : SV_POSITION;
	float4 Color : COLOR0;
    float2 pos : TEXCOORD;
};

float4 MainPS(VertexShaderOutput input) : COLOR
{
    float4 col = tex2D(Sample,input.pos);
    float ipos = 1+sin(input.pos.y*count+time)*intensity;
    float y = 0.5;
    col += abs(input.pos.x-0.5)*ipos*flush;
    return max(col, tex2D(Sample, input.pos));
}

technique BasicColorDrawing
{
	pass P0
	{
		PixelShader = compile PS_SHADERMODEL MainPS();
	}
};