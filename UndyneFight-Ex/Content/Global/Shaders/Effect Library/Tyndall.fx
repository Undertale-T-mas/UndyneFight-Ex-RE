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

uniform float iLightPosY;
uniform float iLightPosX;
uniform float iDistance;
uniform float iSampling; //采样率建议(1.0)

#define amount 20.0
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

float4 MainPS(VertexShaderOutput input) : COLOR
{
    float2 v_vPosition = input.TextureCoordinates * SIZESURFACE;
    float2 direction = normalize(float2(iLightPosX, iLightPosY) - v_vPosition);
    float2 current_step = v_vPosition;
	
    float3 total = float3(0.0, 0.0, 0.0);
    for (int i = 0; i < int(amount); i++)
    {
        float3 result = localToColor(SpriteTextureSampler, current_step).xyz;
        result = smoothstep(0.0, 1.0, result); //blur
        
        total += result;
        current_step -= direction * iDistance;
    }
    
    total /= amount * iSampling;
	
    float4 original = localToColor(SpriteTextureSampler, v_vPosition); //blur
    float3 over = float3(pow(original.x, 0.5), pow(original.y, 0.5), pow(original.z, 0.5)); //blur
    total = lerp(original.xyz, over, total * 2.0); //blur

    return input.Color * float4(total.x, total.y, total.z, 1.0);
}

technique SpriteDrawing
{
    pass P0
    {
        PixelShader = compile PS_SHADERMODEL MainPS();
    }
};