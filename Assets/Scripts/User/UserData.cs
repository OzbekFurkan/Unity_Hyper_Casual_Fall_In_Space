using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace User_Data
{
    public enum Difficulty { EASY, NORMAL, HARD }

    [CreateAssetMenu(fileName = "New User Data", menuName = "Fall_In_Space/New User Data")]
    public class UserData : ScriptableObject
    {
        [Header("Game Data")]
        public float BestScore;
        public int CollectedStar;

        [Header("Game Settings")]
        [Range(0f, 1f)] public float AudioVal;
        public Difficulty Difficulty;

    }
}