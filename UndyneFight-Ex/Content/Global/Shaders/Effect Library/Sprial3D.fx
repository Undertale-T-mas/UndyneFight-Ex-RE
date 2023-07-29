#if OPENGL
	#define SV_POSITION POSITION
	#define VS_SHADERMODEL vs_3_0
	#define PS_SHADERMODEL ps_3_0
#else
	#define VS_SHADERMODEL vs_4_0_level_9_3
	#define PS_SHADERMODEL ps_4_0_level_9_3
#endif

//#define CAMERAHIGH 400.0
#define PI 3.1415926

Texture2D SpriteTexture;

//uniform float iRotation;
uniform float iTime;
uniform float iUnit;

#define SIZESURFACE float2(640.0, 480.0)//
#define SIZEPIXEL 1.0 / SIZESURFACE
#define CAMERAHIGH 400.0
#define amount 16.0

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
    if ((Position.x > SIZESURFACE.x || Position.x < 0.0) || (Position.y > SIZESURFACE.y || Position.y < 0.0))
        return float4(0.0, 0.0, 0.0, 0.0);
    else
        return tex2D(samplerTexture, SIZEPIXEL * Position); //SpriteTextureSampler
}

float3 GetPosition(float z)
{
    return float3(sin(z / 100.0) * 20.0, cos(z / 100.0) * 20.0, z);
}

float4 MainPS(VertexShaderOutput input) : COLOR
{
    float2 v_vPosition = input.TextureCoordinates * SIZESURFACE;
    float2 iSize = SIZESURFACE;
    float2 position = v_vPosition - iSize / 2.0;
    float3 Vector = float3(position.x, position.y, CAMERAHIGH) / CAMERAHIGH;

    float3 clr = float3(0.0, 0.0, 0.0);

    for (int i = 0; i < int(amount); i++)
    {
        float depth = (iTime * 0.62 + (i - iTime * 0.1) * iUnit);
        float3 surCenter = GetPosition(depth) - GetPosition(iTime * 0.62);
        float3 vecInsur = Vector * (i * iUnit);
        float2 vecInText = (vecInsur - surCenter).xy + iSize / 2.0;
        float3 color = localToColor(SpriteTextureSampler, vecInText).xyz;
        clr += smoothstep(float3(0.0, 0.0, 0.0), float3(1.0, 1.0, 1.0), color);
    }
     
    clr /= (amount * 0.75);

    float4 original = localToColor(SpriteTextureSampler, v_vPosition); //blur
    float3 over = float3(pow(original.x, 0.5), pow(original.y, 0.5), pow(original.z, 0.5)); //blur
    clr = lerp(original.xyz, clr, 0.8); //blur

    return input.Color * float4(clr.xyz, 1.0);
}

technique SpriteDrawing
{
    pass P0
    {
        PixelShader = compile PS_SHADERMODEL MainPS();
    }
};