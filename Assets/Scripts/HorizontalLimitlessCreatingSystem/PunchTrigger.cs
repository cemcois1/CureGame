using DG.Tweening;
using UnityEngine;

namespace Developing.Scripts.HorizontalLimitlessCreatingSystem
{
    public class PunchTrigger:MonoBehaviour,IPunchScaleable
    {
        public void DoPunch()
        {
            transform.DOPunchScale(Vector3.one/6f , .5f, 0, 1);
        }
    }
}