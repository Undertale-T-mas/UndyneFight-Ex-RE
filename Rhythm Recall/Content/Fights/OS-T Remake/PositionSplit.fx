#if OPENGL
	#define SV_POSITION POSITION
	#define VS_SHADERMODEL vs_3_0
	#define PS_SHADERMODEL ps_3_0
#else
	#define VS_SHADERMODEL vs_4_0_level_9_1
	#define PS_SHADERMODEL ps_4_0_level_9_1
#endif

Texture2D SpriteTexture;
float splitWidth;

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
    if (splitWidth <= 0)
        return tex2D(SpriteTextureSampler, pos) * input.Color;
    float dist1 = abs(pos.x - 0.5), dist2 = abs(pos.y - 0.5);
    if (dist1 < splitWidth / 640 || dist2 < splitWidth / 480)
        return float4(0, 0, 0, 1);
    if (pos.x < 0.5 && pos.y < 0.5)
    {
        pos.x /= (320 - splitWidth) / 320;
        pos.y /= (240 - splitWidth) / 240;
    }
    else if (pos.x > 0.5 && pos.y < 0.5)
    {
        pos.x = 1 - (1 - pos.x) / ((320 - splitWidth) / 320);
        pos.y /= (240 - splitWidth) / 240;
    }
    else if (pos.x < 0.5 && pos.y > 0.5)
    {
        pos.x /= (320 - splitWidth) / 320;
        pos.y = 1 - (1 - pos.y) / ((240 - splitWidth) / 240);
    }
    else 
    {
        pos.x = 1 - (1 - pos.x) / ((320 - splitWidth) / 320);
        pos.y = 1 - (1 - pos.y) / ((240 - splitWidth) / 240);
    }
	float4 v = tex2D(SpriteTextureSampler, pos) * input.Color;
    return v;
}

technique SpriteDrawing
{
	pass P0
	{
		PixelShader = compile PS_SHADERMODEL MainPS();
	}
};