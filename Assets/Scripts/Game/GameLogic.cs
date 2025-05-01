using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Rocket;
using UI;
using User_Data;


namespace Game
{
    public enum GameState { WAITING, PLAYING, PAUSED, GAME_OVER }

    public class GameLogic : MonoBehaviour
    {
        [HideInInspector] public static GameLogic gameLogic;

        [Header("References")]
        [SerializeField] private RocketData rocketData;
        [SerializeField] private UserData userData;
        [SerializeField] private ParticleSystem bestScoreCelebration;
        [SerializeField] private AudioSource audioSource;
        [SerializeField] private AudioClip collisionSound;
        [SerializeField] private AudioClip starCollectingSound;
        [SerializeField] private AudioClip bestScoreCelebrationSound;

        private float score = 0;
        [HideInInspector] public GameState gameState=GameState.WAITING;

        public static System.Action<GameState> OnGameStateChanged;


        private void Awake()
        {
            if (gameLogic == null)  gameLogic = this;

            else Destroy(gameObject);
        }

        private void Start()
        {
            OnGameStateChanged?.Invoke(gameState);
        }

        private void OnEnable()
        {
            RocketColissionHandler.OnRocketHit += OnRocketHit;
        }
        private void OnDisable()
        {
            RocketColissionHandler.OnRocketHit -= OnRocketHit;
        }

        public void StartGame()
        {
            if (rocketData.rocketDataSO.rocketStatus != RocketStatus.PURCHASED_AND_EQUIPED) return;

            gameState = GameState.PLAYING;
            OnGameStateChanged?.Invoke(gameState);
        }

        public void Continue()
        {
            if (userData.CollectedStar < 10) return;

            userData.CollectedStar -= 10;

            bestScoreCelebration.Stop();

            gameState = GameState.PLAYING;
            OnGameStateChanged?.Invoke(gameState);
        }

        public void PauseGame()
        {
            gameState = GameState.PAUSED;
            OnGameStateChanged?.Invoke(gameState);
        }

        public void EndGame()
        {
            if (score > userData.BestScore)
            {
                userData.BestScore = score;
                bestScoreCelebration.Play();
                audioSource.PlayOneShot(bestScoreCelebrationSound, 0.6f);
            }

            audioSource.PlayOneShot(collisionSound, 0.6f);

            gameState = GameState.GAME_OVER;
            OnGameStateChanged?.Invoke(gameState);
        }

        private void OnRocketHit(CollidingItem collidingItem, GameObject hit)
        {
            switch(collidingItem)
            {
                case CollidingItem.PLANET:
                    EndGame();
                    break;

                case CollidingItem.STAR:
                    CollectStar(hit);
                    break;

                default:

                    break;
            }
        }

        public void AddScore(float value)
        {
            score += value;
            UIManager.uiManager.OnScored(score);
        }
        public float GetScore() => score;

        public void AddStar(int value)
        {
            userData.CollectedStar += value;
        }

        private void CollectStar(GameObject hit)
        {
            AddStar(1);
            UIManager.uiManager.OnStarCollected(userData.CollectedStar);
            audioSource.PlayOneShot(starCollectingSound, 0.6f);
            Destroy(hit);
        }
        public float GetCollectedStar() => userData.CollectedStar;

    }
}