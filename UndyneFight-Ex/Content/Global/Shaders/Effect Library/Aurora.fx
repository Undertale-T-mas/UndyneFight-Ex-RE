#if OPENGL
#define SV_POSITION POSITION
#define VS_SHADERMODEL vs_3_0
#define PS_SHADERMODEL ps_3_0
#else
#define VS_SHADERMODEL vs_4_0_level_9_1
#define PS_SHADERMODEL ps_4_0_level_9_1
#endif 

Texture2D SpriteTexture;
Texture2D hashMap;

//uniform float iRotation;
uniform float3 iRGB;
uniform float iTime; 


sampler2D SpriteTextureSampler = sampler_state
{
    Texture = <SpriteTexture>;
};
sampler2D hashSample = sampler_state
{
    Texture = <hashMap>;
};

struct VertexShaderOutput
{
    float4 Position : SV_POSITION;
    float4 Color : COLOR0;
    float2 TextureCoordinates : TEXCOORD0;
};  

float4 MainPS(VertexShaderOutput input) : COLOR
{ /*
    origin GLSL 
	vec2 uv = fragCoord.xy / iResolution.xy;
    
    float o = texture(iChannel1, uv * 0.25 + vec2(0.0, iTime * 0.025)).r;
    float d = (texture(iChannel0, uv * 0.25 - vec2(0.0, iTime * 0.02 + o * 0.02)).r * 2.0 - 1.0);
    
    float v = uv.y + d * 0.1;
    v = 1.0 - abs(v * 2.0 - 1.0);
    v = pow(v, 2.0 + sin((iTime * 0.2 + d * 0.25) * TAU) * 0.5);
    
    vec3 color = vec3(0.0);
    
    float x = (1.0 - uv.x * 0.75);
    float y = 1.0 - abs(uv.y * 2.0 - 1.0);
    color += vec3(x * 0.5, y, x) * v;
    
    vec2 seed = fragCoord.xy;
    vec2 r;
    r.x = fract(sin((seed.x * 12.9898) + (seed.y * 78.2330)) * 43758.5453);
    r.y = fract(sin((seed.x * 53.7842) + (seed.y * 47.5134)) * 43758.5453);

    float s = mix(r.x, (sin((iTime * 2.5 + 60.0) * r.y) * 0.5 + 0.5) * ((r.y * r.y) * (r.y * r.y)), 0.04); 
    color += pow(s, 70.0) * (1.0 - v);
    
    fragColor.rgb = color;
    fragColor.a = 1.0; */
    float2 uv = input.TextureCoordinates;
    float o = tex2D(SpriteTextureSampler, uv * 0.25 + float2(0, iTime * 0.025)).r;
    float d = tex2D(hashSample, uv * 0.25 - float2(0, iTime * 0.02 + o * 0.02)).r * 2.0 - 1.0;
    float v = uv.y + d * 0.1;
    v = 1.0 - abs(v * 2.0 - 1.0);
    v = pow(v, 2.0 + sin((iTime * 0.2 + d * 0.25) * 6.28) * 0.5);
    
    float3 col = float3(0, 0, 0); 
    float x1 = (1.0 - uv.x * 0.75);
    float y1 = 1.0 - abs(uv.y * 2.0 - 1.0);
    col += float3(x1, y1, x1) * iRGB * v; 
    
    return input.Color * float4(col.x, col.y, col.z, 1);
}

technique SpriteDrawing
{
    pass P0
    {
        PixelShader = compile PS_SHADERMODEL MainPS();
    }
};