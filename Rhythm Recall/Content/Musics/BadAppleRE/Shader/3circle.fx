#if OPENGL
#define SV_POSITION POSITION
#define VS_SHADERMODEL vs_3_0
#define PS_SHADERMODEL ps_3_0
#else
#define VS_SHADERMODEL vs_4_0_level_9_3
#define PS_SHADERMODEL ps_4_0_level_9_3
#endif

#define WIDTH 640.0
#define HEIGHT 640
#define PI 3.1415926


Texture2D SpriteTexture;

sampler2D Sample = sampler_state
{
    Texture = <SpriteTexture>;
};

float intensity1[10];
float intensity2[10];
int count;
float time;
struct VertexShaderOutput
{
    float4 Position : SV_POSITION;
    float4 Color : COLOR0;
    float2 pos : TEXCOORD0;
};

//return tex2D(samplerTexture, SIZEPIXEL * Position);
float2 GV(float len, float rot)
{
    return float2(cos(rot) * len, sin(rot) * len);
}
float len(float2 pos)
{
    return sqrt(pos.x * pos.x + pos.y * pos.y);
}
float2 Aspect(float2 pos)
{
    return float2(pos.x / 3, pos.y / 4);
}
float4 colorset(float t,float t2,float2 pos)
{
    float2 aspect = Aspect(pos - float2(0.5,0.5));
    float rot = atan2(aspect.y,aspect.x);
    float2 mpos = GV(((sin(rot * t2 + time) * t / 15) + (sin(rot * t2/2 - time) * t / 20))/2 + t, rot);
    float distance = len(aspect-mpos)*0.2/t;
    float s = distance*20;
    float4 color = float4(1,1,1,1);
    color -= min(s,1);
    return color;
    
}

float4 MainPS(VertexShaderOutput input) : COLOR
{
    float2 pos = input.pos;
    float4 color = tex2D(Sample,input.pos);
    for (int i = 0; i < count;i++)
        color += colorset(intensity1[i],intensity2[i],input.pos);
    return color;
}

technique SpriteDrawing
{
    pass P0
    {
        PixelShader = compile PS_SHADERMODEL MainPS();
    }
};