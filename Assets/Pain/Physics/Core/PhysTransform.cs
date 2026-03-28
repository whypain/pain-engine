using System;
using UnityEngine;

namespace Pain.Physics.Core
{
    [Serializable]
    public struct PhysTransform
    {
        public PhysVector3 force;
        public PhysVector3 velocity;
        public PhysVector3 acceleration;

        public float mass;
        public float invMass;
        
        public PhysTransform(float mass, Transform transform)
        {
            force = PhysVector3.zero;
            velocity = PhysVector3.zero;
            acceleration = PhysVector3.zero;
            
            this.mass = mass;
            invMass = 1 / mass;
        }
    }
}