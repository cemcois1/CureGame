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
            MakeMoreVisibleColor(color, index);
            if (-.2f + coloramount > .75f)
            {
                materials[index].SetFloat("_TopLine", coloramount * 10);
            }
        }

        private void MakeMoreVisibleColor(Color color, int index)
        {
            if (color.r > 0.98f && color.g > 0.80f && color.b.Equals(1))
            {
                print("Manual Move");
                var mycolor = new Color(0.627451f, 0.2352941f, 1, 255);
                materials[index].SetColor("_GradientTopColor", mycolor);
                materials[index].SetColor("_GradientBottomColor", mycolor);
            }
            else if (color.b > .7f && color.r == 1f && color.g > 0.96f)
            {
                print("Manual Move");
                var mycolor = new Color(1f, 0.52f, 0, 255);
                materials[index].SetColor("_GradientTopColor", mycolor);
                materials[index].SetColor("_GradientBottomColor", mycolor);
            }
            else
            {
                float h, s, v;
                Color.RGBToHSV(color, out h, out s, out v);
                var mycolor = Color.HSVToRGB(h, 60f, v);
                materials[index].SetColor("_GradientTopColor", mycolor);
                materials[index].SetColor("_GradientBottomColor", mycolor);
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