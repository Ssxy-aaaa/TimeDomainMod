sampler uImage0 : register(s0);
sampler uImage1 : register(s1);
float uTime;

float4 PixelShaderFunc(float4 sampleColor : COLOR0, float2 coords : TEXCOORD0) : COLOR0
{
    float4 color = tex2D(uImage0, coords);
    sampleColor.r *= abs(sin(uTime));
    return color * sampleColor;
}
technique Technique1
{
    pass
    {
        PixelShader = compile ps_2_0 PixelShaderFunc();
    }
}