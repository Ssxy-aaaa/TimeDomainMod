sampler uImage0 : register(s0);
sampler uImage1 : register(s1);
float iTime;
float O;

float mod(float a, float b)
{
    return a % b;
}

float4 PixelShaderFunc(float4 sampleColor : COLOR0, float2 coords : TEXCOORD0) : COLOR0
{
    //float2 NewCoords = float2(mod(0.5 + coords.x, 1.0), mod(0.5 + coords.y, 1.0));
    
    float2 NewCoords = coords + cos(coords.xy * 6.28 + iTime) * O;
    NewCoords.x = mod(abs(NewCoords.x), 2.0);
    NewCoords.y = mod(abs(NewCoords.y), 2.0);
    if (NewCoords.x >= 1.0)
        NewCoords.x = 2.0 - NewCoords.x;
    if (NewCoords.y >= 1.0)
        NewCoords.y = 2.0 - NewCoords.y;
    float4 UseColor = tex2D(uImage0, NewCoords);
    return UseColor;
}
float4 PixelShaderFunc2(float4 sampleColor : COLOR0, float2 coords : TEXCOORD0) : COLOR0
{
    sampleColor = tex2D(uImage0, coords);
    float2 NewCoords = sampleColor.rr * 0.05;
    float4 UseColor = tex2D(uImage0, NewCoords);
    return UseColor;
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
}