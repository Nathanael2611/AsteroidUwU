using UnityEngine;

namespace DefaultNamespace
{
    public class Utils
    {
        public static GameObject GetCameraGameObject()
        {
            return GameObject.FindGameObjectWithTag("MainCamera");
        }
    }
}