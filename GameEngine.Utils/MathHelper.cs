namespace GameEngine.Utils
{
    public sealed class MathHelper
    {
        public static float DegreesToRadians(float degree)
        {
            return degree * MathF.PI / 180.0f;
        }
    }
}
