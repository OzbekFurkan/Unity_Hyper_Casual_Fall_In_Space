using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Rocket;
using Zenject;

namespace Game
{
    public class PlanetManager : MonoBehaviour
    {
        [HideInInspector] public Transform rocket;
        [Inject] GameLogic gameLogic;
        private bool hasScored = false;

        // Update is called once per frame
        void Update()
        {
            if (transform.position.x < rocket.position.x && !hasScored)
            {
                gameLogic.AddScore(0.5f);
                hasScored = true;
            }
            if (transform.position.x < rocket.position.x-10)
            {
                Destroy(gameObject);
            }
        }
    }
}