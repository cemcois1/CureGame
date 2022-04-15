using System;

namespace Developing.Scripts.CureGame
{
    public class SellectingPart:Part
    {
        public override void StartPart()
        {
            base.StartPart();
            print("Sellect");
            //UIManager.instance.OpenSellectObjectsText();
        }
    }
}