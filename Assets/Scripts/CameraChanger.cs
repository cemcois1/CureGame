using UnityEngine;

namespace Developing.Scripts
{
    public class CameraChanger:MonoBehaviour,IInteractible
    {
        public void Interact()
        {
            GameManager.LoadLevelEndCamera?.Invoke();
        }
    }
}