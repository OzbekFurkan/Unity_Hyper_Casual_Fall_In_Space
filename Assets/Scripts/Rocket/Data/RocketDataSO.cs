using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Rocket
{
    public enum RocketStatus { PURCHASED_AND_EQUIPED, PURCHASED_NOT_EQUİPED, NOT_PURCHASED }

    [CreateAssetMenu(fileName = "New Rocket", menuName = "Fall_In_Space/New Rocket")]
    public class RocketDataSO : ScriptableObject
    {
        public Sprite RocketSprite;
        public ParticleSystem RocketFireParticle;
        public TrailRenderer RocketTrail;
        public string RocketName;
        public int RocketPrice;
        public float rocketSpeed;
        public RocketStatus rocketStatus;
    }
}
