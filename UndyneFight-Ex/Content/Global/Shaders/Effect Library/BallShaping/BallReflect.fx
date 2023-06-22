#if OPENGL
#define SV_POSITION POSITION
#define VS_SHADERMODEL vs_3_0
#define PS_SHADERMODEL ps_3_0
#else
#define VS_SHADERMODEL vs_4_0_level_9_1
#define PS_SHADERMODEL ps_4_0_level_9_1
#endif

#define WIDTH 640.0
#define HEIGHT 480.0
#define BALLK 1.0
#define pi 3.1415926
#define MoveposDeg 360.0
#define MoveposR 0.9

//Texture2D mainTexture;
//uniform float4 mainTextureUV;

Texture2D SpriteTexture; //TextNoise.png
uniform float4 mainNoiseUV; //TextNoise.png的UV坐标[左上角X， 左上角Y， 宽， 高]环境单位：UV坐标系

uniform float4 mainColor; //渲染的主色调(BELEND)
uniform float2 mainInfor; //size、time
// $argument :: uniform float2 mainInfor.x >> function :: $'explain' >> 是TextNoise.png的图片宽度_建议动态识别（环境单位：像素点）>> name :: $size >> $END
// $argument :: uniform float2 mainInfor.y >> function :: $'explain' >> 一个持续增加的值 >> speed :: 0.02 / fps >> name :: $size >> $END

//sampler2D mainTextureSampler = sampler_state
//{
//	Texture = <mainTexture>;
//};

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

float2 TurnPos(float rad, float des)
{
    return float2(cos(rad) * des, sin(rad) * des);
}

float2 TurnDeg(float2 pos : POSITION)
{
    return float2(degrees(atan2(pos.y, pos.x)), length(pos));
}

float2 GetRat(float2 pos)
{
    return float2(pos.x / (mainInfor.x * 2.0), pos.y / (mainInfor.x * 2.0));
}

float2 TurnUV(float4 muv, float2 pos)
{
    float2 rat = GetRat(pos);
    return float2(muv.x + rat.x * muv.z, muv.y + rat.y * muv.w);
}

float2 TurnBalsurPos(float2 pos)
{
    float2 getdeg = TurnDeg(pos);
    float radius = mainInfor.x;
	
    float oldrat = length(pos) / radius;
    float newrat = sin(oldrat * BALLK);
	
	//return TurnPos( radians( getdeg.x ), oldrat * radius );//返回原始值(不作改变)
    return TurnPos(radians(getdeg.x), newrat * radius);
}

float2 RenderLocation(float4 thisUV, VertexShaderOutput input)
{
    return float2(
                    (input.TextureCoordinates.x - thisUV.x) / thisUV.z * WIDTH,
                    (input.TextureCoordinates.y - thisUV.y) / thisUV.w * HEIGHT
                );
}

float4 NormalDraw(float4 surtag : POSITION, float4 drawtag : POSITION) : COLOR
{
    return float4((drawtag.xyz * drawtag.w) + surtag.xyz * (1.0 - drawtag.w), 1.0);
}

float4 MainPS(VertexShaderOutput input) : COLOR
{
    float2 thisPosistion = RenderLocation(mainNoiseUV, input);
    float2 getPos = thisPosistion;
    float2 getCenter = float2(mainInfor.x, mainInfor.x);
    float lenrat = length(thisPosistion.xy - getCenter) / mainInfor.x;
    getPos = getCenter + TurnBalsurPos(getPos - getCenter);
    float2 getRat = GetRat(getPos);

    float2 getdeg = TurnDeg(getPos - getCenter);
    float2 setPos = TurnPos(radians(getdeg.x - mainInfor.y), getdeg.y);
	
    getPos = getCenter + setPos;
	
    float2 getmovepos = TurnPos(radians(mainInfor.y + MoveposDeg * getRat.x), mainInfor.x - mainInfor.x * MoveposR * getRat.y);
    float2 Movepos = getCenter + getmovepos;
	
    float2 UVPosNow = TurnUV(mainNoiseUV, getPos);
    float2 UVMovepos = TurnUV(mainNoiseUV, Movepos);
	
    float4 TextureNow = float4(0.0, 0.0, 0.0, 1.0); //设置底色
    TextureNow = TextureNow + (float4(mainColor.xyz, 1.0) * tex2D(SpriteTextureSampler, UVMovepos) * tex2D(SpriteTextureSampler, UVPosNow)); //Add
    TextureNow = TextureNow + float4(mainColor.xyz * pow((1.1 - lenrat), 3.0), 1.0);
	
    TextureNow = NormalDraw(TextureNow, float4(mainColor.xyz * pow(lenrat, 3.0), pow(lenrat, 3.0))); //外壳渲染
    TextureNow.w = min(1.0, floor(mainInfor.x / length(thisPosistion.xy - getCenter))); //透明度处理
	
    return TextureNow;
}

technique SpriteDrawing
{
    pass P0
    {
        PixelShader = compile PS_SHADERMODEL MainPS();
    }
};