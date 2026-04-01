using Pain.Physics.Abstract;
using Pain.Physics.Core;
using UnityEngine;

namespace Pain.Physics.Objects
{
    public class AirResistance : PhysicsComponent
    {
        [SerializeField] private float m_airDensity;
        
        public override PhysicsObject OnApply(in PhysicsObject obj)
        {
            // air drag formula from https://en.wikipedia.org/wiki/Drag_%28physics%29#The_drag_equation
            float dragForce = 0.5f * m_airDensity * Mathf.Pow(obj.Velocity.magnitude, 2) 
                              * obj.CrossSectionalArea * obj.DragCoefficient;
            
            PhysVector3 drag = obj.Velocity.normalized.inversed * dragForce;
            
            // Debug.Log($"Velocity: {obj.Velocity.ToString()} | Magnitude: {obj.Velocity.magnitude} | Normalized: {obj.Velocity.normalized.ToString()}");
            Debug.Log($"Drag: {drag}");
            
            obj.AddForce(drag);
            return obj;
        }
    }
}