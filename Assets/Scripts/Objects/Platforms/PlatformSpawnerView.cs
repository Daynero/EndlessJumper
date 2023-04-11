using Enums;
using UnityEngine;

namespace Objects.Platforms
{
    public class PlatformSpawnerView : MonoBehaviour
    {
        [SerializeField] private PlatformView platformPrefab;
        [SerializeField] private Transform topSpawner;
        [SerializeField] private Transform bottomSpawner;

        public PlatformView SpawnPlatform(Spawn spawn)
            => Instantiate(platformPrefab, spawn == Spawn.Top ? topSpawner : bottomSpawner);
    }
}