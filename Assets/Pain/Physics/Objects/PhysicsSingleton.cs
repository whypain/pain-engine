using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Pain.Physics.Abstract;
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
        private PainlyPhysics m_physicsEngine;

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
            
            m_physicsEngine = new PainlyPhysics(m_gravity);
            
            Application.targetFrameRate = m_targetFrameRate;
        }

        private void FixedUpdate()
        {
            float dt = Time.fixedDeltaTime;
            
            ResolveVelocity(dt);
            ResolveCollisions();
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

        /// <summary>
        /// resolves and apply transforms for every physics objects based on force
        /// </summary>
        /// <param name="dt">delta time</param>
        private void ResolveVelocity(float dt)
        {
            foreach (PhysicsObject physObj in m_physicsObjectsArray)
            {
                physObj.pTransform = m_physicsEngine.CalculateVelocity(physObj.pTransform, dt);
                physObj.transform.position += physObj.pTransform.velocity * dt;
            }
        }

        /// <summary>
        /// resolves collision that might happen in a frame
        /// </summary>
        private void ResolveCollisions()
        {
            PhysicsObject[] collisionCandidate = FindCollisionCandidates();
            HashSet<PainCollider> collisions = new HashSet<PainCollider>();
            
            for (int i = 0; i < collisionCandidate.Length; i++)
            {
                if (!collisionCandidate[i].TryGetCollider(out PainCollider a)) continue;
                a.SetIsColliding(false);
                
                for (int j = i + 1; j < collisionCandidate.Length; j++)
                {
                    if (!collisionCandidate[j].TryGetCollider(out PainCollider b)) continue;
                    b.SetIsColliding(false);

                    bool isColliding = m_physicsEngine.IsCollision(a.Data, b.Data);
                    if (!isColliding) continue;
                        
                    collisions.Add(a);
                    collisions.Add(b);
                }
            }
            
            foreach (var c in collisions)
            {
                c.SetIsColliding(true);
            }
        }

        /// <summary>
        /// broad phase, find objects that might be colliding
        /// </summary>
        /// <returns></returns>
        private PhysicsObject[] FindCollisionCandidates()
        {
            // TODO: maybe quadtree
            return m_physicsObjectsArray;
        }
    }
}