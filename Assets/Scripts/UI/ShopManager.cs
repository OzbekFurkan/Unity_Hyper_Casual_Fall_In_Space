using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Rocket;
using TMPro;
using UnityEngine.UI;
using Game;


namespace UI
{
    public class ShopManager : MonoBehaviour
    {
        
        [Header("Rockets")]
        [Tooltip("Drag all of the rocket data scriptable objects")]
        [SerializeField] private RocketDataSO[] allRockets;
        [SerializeField] private RocketData rocketData;

        [Header("Shop Status Button Properties")]
        [Tooltip("Contains all rocket states as the enum\n" +
            "0th: equiped and purchased button\n1th: purchased but not equiped button\n2th: not purchased button")]
        [SerializeField] private Sprite[] shopStatusButtons;
        [SerializeField] private Image shopStatusImage;
        [SerializeField] private GameObject conditionalShopStatusProp;//visible when not purchased or equiped
        [SerializeField] private TextMeshProUGUI priceText;

        [Header("Sounds")]
        [SerializeField] private AudioSource audioSource;
        [SerializeField] private AudioClip buttonSound;
        [SerializeField] private AudioClip unlockSound;

        private int itemIndex = 0;
        public static System.Action OnItemIndexChanged;

        void Awake()
        {
            UpdateShopStatusButton();
            OnItemIndexChanged?.Invoke();
        }

        public void NextRocketButtonPressed()
        {
            if(itemIndex < allRockets.Length-1)
            {
                itemIndex++;
                UpdateShopStatusButton();
                OnItemIndexChanged?.Invoke();
                audioSource.PlayOneShot(buttonSound);
            }
        }
        public void PrevRocketButtonPressed()
        {
            if(itemIndex>0)
            {
                itemIndex--;
                UpdateShopStatusButton();
                OnItemIndexChanged?.Invoke();
                audioSource.PlayOneShot(buttonSound);
            }
        }

        public void ShopButtonHandler()
        {
            switch(rocketData.rocketDataSO.rocketStatus)
            {
                case RocketStatus.NOT_PURCHASED:
                    PurchaseRocket();
                    break;

                case RocketStatus.PURCHASED_NOT_EQUİPED:
                    EquipRocket();
                    break;

                default:

                    break;
            }
        }

        public void EquipRocket()
        {
            if (rocketData.rocketDataSO.rocketStatus != RocketStatus.PURCHASED_NOT_EQUİPED) return;

            foreach(RocketDataSO _rocketDataSO in allRockets)
            {
                if (_rocketDataSO.rocketStatus != RocketStatus.PURCHASED_AND_EQUIPED) continue;

                _rocketDataSO.rocketStatus = RocketStatus.PURCHASED_NOT_EQUİPED;
            }

            rocketData.rocketDataSO.rocketStatus = RocketStatus.PURCHASED_AND_EQUIPED;

            UpdateShopStatusButton();
        }

        public void PurchaseRocket()
        {
            if (GameLogic.gameLogic.GetCollectedStar() < rocketData.rocketDataSO.RocketPrice) return;

            GameLogic.gameLogic.AddStar(-rocketData.rocketDataSO.RocketPrice);

            rocketData.rocketDataSO.rocketStatus = RocketStatus.PURCHASED_NOT_EQUİPED;

            UpdateShopStatusButton();
            audioSource.PlayOneShot(unlockSound);
        }

        private void UpdateShopStatusButton()
        {
            RocketDataSO _rocketData = allRockets[itemIndex];

            rocketData.rocketDataSO = _rocketData;

            shopStatusImage.sprite = shopStatusButtons[(int)_rocketData.rocketStatus];

            if(_rocketData.rocketStatus == RocketStatus.NOT_PURCHASED)
            {
                conditionalShopStatusProp.SetActive(true);

                priceText.text = _rocketData.RocketPrice+"";
            }
            else
                conditionalShopStatusProp.SetActive(false);

        }
    }
}


