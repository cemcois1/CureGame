using System;
using UnityEngine;

namespace Developing.Scripts.CureGame
{
    public class CMCameraManager : MonoBehaviour
    {
        [SerializeField] private GameObject[] cameras;
        [SerializeField] private int index;
        
        private void OnEnable()
        {
            GameManager.LevelStarted += ChangeCamera;
            PartManager.PartComplated += ChangeCamera;
        }

        private void OnDisable()
        {
            GameManager.LevelStarted -= ChangeCamera;
            PartManager.PartComplated -= ChangeCamera;
        }

        private void ChangeCamera()
        {
            if (index >= cameras.Length-1) return;
            for (int i = 0; i < cameras.Length; i++)
            {
                cameras[i].SetActive(false);
            }

            cameras[++index].SetActive(true);
        }
    }
}