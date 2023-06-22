#if OPENGL
	#define SV_POSITION POSITION
	#define VS_SHADERMODEL vs_3_0
	#define PS_SHADERMODEL ps_3_0
#else
	#define VS_SHADERMODEL vs_4_0_level_9_1
	#define PS_SHADERMODEL ps_4_0_level_9_1
#endif 

#define SIZESURFACE float2(640.0, 480.0)//
#define SIZEPIXEL 1.0 / SIZESURFACE

float intensity;
float ratio;
float time;

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
    float x = input.TextureCoordinates.x; 
    float y = input.TextureCoordinates.y;
    float4 col = tex2D(SpriteTextureSampler, input.TextureCoordinates);
    float hashX = (frac(sin(y * 12351 + time) * 9875 - (y + 1) * 12351 * (x + 1)));
    if (hashX > ratio)
        return col;
	
    float hash = (frac(sin(y * 123 + time) * (-820 + time) + y * 100 * y * 1286)) * intensity / 640;
    float4 colR = tex2D(SpriteTextureSampler, input.TextureCoordinates - float2(hash, 0));
    float4 colB = tex2D(SpriteTextureSampler, input.TextureCoordinates + float2(hash, 0));
    return float4(colR.x, col.y, colB.z, col.w);
}

technique SpriteDrawing
{
	pass P0
	{
		PixelShader = compile PS_SHADERMODEL MainPS();
	}
};