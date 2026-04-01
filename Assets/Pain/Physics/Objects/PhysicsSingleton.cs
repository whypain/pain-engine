using System;
using System.Collections.Generic;
using System.Linq;
using Pain.Physics.Abstract;
using UnityEngine;
using Pain.Physics.Core;

namespace Pain.Physics.Objects
{
    public class PhysicsSingleton : MonoBehaviour
    {
        public static PhysicsSingleton Instance { get; private set; }
        
        [SerializeField] private int m_targetFrameRate = 60;
        [SerializeField] private PhysVector3 m_gravity;

        private Dictionary<int, PhysicsObject> m_physicsObjects;
        private PhysicsObject[] m_physicsObjectsArray;
        private PhysicsComponent[] m_physicsComponents;

        private int m_nextId;

        private void Awake()
        {
            if (Instance != null && Instance != this)
            {
                Destroy(this.gameObject);
                return;
            }
            Instance = this;
            
            m_nextId = 0;
            LoadPhysicsComponent();
            
            Application.targetFrameRate = m_targetFrameRate;
        }

        private void FixedUpdate()
        {
            float dt = Time.fixedDeltaTime;
            
            foreach (PhysicsObject physObj in m_physicsObjectsArray)
            {
                ForEach(m_physicsComponents, c =>
                {
                    PhysicsObject p = c.OnApply(in physObj);
                    physObj.Copy(p);
                });
                
                PhysTransform pTransform = physObj.pTransform;
                PhysVector3 newAcceleration = pTransform.acceleration;
                PhysVector3 newVelocity = pTransform.velocity;
                PhysVector3 force = pTransform.force;
                
                // calculate force
                // force += physObj.pTransform.acceleration * pTransform.mass;
                newAcceleration += force * pTransform.invMass;
                
                // apply gravity
                if (physObj.UseGravity) newAcceleration += m_gravity;
                
                // calculate velocity
                newVelocity += newAcceleration * dt;
                
                // apply velocity
                physObj.pTransform.velocity = newVelocity;
                physObj.transform.position += (Vector3)newVelocity * dt;
                
                // clear force and acceleration
                physObj.pTransform.force = PhysVector3.zero;
                physObj.pTransform.acceleration = PhysVector3.zero;
            }
        }

        public void Register(PhysicsObject physicsObject)
        {
            m_physicsObjects ??= new Dictionary<int, PhysicsObject>();
            
            m_physicsObjects.Add(m_nextId, physicsObject);
            m_physicsObjectsArray = m_physicsObjects.Values.ToArray();
            
            physicsObject.OnRegister(m_nextId);
            m_nextId++;
        }

        public void Unregister(int id)
        {
            if (m_physicsObjects == null) return;
            if (!m_physicsObjects.Remove(id)) return;

            m_physicsObjectsArray = m_physicsObjects.Values.ToArray();
        }

        private void LoadPhysicsComponent()
        {
            m_physicsComponents = GetComponentsInChildren<PhysicsComponent>(includeInactive: false);
        }

        private void ForEach<T>(T[] enumerator, Action<T> action)
        {
            foreach (T item in enumerator)
            {
                action(item);
            }
        }
    }
}