#if OPENGL
	#define SV_POSITION POSITION
	#define VS_SHADERMODEL vs_3_0
	#define PS_SHADERMODEL ps_3_0
#else
	#define VS_SHADERMODEL vs_4_0_level_9_1
	#define PS_SHADERMODEL ps_4_0_level_9_1
#endif

matrix WorldViewProjection;
float3 icolor;
float intensity;
float intensity2;
float iy;
sampler2D Sample = sampler_state
{
    Texture = <SpriteTexture>;
};

struct VertexShaderOutput
{
	float4 Position : SV_POSITION;
	float4 Color : COLOR0;
    float2 pos : TEXCOORD;
};

float4 MainPS(VertexShaderOutput input) : COLOR
{
    float y = iy / 480;
    float4 color = tex2D(Sample,input.pos);
    float2 po = input.pos;
    float distance = pow(pow(po.y - y, 2) + pow(po.x-0.5,2),0.5)*intensity2;
    color += min(float4(icolor.x, icolor.y, icolor.z, 1) * distance * intensity, float4(icolor.x,icolor.y,icolor.z,1));
    if (po.y>y+0.5)
    {
        color = float4(0,0,0,1);
    }
    return color;
}

technique BasicColorDrawing
{
	pass P0
	{
		PixelShader = compile PS_SHADERMODEL MainPS();
	}
};