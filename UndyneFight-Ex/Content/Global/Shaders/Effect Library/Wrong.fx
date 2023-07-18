#define VS_SHADERMODEL vs_3_0
#define PS_SHADERMODEL ps_3_0

//#define CAMERAHIGH 400.0
#define PI 3.1415926

uniform float iTime;
uniform float iValue;
Texture2D SpriteTexture;

#define SIZESURFACE float2(640.0, 480.0)//
#define SIZEPIXEL 1.0 / SIZESURFACE

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

float4 localToColor(sampler2D samplerTexture, float2 Position)
{
	return tex2D(samplerTexture, SIZEPIXEL * Position);
}

float2 GetRandomF2(float2 Area, float iTime)
{
	return float2(sqrt(abs(cos(sin(Area.x * 9.0 / cos(iTime)) / cos(Area.y * iTime) * iTime))),
		sqrt(abs(sin(cos(Area.x * 16.0 * sin(iTime)) * cos(Area.y * iTime) * iTime))));
}

float4 GetRandomF4(float2 Position, float iTime, float2 Block, float2 Offect)
{
	float2 pos = floor(Position / Block);
	pos += Offect;
	return float4(5.0 * (sin(sqrt(iTime) / 3.0) * cos(iTime * 2.0 / log2(iTime)) * sin(pos.x * pos.y * (iTime % 20.0) / 20.0)),
		5.0 * (sin(sqrt(iTime) / 7.0) * cos(iTime * 2.0 / log2(iTime)) * cos(pos.x * pos.y * (iTime % 5.0) / 20.0)),
		5.0 * (cos(sqrt(iTime) / 12.0) * cos(iTime * 8.0 / log2(iTime)) * sin(pos.x * pos.y * (iTime % 10.0) / 20.0)),
		1.0);
}

float4 MainPS(VertexShaderOutput input) : COLOR
{
	float2 v_vPosition = input.TextureCoordinates * SIZESURFACE;
	float2 Offect = float2(640.0, 480.0);
	float4 clr = abs(GetRandomF4(v_vPosition, iTime, 1.0, Offect));

	float2 BlockArea = floor(v_vPosition / (4.0 + GetRandomF2(float2(35.0, 23.0), iTime) * 12.0));
	float2 BlockInGetRandomF2 = GetRandomF2(BlockArea, iTime) * 100.0;

	float4 moveHigh = GetRandomF4(v_vPosition, iTime, max(float2(1.0, 1.0), BlockInGetRandomF2), Offect);
	float2 move = (float)(clr.x > 0.9) * float2((moveHigh.x + moveHigh.z) / 2.0, (moveHigh.y + moveHigh.x) / 2.0) / 50.0;
	clr.xyz += (float)(clr.x < 0.9);

	float4 effectColor = clr * tex2D(SpriteTextureSampler, input.TextureCoordinates + move);

	return input.Color * lerp(tex2D(SpriteTextureSampler, input.TextureCoordinates), effectColor, iValue);
}

technique SpriteDrawing
{
	pass P0
	{
		PixelShader = compile PS_SHADERMODEL MainPS();
	}
};