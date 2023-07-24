#if OPENGL
#define SV_POSITION POSITION
#define VS_SHADERMODEL vs_3_0
#define PS_SHADERMODEL ps_3_0
#else
#define VS_SHADERMODEL vs_4_0_level_9_1
#define PS_SHADERMODEL ps_4_0_level_9_1
#endif

//导入纹理数据
uniform float2 itextureSize; //该表面大小

//导入物体数据
uniform float3 iProjectAxisX, iProjectAxisY, iProjectAxisZ; //物体坐标系
uniform float3 iProjectPoint; //物体在世界坐标系中的位置
uniform float2 iProjectPointOffect; //物体在世界坐标系中的锚点

//导入相机数据
uniform float3 iVisuospatial; //视场
uniform float3 iPosition; //位置
uniform float3 iRotation; //旋转角
uniform float3 iAhead, iRight, iDown; //向量
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

float4 localToColor(sampler2D samplerTexture, float2 Position)
{
    if ((Position.x > 1.0) || (Position.y > 1.0) || (Position.x < 0.0) || (Position.y < 0.0))
        return float4(0.0, 0.0, 0.0, 0.0);
    else
        return tex2D(samplerTexture, Position);
}

float3 vectorRotation(float3 rotVector, float3 forceVector, float angle)
{
    return (rotVector * cos(angle) + cross(rotVector, forceVector) * sin(angle) + forceVector * (dot(forceVector, rotVector) * (1.0 - cos(angle))));
}

float3 breakVector(float3 _vector, float3 normalVectorX, float3 normalVectorY, float3 normalVectorZ)
{
    return float3(dot(_vector, normalVectorX), dot(_vector, normalVectorY), dot(_vector, normalVectorZ));
}

float4 MainPS(VertexShaderOutput input) : COLOR
{
    float2 v_vPosition = itextureSize * input.TextureCoordinates;
    float2 surfaceVector = float2(v_vPosition - iVisuospatial.xy / 2.0);
    float3 cameraVector = surfaceVector.x * iRight + surfaceVector.y * iDown + iVisuospatial.z * iAhead;
    cameraVector = normalize(vectorRotation(cameraVector, iAhead, iRotation.z));
    float4 color = float4(0.0, 0.0, 0.0, 0.0);
	
    float vectorCheck = dot(iProjectAxisZ, cameraVector);
    if (vectorCheck != 0.0)
    {
        float crashLength = -(dot(iProjectAxisZ, iPosition) - dot(iProjectAxisZ, iProjectPoint)) / vectorCheck;
        if (crashLength > 0.0)
        {
            float3 surfaceVector = iPosition + cameraVector * crashLength - iProjectPoint + iProjectAxisX * iProjectPointOffect.x + iProjectAxisY * iProjectPointOffect.y;
            float3 textureVector = breakVector(surfaceVector, iProjectAxisX, iProjectAxisY, iProjectAxisZ);
            color += localToColor(SpriteTextureSampler, textureVector.xy / itextureSize);
        }
    }
    return input.Color * color;
}

technique SpriteDrawing
{
    pass P0
    {
        PixelShader = compile PS_SHADERMODEL MainPS();
    }
};