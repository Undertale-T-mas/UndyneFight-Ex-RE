#if OPENGL
	#define SV_POSITION POSITION
	#define VS_SHADERMODEL vs_3_0
	#define PS_SHADERMODEL ps_3_0
#else
	#define VS_SHADERMODEL vs_4_0_level_9_1
	#define PS_SHADERMODEL ps_4_0_level_9_1
#endif
  
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

float iChunkHeight;
float iIntensity;
float iTime;

float hash11(float p)
{
    p = frac(p * 230.1031);
    p *= p + 33.33;
    p *= p + p;
    return frac(p);
}

float4 MainPS(VertexShaderOutput input) : COLOR
{
    float2 uvOld = input.TextureCoordinates;
    float y = uvOld.y;
    int chunk = (int) (y / iChunkHeight);
	
    float del = iIntensity * hash11(chunk);
	
    return input.Color * tex2D(SpriteTextureSampler, del);
}

technique SpriteDrawing
{
	pass P0
	{
		PixelShader = compile PS_SHADERMODEL MainPS();
	}
};