using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UI;
using Utility;

namespace Rocket
{
    public class RocketData : MonoBehaviour
    {
        //read only data
        public RocketDataSO rocketDataSO;

        //rocket datas read/write
        [HideInInspector] public float rocketSpeed;

        [Header("Rocket Components")]
        [SerializeField] private SpriteRenderer rocketSpriteComponent;
        [SerializeField] private Transform rocketFireParticlePoint;
        [SerializeField] private Transform rocketTrailPoint;


        void Awake()
        {
            UpdateRocketDatas();
        }

        private void OnEnable()
        {
            ShopManager.OnItemIndexChanged += UpdateRocketDatas;
        }
        private void OnDisable()
        {
            ShopManager.OnItemIndexChanged -= UpdateRocketDatas;
        }

        private void UpdateRocketDatas()
        {
            //sprite
            rocketSpriteComponent.sprite = rocketDataSO.RocketSprite;

            //fire particle
            if (rocketFireParticlePoint.childCount > 0)
                Destroy(rocketFireParticlePoint.GetChild(0).gameObject);

            Instantiate(rocketDataSO.RocketFireParticle, rocketFireParticlePoint);

            //rocket trail
            if (rocketTrailPoint.childCount > 0)
                Destroy(rocketTrailPoint.GetChild(0).gameObject);

            Instantiate(rocketDataSO.RocketTrail, rocketTrailPoint);

            //Delete existing polygon collider
            rocketSpriteComponent.gameObject.TryGetComponent<PolygonCollider2D>(out var polygonCollider2D);
            if (polygonCollider2D != null)
                Destroy(polygonCollider2D);

            //Add new polygon collider
            Utils.AddPolygonCollider2D(rocketSpriteComponent.gameObject, rocketSpriteComponent.sprite);

        }

    }
}