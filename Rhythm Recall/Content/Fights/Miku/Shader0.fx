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

float4 Fl4(float fl1)
{
    return float4(fl1, fl1, fl1, fl1);
}
float del = 0;

float4 MainPS(VertexShaderOutput input) : COLOR
{
    float2 pos[3] = { float2(-0.2, 1.2), float2(1.2, 1.2), float2(0.5, -0.3 - del) };
    float idt = 0; 
    idt = max(idt, 0.8 - distance(pos[0], input.TextureCoordinates) * (1.5 - del));
    idt = max(idt, 0.8 - distance(pos[1], input.TextureCoordinates) * (1.5 - del));
    idt = max(idt, 1 - distance(pos[2], input.TextureCoordinates) * 0.95);
    float4 col = Fl4(max(0, idt));
    return (tex2D(SpriteTextureSampler, input.TextureCoordinates) + col) * input.Color;
}

technique SpriteDrawing
{
	pass P0
	{
		PixelShader = compile PS_SHADERMODEL MainPS();
	}
};