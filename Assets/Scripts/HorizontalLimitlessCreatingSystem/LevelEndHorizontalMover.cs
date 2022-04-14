using System;
using DG.Tweening;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Developing.Scripts.HorizontalLimitlessCreatingSystem
{
    [SelectionBase]
    public class LevelEndHorizontalMover : MonoBehaviour, IMiniGame
    {
        [SerializeField] private FinishTrigger _finishTrigger;
        [SerializeField] private GameObject HighScorePrefab;

        private void OnEnable()
        {
            _finishTrigger.MiniGame = this;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out IPunchScaleable punchable))
            {
                punchable.DoPunch();
            }
        }

        public void StartMinigame()
        {
        }

        public void StartMinigame(int score)
        {
            if (score > 600)
            {
                score = 600;
            }

            if (PlayerPrefs.GetInt("Highscore") < score)
            {
                PlayerPrefs.SetInt("Highscore", score);
            }

            print("Highscore" + score);
            var endValue = (score / 5) - 19;
            CreateHighScoreText((PlayerPrefs.GetInt("Highscore") / 5) - 20);
            transform.DOLocalMoveY(endValue, 3f).OnComplete(() => GameManager.MinilevelFinished?.Invoke());
        }

        private void CreateHighScoreText(int endValue)
        {
            var obj = Instantiate(HighScorePrefab);
            obj.transform.parent = transform.parent;
            obj.transform.position = transform.position;
            obj.transform.localScale = Vector3.one *0.3f;
            obj.transform.localPosition = new Vector3(-0.456f, endValue,-2.07f);
        }
    }
}