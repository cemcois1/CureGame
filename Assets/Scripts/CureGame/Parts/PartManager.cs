using System;
using UnityEngine;

namespace Developing.Scripts.CureGame
{
    public class PartManager : MonoBehaviour
    {
        public static Action PartComplated;
        [SerializeField] private Part[] Parts;
        private int partcounter = 0;

        private void OnEnable()
        {
            GameManager.LevelStarted += OpenNextPart;
            PartComplated += OpenNextPart;
        }

        private void OnDisable()
        {
            GameManager.LevelStarted -= OpenNextPart;
            PartComplated -= OpenNextPart;
        }

        private void OpenNextPart()
        {
            Parts[partcounter].StartPart();
            partcounter++;
        }
    }
}