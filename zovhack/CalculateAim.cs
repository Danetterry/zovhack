using System;
using System.Numerics;

namespace zovhack
{
    public static class CalculateAim
    {
        public static Vector2 CalculateAngles(Vector3 from, Vector3 to)
        {
            float x = to.X - from.X;
            float num = to.Y - from.Y;
            return new Vector2((float)(Math.Atan2((double)num, (double)x) * 180.0 / Math.PI), -(float)(Math.Atan2((double)to.Z - (double)from.Z, Math.Sqrt(Math.Pow((double)x, 2.0) + Math.Pow((double)num, 2.0))) * 180.0 / Math.PI));
        }
    }
}
