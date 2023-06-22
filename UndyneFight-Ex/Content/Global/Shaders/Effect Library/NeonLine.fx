#if OPENGL
	#define SV_POSITION POSITION
	#define VS_SHADERMODEL vs_3_0
	#define PS_SHADERMODEL ps_3_0
#else
	#define VS_SHADERMODEL vs_4_0_level_9_1
	#define PS_SHADERMODEL ps_4_0_level_9_1
#endif

uniform float4 maincolor;//渲染的主色调(BELEND)
uniform float1 maintime; 

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

float4 NeonlineGetColor(float2 _xy, float1 time, float4 using_color)
{
    float1 pointvalue = (sin(( (_xy.y + time / 250.0) * 103.0 + time / 90.0) * 4.0) *
						sin((_xy.y * 64.0 - time / 50.0 ) * 2.0) - 
						sin(( (_xy.y + time / 450.0) * 52.0 + time / 70.0) * 3.0) -
						cos((_xy.y * 49.0 - time / 30.0 ) * 3.0)
						) * 0.6 + 0.1;
	
    float scale = max(0, min(1, pointvalue));
	
    return (using_color * float4(scale, scale, scale, scale));
}

float4 MainPS(VertexShaderOutput input) : COLOR
{
    float4 color = tex2D(SpriteTextureSampler, input.TextureCoordinates);

    color += NeonlineGetColor(input.TextureCoordinates, maintime, maincolor);

    return color;
    
}

technique SpriteDrawing
{
	pass P0
	{
		PixelShader = compile PS_SHADERMODEL MainPS();
	}
};


