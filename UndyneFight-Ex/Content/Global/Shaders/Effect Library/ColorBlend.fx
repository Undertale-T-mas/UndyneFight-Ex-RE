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

uniform float iSpeedX, iSpeedY;
uniform float2 iCoreSpeed;
uniform float iTime;
float4 iColorA, iColorB;
 
sampler2D SpriteTexture : register(s0);
sampler2D hashSample : register(s0);

struct VertexShaderOutput
{
	float4 Position : SV_POSITION;
	float4 Color : COLOR0;
	float2 TextureCoordinates : TEXCOORD0;
};  

float4 Test(VertexShaderOutput input) : COLOR
{
    return tex2D(hashSample, input.TextureCoordinates);
}

float rand(float2 n)
{
    return tex2D(hashSample, frac(n)).r;
}
float __noise(float2 p)
{ /*    vec2 ip = floor(p);
    vec2 u = fract(p);
    u = u*u*(3.0-2.0*u);

    float res = mix(
        mix(rand(ip),rand(ip+vec2(1.0,0.0)),u.x),
        mix(rand(ip+vec2(0.0,1.0)),rand(ip+vec2(1.0,1.0)),u.x),u.y);
    return res*res;*/
    return rand(p);
    float2 ip = floor(p);
    float2 u = p;
    u = u * u * (3 - 2 * u);
    float res = lerp(
        lerp(rand(ip), rand(ip + float2(1, 0)), u.x),
        lerp(rand(ip + float2(0, 1)), rand(ip + float2(1, 1)), u.x),
        u.y
    );
    return res * res;

}
float4 MainPS1(VertexShaderOutput input) : COLOR
{ 
    return rand(input.TextureCoordinates) + 0.1;
    float2 p = input.TextureCoordinates * float2(11, 7);
    float a = __noise(p + float2(iTime * iSpeedX - p.x - p.y, 0));
    float b = __noise(p - float2(iTime * iSpeedY, 0));
    return float4(a, b, 1.0, 1.0);
}
float4 MainPS3(VertexShaderOutput input) : COLOR
{
    float2 p = input.TextureCoordinates * float2(11, 7);
    float a = input.Color.x;
    float b = input.Color.y;
    float v = __noise(p + float2(a, b) - iCoreSpeed * iTime);
    v = clamp(v, 0, 1);
    float4 c = iColorA * (1 - v) + iColorB * v;
    return float4(0, 0, 0, 0);
    //  float3 c = lerp(iColorA, iColorB, noise(p + float2(a, b) - iCoreSpeed * iTime));
    return c;
}

technique SpriteDrawing
{ 
    pass P0
    {
        PixelShader = compile PS_SHADERMODEL Test();
    } 
};