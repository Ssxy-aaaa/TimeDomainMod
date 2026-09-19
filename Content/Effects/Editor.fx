sampler uImage0 : register(s0);
sampler uImage1 : register(s1);
float uTime;
float mod(float a,float b)
{
    return a % b;
}

float4 PixelShaderFunc(float4 sampleColor : COLOR0, float2 coords : TEXCOORD0) : COLOR0
{
    float uO2 = mod(uTime, 2.0);
    float uO;
    if (uO2 >= 1.0)
        uO = 1.0 - mod(uTime, 1.0);
    else
        uO = mod(uTime, 1.0);
    float4 color = tex2D(uImage0, coords);
    float4 blue = float4(0, 0, 1, 1);
    float4 red = float4(1, 0, 0, 1);
    float UseColorR = uO + coords.x;
    float UseColorG = coords.y;
    float UseColorB = 1.0 - uO + coords.x;
    //sampleColor.r *= abs(sin(uTime));
    if (color.r == 0.0 && color.g == 0.0 && color.b == 0.0 && color.a == 1.0)
    {
        return float4(0, 0, 0, 0);
    }
    return float4(float3(UseColorR, UseColorG, UseColorB), 1.0) * color.a; //color * sampleColor;
}
float4 PixelShaderFunc2(float4 sampleColor : COLOR0, float2 coords : TEXCOORD0) : COLOR0
{
    float4 color = tex2D(uImage0, coords);
    float4 blue = float4(0, 0, 1, 1);
    float4 red = float4(1, 0, 0, 1);
    float a = abs(sin(uTime + coords.x));
    float UseColorR = a;
    float UseColorB = 1.5 - a;
    //sampleColor.r *= abs(sin(uTime));
    return float4(float3(UseColorR, coords.y, UseColorB), 1) * color.a; //color * sampleColor;
}
float4 PixelShaderFunc3(float4 sampleColor : COLOR0, float2 coords : TEXCOORD0) : COLOR0
{
    float uO2 = mod(uTime, 2.0);
    float uO;
    if (uO2 >= 1.0)
        uO = 1.0 - mod(uTime, 1.0);
    else
        uO = mod(uTime, 1.0);
    float4 color = tex2D(uImage0, coords);
    float4 blue = float4(0, 0, 1, 1);
    float4 red = float4(1, 0, 0, 1);
    float UseColorR = uO + coords.x;
    float UseColorG = coords.y;
    float UseColorB = 1.0 - uO + coords.x;
    //sampleColor.r *= abs(sin(uTime));
    if (color.r == 0.0 && color.g == 0.0 && color.b == 0.0 && color.a == 1.0)
    {
        return float4(0, 0, 0, 0);
    }
    return float4(float3(UseColorR + color.r, UseColorG + color.g, UseColorB + color.b), 0.1) * color.a;  //color * sampleColor;
}
technique Technique1
{
    pass Pass0
    {
        PixelShader = compile ps_2_0 PixelShaderFunc();
    }
    pass Pass1
    {
        PixelShader = compile ps_2_0 PixelShaderFunc2();
    }
    pass Pass2
    {
        PixelShader = compile ps_2_0 PixelShaderFunc3();
    }
}