#if OPENGL
	#define SV_POSITION POSITION
	#define VS_SHADERMODEL vs_3_0
	#define PS_SHADERMODEL ps_3_0
#else
	#define VS_SHADERMODEL vs_4_0_level_9_1
	#define PS_SHADERMODEL ps_4_0_level_9_1
#endif

Texture2D SpriteTexture;
float time;
float width;
float count;
float width2;
float count2;
float type;
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
    float si = sin(time + pos.y * count) * (width / 640);
    float si2 = sin(time + pos.x * count2) * (width2 / 640);
    pos.x +=si;
    pos.y +=si2;
    if (type==1)
    {
    
        if (pos.x < si || pos.x > 1 - si)
        {
            return float4(0, 0, 0, 1);
        }
        if (pos.y < si2 || pos.y > 1 - si2)
        {
            return float4(0, 0, 0, 1);
        }
    }
    return tex2D(SpriteTextureSampler, pos) * input.Color;

}

technique SpriteDrawing
{
	pass P0
	{
		PixelShader = compile PS_SHADERMODEL MainPS();
	}
};