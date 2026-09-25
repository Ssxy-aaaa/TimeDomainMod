sampler uImage0 : register(s0);

float4 Ps(float4 color : COLOR0, float2 coords : TEXCOORD0) : COLOR0
{
    //color.rgb = abs(u_negativeAmount - color.rgb);
    float4 UseColor;
    UseColor = tex2D(uImage0, coords);
    UseColor.rgb = float3(1.0, 1.0, 1.0) - UseColor.rgb;
    return UseColor * UseColor.a;
}
technique Technique1
{
    pass Pass0
    {
        PixelShader = compile ps_2_0 Ps();
    }
}