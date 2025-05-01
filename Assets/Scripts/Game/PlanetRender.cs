using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utility;

namespace Game
{
    public class PlanetRender : MonoBehaviour
    {
        [Tooltip("Drag all planet sprites")]
        [SerializeField] private Sprite[] planets;
        [SerializeField] private SpriteRenderer spriteRenderer;

        private void Awake()
        {
            int randomPlanetIndex = Random.Range(0,planets.Length-1);
            spriteRenderer.sprite = planets[randomPlanetIndex];

            Utils.AddPolygonCollider2D(gameObject, planets[randomPlanetIndex]);
        }
    }
}

