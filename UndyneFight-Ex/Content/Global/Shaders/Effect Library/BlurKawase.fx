/*
#if OPENGL
#define SV_POSITION POSITION
#define VS_SHADERMODEL vs_3_0
#define PS_SHADERMODEL ps_3_0
#else
#define VS_SHADERMODEL vs_4_0_level_9_1
#define PS_SHADERMODEL ps_4_0_level_9_1
#endif

//#define CAMERAHIGH 400.0
#define PI 3.1415926
#define RADIUS 2
#define SIZE (RADIUS * 2 + 1)

Texture2D SpriteTexture;

//uniform float iRotation;
uniform float iVariance;
uniform float iFactorX;
uniform float iFactorY;

#define SIZESURFACE float2(640.0, 480.0)//
#define SIZEPIXEL 1.0 / SIZESURFACE

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

float4 localToColor(sampler2D samplerTexture, float2 Position)
{
    return tex2D(samplerTexture, SIZEPIXEL * Position);
}

float GetInitialValue(float2 vec)
{
    float lenSquare = vec.x * vec.x + vec.y * vec.y;
    if (iVariance < 0.0001f)
        return float(lenSquare < 0.0001f);
    else
    {
        if (lenSquare < 0.0001f)
            return 1.0 / (2.0 * PI * iVariance);
        else
            return (exp(-lenSquare / (2.0 * iVariance))) / (2.0 * PI * iVariance);
    }
} 

float2 vectorRotation(float2 vec, float rot)
{
    return float2(vec.x * cos(rot) + vec.y * sin(rot), -vec.x * sin(rot) + vec.y * cos(rot));
}

float4 MainPS(VertexShaderOutput input) : COLOR
{
    float2 side_1[SIZE];
    float InitialValueSum = 0.0;

    for (int i = 0; i < SIZE; i++)
    {
        side_1[i] = float2(iFactorX, iFactorY) * float(i - RADIUS);
        InitialValueSum += GetInitialValue(side_1[i]);
    }
	
	///////////////////////////////////////////////////////////////////////////////////////////
	//集成所有值
	
	///////////////////////////////////////////////////////////////////////////////////////////
	//将标准率应用到所有格
	///////////////////////////////////////////////////////////////////////////////////////////
	///////////////////////////////////////////////////////////////////////////////////////////
	//获取和收集所有
    float4 color = float4(0.0, 0.0, 0.0, 0.0);
	
    for (i = 0; i < SIZE; i++)//s1
        color += localToColor(SpriteTextureSampler, input.TextureCoordinates * SIZESURFACE + side_1[i]) * (GetInitialValue(side_1[i]) / InitialValueSum);
	
    color = clamp(color, float4(0.0, 0.0, 0.0, 0.0), float4(1.0, 1.0, 1.0, 1.0));

    return input.Color * color;
}

technique SpriteDrawing
{
    pass P0
    {
        PixelShader = compile PS_SHADERMODEL MainPS();
   
    }
}
*/
#if OPENGL
#define SV_POSITION POSITION
#define VS_SHADERMODEL vs_3_0
#define PS_SHADERMODEL ps_3_0
#else
#define VS_SHADERMODEL vs_4_0_level_9_1
#define PS_SHADERMODEL ps_4_0_level_9_1
#endif 

Texture2D SpriteTexture;

//uniform float iRotation;
uniform float2 iDelta; 

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
    float2 pos = input.TextureCoordinates;
    float4 res = float4(0, 0, 0, 0);
    res += tex2D(SpriteTextureSampler, pos + iDelta) * 0.2;
    res += tex2D(SpriteTextureSampler, pos - iDelta) * 0.2;
    float2 del2 = iDelta * float2(-1, 1);
    res += tex2D(SpriteTextureSampler, pos + del2) * 0.2;
    res += tex2D(SpriteTextureSampler, pos - del2) * 0.2;
    res += tex2D(SpriteTextureSampler, pos) * 0.2;
    return input.Color * res;
}

technique SpriteDrawing
{
    pass P0
    {
        PixelShader = compile PS_SHADERMODEL MainPS();
    }
};