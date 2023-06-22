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
uniform float2 iSize;
//textureData = float4(0.0, 0.0, 1.0, 1.0);
//iSize = float2(640, 480);
extern float3x3 GX = 
{
	-1.0, 0.0, 1.0,
	-2.0, 0.0, 2.0,
	-1.0, 0.0, 1.0
};
extern float3x3 GY = 
{
	-1.0, -2.0, -1.0,
	0.0, 0.0, 0.0,
	1.0, 2.0, 1.0
};

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

float grayscale(float4 clr)
{
	return (clr.x + clr.y + clr.z) / 3.0;
}

float localToGray(float2 location)
{
	return grayscale(localToColor(SpriteTextureSampler, textureData, location, iSize));
}

float convolutionMat(float3x3 G, float3x3 C)
{
	return dot(G[0], C[0]) + dot(G[1], C[1]) + dot(G[2], C[2]);
}

float4 MainPS(VertexShaderOutput input) : COLOR
{
	float2 v_vPosition = input.TextureCoordinates * iSize;
	float3x3 convolutionKernel = 
	{
		localToGray(v_vPosition + float2(-1.0, -1.0)), localToGray(v_vPosition + float2(0.0, -1.0)), localToGray(v_vPosition + float2(1.0, -1.0)),
		localToGray(v_vPosition + float2(-1.0,  0.0)), localToGray(v_vPosition + float2(0.0,  0.0)), localToGray(v_vPosition + float2(1.0,  0.0)),
		localToGray(v_vPosition + float2(-1.0,  1.0)), localToGray(v_vPosition + float2(0.0,  1.0)), localToGray(v_vPosition + float2(1.0,  1.0))
	};
	float3 p = float3(length(float2(convolutionMat(convolutionKernel, GX), convolutionMat(convolutionKernel, GY))), 0, 0);
	return input.Color * float4(p, tex2D(SpriteTextureSampler, input.TextureCoordinates).w);
}

technique SpriteDrawing
{
	pass P0
	{
		PixelShader = compile PS_SHADERMODEL MainPS();
	}
};







