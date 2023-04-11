using System.Collections.Generic;
using UnityEngine;

namespace Utils
{
    public static class GlobalConstants
    {
        public const float GameScoreMultiplier = 5;

        public const float PlatformSpeed = 250f;

        private const float MinPlatformWidth = 100f;
        private const float MaxPlatformWidth = 600f;

        public static float RandomPlatformWidth => Random.Range(MinPlatformWidth, MaxPlatformWidth);
    }
}