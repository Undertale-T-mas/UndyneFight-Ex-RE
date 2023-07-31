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

float iFrequency1, iFrequency2, iFrequency3;
float iIntensity2, iIntensity1, iIntensity3;
float iTime; 

float4 MainPS3x(VertexShaderOutput input) : COLOR
{ 
    float y = input.TextureCoordinates.y + iTime;
    float del = sin(y * iFrequency1) * iIntensity1 + sin(y * iFrequency2) * iIntensity2 + sin(y * iFrequency3) * iIntensity3;
    return tex2D(SpriteTextureSampler, input.TextureCoordinates + float2(del, 0));
} 
float4 MainPS2x(VertexShaderOutput input) : COLOR
{ 
    float y = input.TextureCoordinates.y + iTime;
    float del = sin(y * iFrequency1) * iIntensity1 + sin(y * iFrequency2) * iIntensity2;
    return tex2D(SpriteTextureSampler, input.TextureCoordinates + float2(del, 0));
} 
technique SpriteDrawing2x
{
    pass P0
    {
        PixelShader = compile PS_SHADERMODEL MainPS2x();
    }
}
technique SpriteDrawing3x
{
    pass P0
    {
        PixelShader = compile PS_SHADERMODEL MainPS3x();
    }
}