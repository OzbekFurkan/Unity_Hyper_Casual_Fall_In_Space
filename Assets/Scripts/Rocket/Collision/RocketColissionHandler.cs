using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Rocket
{

    public enum CollidingItem { PLANET, STAR }

    public class RocketColissionHandler : MonoBehaviour
    {
        public static System.Action<CollidingItem, GameObject> OnRocketHit;


        private void OnTriggerEnter2D(Collider2D collision)
        {
            string tag = collision.tag;

            switch(tag)
            {
                case "planet":
                    OnRocketHit?.Invoke(CollidingItem.PLANET, collision.gameObject);
                    break;

                case "star":
                    OnRocketHit?.Invoke(CollidingItem.STAR, collision.gameObject);
                    break;

                default:

                    break;
            }

        }

    }
}