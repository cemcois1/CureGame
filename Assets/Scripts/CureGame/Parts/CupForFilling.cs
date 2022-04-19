using System;
using DG.Tweening;
using UnityEngine;

namespace Developing.Scripts.CureGame
{
    public class CupForFilling : MonoBehaviour
    {
        public Transform finishTransform;
        public Transform fallingTransform;
        public static bool isSelectableCups = true;
        public Transform popsicleFallTransform;
        [SerializeField] private float movingDuration = .5f;
        private bool isRotateable;
        float rotateAmount = 0.4f;
        private static int complatedCounter;
        private ColourHolder colorholder;
        public HorizontalColorManager colorManager;
        private bool isFalling = false;
        [SerializeField] [Range(0f, 1f)] private float flowSpeed = 1f;
        public Transform pupsicleCup;
        public Transform pupsicle;
        public Transform frigeTransform;
        public Vector3 popsicleStartPosition;
        [SerializeField] public Transform frigeKapakTransform;
        [SerializeField] public Transform customerHandTransform;
        public CustomerSelector customSellector;

        private void OnEnable()
        {
            isSelectableCups = true;
            complatedCounter = 0;
            colorholder = GetComponent<ColourHolder>();
        }

        private void OnMouseUp()
        {
            if (!isSelectableCups) return;
            transform.DOJump(popsicleFallTransform.position, .2f, 1, movingDuration);
            isSelectableCups = false;
            isRotateable = true;
        }

        private void LateUpdate()
        {
            if (isFalling)
            {
                FillPopsicle(-transform.GetChild(0).rotation.x * flowSpeed * (1 / rotateAmount), colorholder.color,
                    complatedCounter);
            }
        }

        private void Update()
        {
            if (!isRotateable) return;
            if (Input.GetMouseButtonDown(0))
            {
                if (transform.GetChild(0).rotation.x < rotateAmount)
                {
                    isFalling = true;
                    transform.GetChild(0).DORotate(Vector3.right * -45f, 1f).OnComplete(() =>
                    {
                        isFalling = false;
                        isRotateable = false;
                        transform.GetChild(0).DetachChildren();
                        var seq = DOTween.Sequence();
                        seq.Append(transform.DOMove(finishTransform.position, movingDuration).OnComplete(
                            () =>
                            {
                                isSelectableCups = true;
                                complatedCounter++;
                                if (complatedCounter == 3)
                                {
                                    StartFreezingPart();
                                    isSelectableCups = false;
                                }
                            }).SetDelay(1.5f));
                    });
                }
                /*else
                {
                    isRotateable = false;
                    transform.GetChild(0).DetachChildren();
                    var seq = DOTween.Sequence();
                    print("Finished");
                    seq.Append(transform.DOMove(finishTransform.position, movingDuration).OnComplete(
                        () =>
                        {
                            isSelectableCups = true;
                            complatedCounter++;
                            print(complatedCounter);
                            if (complatedCounter == 3)
                            {
                                StartFreezingPart();
                                isSelectableCups = false;
                            }
                        }).SetDelay(2.5f));
                }*/
            }
        }

        //frigeKapakTransform.DORotate(Vector3.up * -120, .5f);
        private void StartFreezingPart()
        {
            customSellector.ChangePositionAndAnimation();
            var seq = DOTween.Sequence();
            seq.Append(pupsicleCup.DOMove(frigeTransform.position, 1f)); //
            seq.Append(frigeKapakTransform.DOLocalRotate(Vector3.up * 0, .5f)).SetDelay(1f);

            seq.Append(frigeKapakTransform.DOLocalRotate(Vector3.up * -120, .5f)).SetDelay(1f);
            seq.SetDelay(1f);
            seq.Append(pupsicleCup.DOMove(popsicleStartPosition, .5f)).SetDelay(1f);
            //dolabı uzaklaştır
            seq.Append(frigeKapakTransform.parent.DOMove(frigeKapakTransform.parent.position + Vector3.forward * -5f,
                1));
            //dondurmayı ele ver
            seq.Append(pupsicle.DOLocalMove(Vector3.forward * 0.53f,
                .5f).SetDelay(.5f));

            seq.Append(pupsicle.DOLocalRotate((Vector3.up * 180), .5f).SetDelay(.5f));
            seq.Append(pupsicle.DOMove(customerHandTransform.position, .5f).SetDelay(.5f))
                .OnComplete(() =>
                {
                    customSellector.PlayHappyParticle();
                    UIManager.instance.LevelComplatedUI();
                });
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="coloramount">0 is white, 1 is colorful</param>
        /// <param name="color"></param>
        private void FillPopsicle(float coloramount, Color color, int index)
        {
            this.MakeAction(() => colorManager.SetProgress(Mathf.Abs(coloramount), color, index), 1f - (index / 3f));
        }
    }
}