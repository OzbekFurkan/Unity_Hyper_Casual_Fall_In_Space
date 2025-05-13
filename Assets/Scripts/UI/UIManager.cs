using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Game;
using TMPro;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.SceneManagement;
using User_Data;

namespace UI
{
    public class UIManager : MonoBehaviour
    {
        public static UIManager uiManager;
        [SerializeField] UserData userData;

        [Header("Menu Panels")]
        [SerializeField] private GameObject shopAndInventory;
        [SerializeField] private GameObject playButton;
        [SerializeField] private GameObject gameOver;

        [Header("Game UI Items")]
        [SerializeField] private TextMeshProUGUI collectedStarText;
        [SerializeField] private TextMeshProUGUI scoreText;
        [SerializeField] private Image starCollectedImage;

        [Header("Game Over Items")]
        [SerializeField] private RectTransform tryAgainButton;
        [SerializeField] private RectTransform continueButton;
        [SerializeField] private RectTransform bestScorePanel;
        [SerializeField] private TextMeshProUGUI bestScoreText;

        private void Awake()
        {
            if (uiManager == null) uiManager = this;

            else Destroy(gameObject);

            InitializeUI();
        }

        private void OnEnable()
        {
            GameLogic.OnGameStateChanged += HandleGameState;
        }
        private void OnDisable()
        {
            GameLogic.OnGameStateChanged -= HandleGameState;
        }

        private void InitializeUI()
        {
            collectedStarText.text = userData.CollectedStar+"";
        }

        private void HandleGameState(GameState gameState)
        {
            switch(gameState)
            {
                case GameState.WAITING:
                    OnGameWaiting();
                    break;

                case GameState.PLAYING:
                    OnGameStarted();
                    break;

                case GameState.PAUSED:

                    break;

                case GameState.GAME_OVER:
                    OnGameEnded();
                    break;

                default:

                    break;
            }
        }

        private void OnGameWaiting()
        {
            gameOver.SetActive(false);
            shopAndInventory.transform.DOScale(Vector3.zero, 0.3f).From().OnComplete(() => {


                shopAndInventory.SetActive(true);
                playButton.SetActive(true);
                playButton.transform.DOScale(1.2f, 1f).SetEase(Ease.InElastic).SetLoops(-1, LoopType.Yoyo);
            });
            
        }

        private void OnGameStarted()
        {
            gameOver.SetActive(false);
            shopAndInventory.SetActive(false);
            playButton.SetActive(false);
        }

        private void OnGameEnded()
        {
            gameOver.SetActive(true);
            gameOver.transform.DOScale(Vector3.zero, 0.3f).From().OnComplete(()=> {

                bestScoreText.text = (int)userData.BestScore + "";

                if (userData.CollectedStar >=10)
                    continueButton.DOScale(1.2f, 1f).SetEase(Ease.InElastic).SetLoops(-1, LoopType.Yoyo);

                else
                    tryAgainButton.DORotate(new Vector3(0, 0, -45), 1f).SetEase(Ease.InOutElastic).SetLoops(-1, LoopType.Yoyo);

                if (userData.BestScore == GameLogic.gameLogic.GetScore())
                    bestScorePanel.DOAnchorPosY(600f, 0.5f).From().SetEase(Ease.InElastic);
            });
            
        }

        public void TryAgain()
        {
            gameOver.transform.DOScale(Vector3.zero, 0.3f).OnComplete(()=> {

                SceneManager.LoadScene(0);
            });
            
        }

        public void OnScored(float score)
        {
            scoreText.rectTransform.DOScale(1.5f, 0.4f).SetEase(Ease.OutCirc).OnComplete(()=> {

                scoreText.text = (int)score + "";
                scoreText.rectTransform.DOScale(1f, 0.4f).SetEase(Ease.OutCirc);
            });
            
        }

        public void DisplayStar(int collectedStar)
        {
            collectedStarText.text = collectedStar + "";
        }

        public void OnStarCollected(int collectedStar)
        {
            DisplayStar(collectedStar);

            starCollectedImage.gameObject.SetActive(true);

            Vector3 originalPosition = starCollectedImage.rectTransform.anchoredPosition;
            Color originalColor = starCollectedImage.color;

            starCollectedImage.rectTransform.DOAnchorPosY(-600f, 1f).SetEase(Ease.InOutQuad);

            starCollectedImage.DOFade(0.3f, 1f).SetEase(Ease.InOutQuad).OnComplete(() => {

                starCollectedImage.rectTransform.anchoredPosition = originalPosition;

                starCollectedImage.color = originalColor;

                starCollectedImage.gameObject.SetActive(false);
            });

        }

    }
}

