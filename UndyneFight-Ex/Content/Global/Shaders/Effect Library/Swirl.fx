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

//uniform float iRotation;
uniform float iAngle;
uniform float iRadius;
uniform float iRadiusOut;
uniform float iDense;
uniform float iDistort;

#define SIZESURFACE float2(640.0, 480.0)//
#define PI 3.1415926

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

float vectorToAngle(float2 vec)
{
    return (-atan2(vec.y, vec.x) + 2.0 * PI);
}

float4 MainPS(VertexShaderOutput input) : COLOR
{
    float2 vector_center = (input.TextureCoordinates - float2(0.5f, 0.5f)) * SIZESURFACE;
    float v_length = length(vector_center);

	//靠近半径收敛为1的线性函数
    float rate = max(iRadius + iRadiusOut - v_length, 0.0f) / iRadiusOut;
    rate = smoothstep(0.0f, 1.0f, ((v_length < iRadius) ? (v_length / iRadius) : rate));
	
    float filter_1 = rate * smoothstep(-1.0, 1.0, sin((vectorToAngle(vector_center) + tan(v_length / 100.0) * iDistort) * iDense - iAngle));
    float4 color = float4(filter_1, filter_1, filter_1, filter_1 * float(v_length <= iRadius + iRadiusOut));

    return input.Color * color;
}

technique SpriteDrawing
{
    pass P0
    {
        PixelShader = compile PS_SHADERMODEL MainPS();
    }
};