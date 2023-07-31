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
    float y = uvOld.y + iTime;
    int chunk = (int) (y / iChunkHeight);
	
    float del = iIntensity * hash11(chunk);
	
    return input.Color * tex2D(SpriteTextureSampler, float2(del, 0) + uvOld);
}
float4 MainPS2(VertexShaderOutput input) : COLOR
{
    float2 uvOld = input.TextureCoordinates;
    float y = uvOld.y + iTime;
    int chunk = (int) (y / iChunkHeight);
	
    float del = iIntensity * (hash11(chunk) * 2 - 1);
	
    float4 result;
    if (del > 0)
    {
		float4 rg_a = input.Color * tex2D(SpriteTextureSampler, uvOld);
        rg_a.b = input.Color.b * tex2D(SpriteTextureSampler, float2(del, 0) + uvOld).b;
        result = rg_a;
    }
    else
    {
        float4 _gba = input.Color * tex2D(SpriteTextureSampler, uvOld);
        _gba.r = input.Color.r * tex2D(SpriteTextureSampler, float2(del, 0) + uvOld).r;
        result = _gba;
    }
    return result;
}

technique SpriteDrawing
{
	pass P0
	{
		PixelShader = compile PS_SHADERMODEL MainPS();
	}
};
technique SpriteDrawingRGB
{
	pass P0
	{
		PixelShader = compile PS_SHADERMODEL MainPS2();
	}
};