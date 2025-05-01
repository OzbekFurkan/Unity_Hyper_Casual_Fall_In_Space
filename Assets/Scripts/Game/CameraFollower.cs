using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game
{
    public class CameraFollower : MonoBehaviour
    {
        public Transform target;
        public Vector3 offset;
        public float smoothSpeed;

        private bool following = false;

        private void OnEnable()
        {
            GameLogic.OnGameStateChanged += HandleGamestate;
        }
        private void OnDisable()
        {
            GameLogic.OnGameStateChanged -= HandleGamestate;
        }

        private void HandleGamestate(GameState gameState)
        {
            if(gameState != GameState.PLAYING)
            {
                following = false;
                return;
            }
            following = true;
        }

        void FixedUpdate()
        {
            if (!following) return;

            Vector3 desiredposition = target.position + offset;
            Vector3 smoothedposition = Vector3.Lerp(transform.position, desiredposition, smoothSpeed);
            transform.position = smoothedposition;
            transform.LookAt(target);
        }
    }
}

