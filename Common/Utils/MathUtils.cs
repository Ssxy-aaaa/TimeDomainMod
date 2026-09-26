using Microsoft.Xna.Framework;

namespace TimeDomain.Common.Utils
{
    public static class MathUtils
    {
        /// <summary>
        /// 把 value 从 [fromMin, fromMax] 线性映射到 [toMin, toMax]。
        /// clamp 为 true 时，结果会被限制在 [toMin, toMax] 之间。
        /// </summary>
        public static float Remap(float value, float fromMin, float fromMax, float toMin, float toMax, bool clamp = false)
        {
            float t = (value - fromMin) / (fromMax - fromMin);
            if (clamp)
                t = MathHelper.Clamp(t, 0f, 1f);
            return toMin + t * (toMax - toMin);
        }
    }
}
