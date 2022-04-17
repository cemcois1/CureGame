using System;
using UnityEngine;

namespace Developing.Scripts.CureGame
{
    public class HorizontalColorManager : MonoBehaviour
    {
        [SerializeField] private Material[] materials;

        private void OnEnable()
        {
            ResetProgress();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="coloramount">0 is white, 1 is colorful</param>
        /// <param name="color"></param>
        /// 
        public void SetProgress(float coloramount, Color color, int index)
        {
            //float speed=3f;
           // Mathf.Lerp(materials[index].GetFloat("_TopLine"), -.2f + coloramount,Time.deltaTime*speed);
            materials[index].SetFloat("_TopLine", -.2f + coloramount);
            materials[index].SetColor("_GradientTopColor", color);
            materials[index].SetColor("_GradientBottomColor", color);
            if (-.2f + coloramount>.75f)
            {
                materials[index].SetFloat("_TopLine",  coloramount*10);
            }
        }

        public void ResetProgress()
        {
            for (int i = 0; i < materials.Length; i++)
            {
                materials[i].SetFloat("_TopLine", -.2f);
                materials[i].SetFloat("_BottomLine", -.2f);
                materials[i].SetColor("_OverTopColor", Color.white);
                materials[i].SetColor("_GradientTopColor", Color.white);
                materials[i].SetColor("_GradientBottomColor", Color.white);
            }
        }
    }
}