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

Texture2D SpriteTexture;

//uniform float iAngle;ȡ������ת��ƫ��
uniform float2 iRadius; //�뾶, ��Ȧ�뾶��ֵ

uniform float iDense; //�����仯��
uniform float iDistort; //������

#define SIZESURFACE float2(640.0, 480.0)//
#define PI2 6.2831852

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
    float2 vector_center = (input.TextureCoordinates - float2(0.5f, 0.5f)) * SIZESURFACE;
    float v_length = length(vector_center);
	
    float rate_length = saturate((iRadius.x - v_length) / iRadius.y); //����仯
    float rate_angle = PI2 - atan2(vector_center.y, vector_center.x) + tan(v_length / 100.0) * iDistort; //�Ƕȱ仯

    return input.Color * rate_length * (sin(rate_angle * iDense) * 0.5f + 0.5f);
}

technique SpriteDrawing
{
    pass P0
    {
        PixelShader = compile PS_SHADERMODEL MainPS();
    }
};