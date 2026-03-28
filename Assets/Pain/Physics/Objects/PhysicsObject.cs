using System;
using Pain.Physics.Core;
using UnityEngine;

namespace Pain.Physics.Objects
{
    public class PhysicsObject : MonoBehaviour
    {
        [SerializeField] private float m_mass = 1f;
        [SerializeField] private PhysVector3 m_testForce;
        
        public PhysTransform physTransform { get; private set; }

        private void Awake()
        {
            physTransform = new PhysTransform(m_mass, transform);
        }

        private void Start()
        {
            AddForce(m_testForce);
        }

        private void FixedUpdate()
        {
            physTransform.Step(Time.fixedDeltaTime);
        }

        private void AddForce(PhysVector3 force)
        {
            physTransform = physTransform.AddForce(force);
        }
    }
}
