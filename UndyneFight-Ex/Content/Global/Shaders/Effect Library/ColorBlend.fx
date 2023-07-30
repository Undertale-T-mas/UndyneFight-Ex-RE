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
#define DISTORT_SAMPLER float2(12.9898,78.233)
#define NOISE_SAMPLER float2(76.9898,88.463) 
 
sampler2D sampler0 : register(s0);
sampler2D sampler1 : register(s1);

struct VertexShaderOutput
{
	float4 Position : SV_POSITION;
	float4 Color : COLOR0;
	float2 TextureCoordinates : TEXCOORD0;
};  

float4 Test(VertexShaderOutput input) : COLOR
{ 
    return tex2D(sampler0, input.TextureCoordinates) * tex2D(sampler1, input.TextureCoordinates);

}
 
technique SpriteDrawing
{ 
    pass P0
    {
        PixelShader = compile PS_SHADERMODEL Test();
    } 
};