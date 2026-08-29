sampler uImage0 : register(s0);
sampler uImage1 : register(s1);
float2 uResolution;

//这里可以理解为你在游戏中传入的纹理，这里我为了方便教学就手动绘制了这些黑白相间的小方块
float3 Tex2D(float2 uv)
{
   //（这里传入的贴图是下方的iChannel1）
    return tex2D(uImage1, uv).xyz;
    
    //这里是手动绘制的黑白相间的小方块
    /*
    float3 col = float3(1.0);
    col *= step(0.0, cos(uv.xxx * 114.514) * cos(uv.yyy * 114.514));
    return col;
    */
}

///这是要获取的素材中像素坐标 增加/减少 的值
float2 uvAddValue(float2 uv)
{
    //最为简单直接的全屏偏移
    //return float2(0.0);
    
    //跟随时间的扭曲（你只需要在泰拉的绘制里面传入这个iTime的值就行）
    //return cos((uv.xy * 6.28)/*&lt;-这里不用动*/ * 10.0 + iTime * 10.0) * 0.01;
    
    //根据传入的贴图进行偏移扭曲（这里传入的贴图是下方的iChannel0）
    float3 t = tex2D(uImage0, uv).xyz;
    //t.rr : 由纹理t的红色的值组成的float2(),(相当于泰拉的shader中的float2（）)
    return t.bb * 0.5;
}

//主函数
void mainImage(out float4 fragColor, in float2 fragCoord)
{
    //uv: 理解为你的屏幕参数坐标（在泰拉的shader中是-&gt;左上角到右下角：(0.0, 0.0)~(1.0, 1.0)）
    float2 uv = fragCoord / uResolution.xy; //在泰拉的shader中不需要写这个东西
    
    
    float2 pos = uv;
    
    //对需要获取像素的坐标进行偏移处理
    uv += uvAddValue(pos);
    
    float3 final = Tex2D(uv);

    //fragColor = xxx：相当于泰拉的shader里面主函数的return xxx
    fragColor = float4(final, 1.0);
}
technique Technique1
{
    pass
    {
        PixelShader = compile ps_2_0 uvAddValue();
    }
}