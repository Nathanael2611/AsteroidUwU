using System;
using Unity.VisualScripting;
using UnityEngine;

namespace DefaultNamespace
{
    public class AsteroidSpawner : MonoBehaviour
    {
        private void Start()
        {

            AsteroidPart.NewAsteroidPart();

        }
    }
}