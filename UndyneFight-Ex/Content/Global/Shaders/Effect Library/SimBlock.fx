#if OPENGL
	#define SV_POSITION POSITION
	#define VS_SHADERMODEL vs_3_0
	#define PS_SHADERMODEL ps_3_0
#else
	#define VS_SHADERMODEL vs_4_0_level_9_1
	#define PS_SHADERMODEL ps_4_0_level_9_1
#endif

#define ACCURACY 32
#define PI 3.1415926

Texture2D SpriteTexture;
uniform float4 textureData;//纹理数据UV坐标系（左上角X， 左上角Y， 宽， 高）示例（0f, 0f, 1f, 1f）
uniform float iDegree;
uniform float2 iSize;

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

float4 localToColor(sampler2D textureSampler, float4 textureData, float2 location, float2 screenSize)//获取表面上一点的颜色值
{
	return tex2D(textureSampler, float2(textureData.x + textureData.z * ( location.x / screenSize.x ), 
										textureData.y + textureData.w * ( location.y / screenSize.y )));
}

bool isEmpty(float4 color)
{
	return ( ( color.x + color.y + color.z == 0 ) || ( color.w == 0.0 ) );
}

float2 vec2Rotation(float2 vec, float ang)
{
	return float2(vec.x * cos(ang) - vec.y * sin(ang), vec.x * sin(ang) + vec.y * cos(ang));
}

float4 MainPS(VertexShaderOutput input) : COLOR
{
	float4 color = localToColor(SpriteTextureSampler, textureData, input.TextureCoordinates * iSize, iSize);
	if (!isEmpty(color))
	{
		float2 checkPosition = float2(1.0, 0.0);
		[unroll(32)] for (int i = 0; i < ACCURACY; i += 1)
		{
			for (float j = 0.; j < 5.0; j += 1.0)
				if (isEmpty(localToColor(SpriteTextureSampler, textureData, input.TextureCoordinates * iSize + checkPosition * j, iSize)))
				{
					color.w = 0.0;
					break;
				}
			checkPosition = vec2Rotation(checkPosition, PI / (float(ACCURACY) / 2.0));
		}
	}
	return color * input.Color;
}
technique SpriteDrawing
{
	pass P0
	{
		PixelShader = compile PS_SHADERMODEL MainPS();
	}
};


