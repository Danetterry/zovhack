using System.Numerics;

namespace zovhack
{
    public static class Calculate
    {
        public static Vector2 WorldToScreen(float[] matrix, Vector3 pos, Vector2 windowSize)
        {
            float num1 = (float)((double)matrix[12] * (double)pos.X + (double)matrix[13] * (double)pos.Y + (double)matrix[14] * (double)pos.Z) + matrix[15];
            if ((double)num1 <= 1.0 / 1000.0)
                return new Vector2(-99f, -99f);
            float num2 = (float)((double)matrix[0] * (double)pos.X + (double)matrix[1] * (double)pos.Y + (double)matrix[2] * (double)pos.Z) + matrix[3];
            float num3 = (float)((double)matrix[4] * (double)pos.X + (double)matrix[5] * (double)pos.Y + (double)matrix[6] * (double)pos.Z) + matrix[7];
            return new Vector2((float)((double)windowSize.X / 2.0 + (double)windowSize.X / 2.0 * (double)num2 / (double)num1), (float)((double)windowSize.Y / 2.0 - (double)windowSize.Y / 2.0 * (double)num3 / (double)num1));
        }
    }
}
