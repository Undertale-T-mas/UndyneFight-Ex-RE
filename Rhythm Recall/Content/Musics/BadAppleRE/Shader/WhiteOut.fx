#if OPENGL
#define SV_POSITION POSITION
#define VS_SHADERMODEL vs_3_0
#define PS_SHADERMODEL ps_3_0
#else
#define VS_SHADERMODEL vs_4_0_level_9_3
#define PS_SHADERMODEL ps_4_0_level_9_3
#endif

#define WIDTH 640.0
#define HEIGHT 640
#define PI 3.1415926


Texture2D SpriteTexture;

sampler2D SpriteTextureSampler = sampler_state
{
    Texture = <SpriteTexture>;
};

float set;
struct VertexShaderOutput
{
    float4 Position : SV_POSITION;
    float4 Color : COLOR0;
    float2 TextureCoordinates : TEXCOORD0;
};

//return tex2D(samplerTexture, SIZEPIXEL * Position);

float4 MainPS(VertexShaderOutput input) : COLOR
{
    float2 centre = float2(0.5, 0.5);
   
	
    float4 color = tex2D(SpriteTextureSampler, input.TextureCoordinates);
    float2 delta = input.TextureCoordinates - centre;
   
    
   
            //centerX - (sin(rot) * (centerX - delta.x) + cos(rot) * (centerY - delta.y))
    color += set;
            
        
        

    
   
    return input.Color * color;
}

technique SpriteDrawing
{
    pass P0
    {
        PixelShader = compile PS_SHADERMODEL MainPS();
    }
};