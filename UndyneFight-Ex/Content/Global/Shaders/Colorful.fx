#if OPENGL
	#define SV_POSITION POSITION
	#define VS_SHADERMODEL vs_3_0
	#define PS_SHADERMODEL ps_3_0
#else
	#define VS_SHADERMODEL vs_4_0_level_9_1
	#define PS_SHADERMODEL ps_4_0_level_9_1
#endif

Texture2D SpriteTexture;

float4 colors[4];

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
    float4 color = float4(0, 0, 0, 0);
    float weight[4];
    
    weight[0] = (1.415 - distance(input.TextureCoordinates, float2(0, 0))) / 1.415;
    weight[1] = (1.415 - distance(input.TextureCoordinates, float2(0, 1))) / 1.415;
    weight[2] = (1.415 - distance(input.TextureCoordinates, float2(1, 0))) / 1.415;
    weight[3] = (1.415 - distance(input.TextureCoordinates, float2(1, 1))) / 1.415;
    
    float tot = weight[0] + weight[1] + weight[2] + weight[3];
    
    for (int i = 0; i <= 3; i++)
    {
        color.r += weight[i] * colors[i].r / tot;
        color.g += weight[i] * colors[i].g / tot;
        color.b += weight[i] * colors[i].b / tot;
    }
    color.a = tex2D(SpriteTextureSampler, input.TextureCoordinates).a;
    return tex2D(SpriteTextureSampler, input.TextureCoordinates) * input.Color * color;
}

technique SpriteDrawing
{
	pass P0
	{
		PixelShader = compile PS_SHADERMODEL MainPS();
	}
};