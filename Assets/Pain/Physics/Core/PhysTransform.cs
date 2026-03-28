using System;
using UnityEngine;

namespace Pain.Physics.Core
{
    [Serializable]
    public struct PhysTransform
    {
        public PhysVector3 velocity     { get; private set; }
        public PhysVector3 acceleration { get; private set; }

        public float mass { get; private set; }

        private Transform m_transform;

        public PhysTransform(float mass, Transform transform)
        {
            velocity = PhysVector3.zero;
            acceleration = PhysVector3.zero;
            
            this.mass = mass;
            m_transform = transform;
        }

        public PhysTransform AddForce(PhysVector3 force)
        {
            acceleration += force * mass;
            return this;
        }

        public void Step(float dt)
        {
            velocity += acceleration * dt;
            m_transform.position += (Vector3)velocity * dt;
        }
    }
}