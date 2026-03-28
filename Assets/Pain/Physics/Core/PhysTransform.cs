using System;
using UnityEngine;

namespace Pain.Physics.Core
{
    [Serializable]
    public struct PhysTransform
    {
        public PhysVector3 force        { get; private set; }
        public PhysVector3 velocity     { get; private set; }
        public PhysVector3 acceleration { get; private set; }

        public float mass { get; private set; }
        private float m_invMass;

        private Transform m_transform;

        public PhysTransform(float mass, Transform transform)
        {
            force = PhysVector3.zero;
            velocity = PhysVector3.zero;
            acceleration = PhysVector3.zero;
            
            this.mass = mass;
            m_invMass = 1 / mass;
            m_transform = transform;
        }

        public PhysTransform AddForce(PhysVector3 force)
        {
            this.force += force;
            return this;
        }

        public PhysTransform AddAcceleration(PhysVector3 acceleration)
        {
            this.acceleration += acceleration;
            return this;
        }

        public void Step(float dt)
        {
            acceleration += force * m_invMass;
            
            velocity += acceleration * dt;
            m_transform.position += (Vector3)velocity * dt;
            
            // reset values to prevent object speeding overtime
            force = PhysVector3.zero;
            acceleration = PhysVector3.zero;
        }
    }
}