using System;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Serialization;

namespace Developing.Scripts.CureGame
{
    [SelectionBase]
    public class Cup : MonoBehaviour
    {
        public static bool isSelectable = true;
        public static int complatedCounter;

        [FormerlySerializedAs("targetTransform")]
        public Transform blenderFallTransform;

        public float duration;
        private bool isRotateable;
        [SerializeField] float rotateAmount = -0.85f;
        public Vector3 startPosition;
        public Transform fillingTransform;
        public Transform finalTransform;
        float fillingTime = 2f;
        public Faucet faucet;

        private void OnEnable()
        {
            isSelectable = true;
            complatedCounter = 0;
        }

        private void OnMouseUp()
        {
            if (!isSelectable) return;
            transform.DOJump(blenderFallTransform.position, .2f, 1, duration);
            isSelectable = false;
            isRotateable = true;
        }

        private void Update()
        {
            if (!isRotateable) return;
            if (Input.GetMouseButton(0))
            {
                if (transform.rotation.x > rotateAmount)
                {
                    transform.Rotate(Vector3.right * -90 * Time.deltaTime);
                }
                else
                {
                    isRotateable = false;
                    transform.GetChild(0).DetachChildren();
                    var seq = DOTween.Sequence();
                    seq.Append(transform.DOJump(fillingTransform.position, 1, 1, duration).SetDelay(.2f).OnComplete(
                        () =>
                        {
                            faucet._obiEmiter.speed = 1;
                            faucet._obiParticleRenderer.particleColor = GetComponent<ColourHolder>().color;
                            this.MakeAction(() =>
                            {
                                faucet._obiEmiter.speed = 0;
                                faucet.transform.parent = transform;
                            }, 1f);
                        }));
                    seq.Join(transform.DORotate(Vector3.zero, duration));
                    seq.Append(transform.DOMove(finalTransform.position, duration).SetDelay(fillingTime).OnComplete(
                        () =>
                        {
                            isSelectable = true;
                            complatedCounter++;
                            if (complatedCounter == 3)
                            {
                                UIManager.instance.OpenDoneButton();
                                isSelectable = false;
                            }
                        }));
                }
            }
        }
    }
}