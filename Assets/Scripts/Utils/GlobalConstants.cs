using UnityEngine;

namespace Utils
{
    public static class GlobalConstants
    {
        private const float MinPlatformWidth = 50f;
        private const float MaxPlatformWidth = 600f;
        private static float ScreenDelta = Screen.width / 1080;

        public static float PlatformSpeed => 250f * ScreenDelta;
        public const int PlatformPoolSize = 10;
        public static float RandomPlatformWidth => Random.Range(MinPlatformWidth, MaxPlatformWidth) * ScreenDelta;
    }
}