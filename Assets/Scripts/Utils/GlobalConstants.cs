using UnityEngine;

namespace Utils
{
    public static class GlobalConstants
    {
        private const float MinPlatformWidth = 50f;
        public const float MaxPlatformWidth = 600f;
        private static float ScreenDelta = Screen.width / 1080;

        public const float JumpForce = 600f;
        public const float DoubleClickTime = 0.2f;
        public static float PlatformSpeed => 250f * ScreenDelta;
        public const int PlatformPoolSize = 10;
        public static float RandomPlatformWidth => Random.Range(MinPlatformWidth, MaxPlatformWidth) * ScreenDelta;
    }
}