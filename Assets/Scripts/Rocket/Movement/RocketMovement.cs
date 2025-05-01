using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Game;
using DG.Tweening;


namespace Rocket
{
    public class RocketMovement : MonoBehaviour
    {

        [Header("Rocket Components")]
        [SerializeField] private RocketData _rocketData;
        [SerializeField] private Rigidbody2D _rigidbody;
        [SerializeField] private AudioSource audioSource;

        private bool lockMovement;
        private bool toggleRocketDirection=true;

        private void Awake()
        {
            lockMovement = true;
        }

        private void OnEnable()
        {
            GameLogic.OnGameStateChanged += SetRocketValues;
        }
        private void OnDisable()
        {
            GameLogic.OnGameStateChanged -= SetRocketValues;
        }

        private void SetRocketValues(GameState gameState)
        {
            if (gameState != GameState.PLAYING)
            {
                lockMovement = true;
                audioSource.volume = 0;
            }
            else
            {
                lockMovement = false;
                audioSource.volume = 0.3f;
            }
                
        }

        private void Update()
        {
            if (lockMovement == true)
            {
                _rocketData.rocketSpeed = 0;
                return;
            }

            _rocketData.rocketSpeed = _rocketData.rocketDataSO.rocketSpeed;

            transform.position = new Vector3(transform.position.x, Mathf.Clamp(transform.position.y, -5f, 5f));

            if (!Input.GetButtonDown("Fire1")) return;

            toggleRocketDirection = !toggleRocketDirection;

            transform.rotation = Quaternion.Euler(0, 0, toggleRocketDirection ? -45 : 45);

        }
        private void FixedUpdate()
        {
            if (lockMovement)
            {
                _rigidbody.velocity = Vector2.zero;
                return;
            }

            _rigidbody.velocity = transform.right * _rocketData.rocketSpeed;
        }
    }
}


