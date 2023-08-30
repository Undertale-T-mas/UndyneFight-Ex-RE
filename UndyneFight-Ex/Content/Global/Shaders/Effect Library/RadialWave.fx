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

uniform float2 iCenter;//中心
uniform float iRadius;//半径范围
uniform float iProgress;//在半径内播放动画的rate，该值在0-1之间

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

//return tex2D(samplerTexture, SIZEPIXEL * Position);

float mapping(float vector_length, float progress, float radius)
{
	progress = max(progress, 0.0001);
	float radius_value = radius * 0.368626;
	float progress_sqrt = sqrt(progress);
	return min((sin(clamp((vector_length - progress * radius) / radius_value + 1.0, -1.0, 1.0) * 1.57) / 2.0 + 0.5), 1.0) * 
				atan(vector_length / 30.0) * (pow(progress, 1.0 / 6.0) * (1.0 - progress_sqrt)) / (radius * progress_sqrt) * vector_length * (radius * progress_sqrt - vector_length);
}

float4 MainPS(VertexShaderOutput input) : COLOR
{
	float2 v_vPosition = input.TextureCoordinates * float2(WIDTH, HEIGHT);
	float2 vector_center = v_vPosition - iCenter;
	float2 vector_normal = normalize(vector_center);
	float vector_length = length(vector_center);
	
	float offset = max(0.0, mapping(vector_length, iProgress, iRadius));
	offset = min(offset, vector_length);
	float2 vector_result = v_vPosition - offset * vector_normal;
	vector_result /= float2(WIDTH, HEIGHT);

    return input.Color * tex2D(SpriteTextureSampler, vector_result);
}

technique SpriteDrawing
{
	pass P0
	{
		PixelShader = compile PS_SHADERMODEL MainPS();
	}
};