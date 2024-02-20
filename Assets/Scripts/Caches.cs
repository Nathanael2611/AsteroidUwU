using UnityEngine;

namespace DefaultNamespace
{
    public class Caches
    {
        public static readonly ResourceCache<Sprite> SpriteCache = new ResourceCache<Sprite>();
        public static readonly ResourceCache<GameObject> PrefabCache = new ResourceCache<GameObject>();

    }
}