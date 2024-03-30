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


float centerY ;
float centerX;
float centerX2;
float rot;
float Width ;
float Height ;
float Type;
float size;
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

    float type = Type;
    float4 color = tex2D(SpriteTextureSampler, input.TextureCoordinates);
    float2 delta = input.TextureCoordinates - centre;
    float2 delta2 = float2(delta.x / 3, delta.y / 4);
    float2 centre2 = float2(centerX, centerY);
    float distance = pow(delta2.x * delta2.x + delta2.y * delta2.y, 0.5);
    float2 centerrot = float2((centre2.x - (0 - cos((float) rot) * (centre2.x - delta2.x)) + (0 - sin(float(rot)) * (centre2.y - delta2.y))),
                               centre2.y - (sin(float(rot)) * (centre2.x - delta2.x) + cos(float(rot)) * (centre2.y - delta2.y)));
    if (Type==0)
    {
    
      if (centerrot.x <= centerX + Width * 0.5f && centerrot.x >= centerX - Width * 0.5f &&
        centerrot.y <= centerY + Height * 0.5f && centerrot.y >= centerY - Height * 0.5f)
      {
        if (true)
        {
            //centerX - (sin(rot) * (centerX - delta.x) + cos(rot) * (centerY - delta.y))
            color.r = (1 - color.r);
            color.g = (1 - color.g);
            color.b = (1 - color.b);
        }
        

      }
    }
    if (Type==2)
    {
    
        if (delta.x <= centerX )
        {
            
            //centerX - (sin(rot) * (centerX - delta.x) + cos(rot) * (centerY - delta.y))
                color.r = (1 - color.r);
                color.g = (1 - color.g);
                color.b = (1 - color.b);
            
        

        }
        if ( delta.x >= centerX2)
        {
            
            //centerX - (sin(rot) * (centerX - delta.x) + cos(rot) * (centerY - delta.y))
            color.r = (1 - color.r);
            color.g = (1 - color.g);
            color.b = (1 - color.b);
            
        

        }
       
    }
    if (Type==3)
    {
        if (distance>size)
        {
            color.r = (1 - color.r);
            color.g = (1 - color.g);
            color.b = (1 - color.b);
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