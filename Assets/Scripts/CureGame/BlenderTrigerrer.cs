using System;
using UnityEngine;
using Obi;
namespace Developing.Scripts.CureGame
{
    public class BlenderTrigerrer:MonoBehaviour
    {
        [SerializeField] private  ObiEmitter emiter;
        
        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out Fruit fruit))
            {
                CreateWater(fruit.particleAmount, fruit.Color);
                throw new NotImplementedException();
            }
        }

/// <param name="componentParticleAmount"> amount of particle for each particle</param>
/// <param name="componentColor">color of particle </param>
        private void CreateWater(float componentParticleAmount, Color componentColor)
        {
            
        }
    }
}