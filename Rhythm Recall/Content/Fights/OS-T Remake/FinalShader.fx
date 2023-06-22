#if OPENGL
	#define SV_POSITION POSITION
	#define VS_SHADERMODEL vs_3_0
	#define PS_SHADERMODEL ps_3_0
#else
	#define VS_SHADERMODEL vs_4_0_level_9_1
	#define PS_SHADERMODEL ps_4_0_level_9_1
#endif

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

float time;
float sinMul0, sinMul1; 
float range;
float height;

float4 MainPS(VertexShaderOutput input) : COLOR
{
    float2 det = input.TextureCoordinates - float2(0.5, 0.39);
    float dist = sqrt(det.x * det.x + det.y * det.y), rot = atan2(det.y, det.x);
    float res = 0.0;
    
    res += sin(1 * time + rot * 32) * sinMul0;
    res += sin(2.2 * time + rot * 50) * sinMul1; 
    
    res = res * res * res;
	
    float final = smoothstep(0, 1, range - dist * (1 + res));
    final = smoothstep(0, 1, 4 * (range - dist + res));
    final = max(7 * (range - dist + res), range * 1.5 - dist);
    //final = range - dist * 1.8 + res * (range + 0.1) / 2;
    return float4(final, final, final, final);
} 


technique SpriteDrawing
{
	pass P0
	{
		PixelShader = compile PS_SHADERMODEL MainPS();
    } 
};