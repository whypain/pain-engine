using Pain.Physics.Core;
using UnityEngine;

namespace Pain.Physics.Objects
{
    public class PhysicsObject : MonoBehaviour
    {
        [SerializeField] private float m_mass = 1f;
        [SerializeField] private float m_dragCoefficient = 1f;
        [SerializeField] private float m_crossSectionalArea = 1f;
        
        public bool UseGravity = true;
        public float Mass => m_mass;
        public float DragCoefficient => m_dragCoefficient;
        public float CrossSectionalArea => m_crossSectionalArea;
        public PhysVector3 Velocity => pTransform.velocity;

        internal int ID;
        internal PhysTransform pTransform;

        private void Awake()
        {
            pTransform = new PhysTransform(m_mass, transform);
        }

        private void Start()
        {
            PhysicsSingleton.Instance.Register(this);
        }

        private void OnDestroy()
        {
            PhysicsSingleton.Instance.Unregister(ID);
        }

        public void AddForce(Vector3 force)
        {
            pTransform.force += force;
        }

        internal void OnRegister(int id)
        {
            ID = id;
        }
        
        internal void SetMass(float mass) => m_mass = mass;
        internal void SetDragCoefficient(float dragCoefficient) => m_dragCoefficient = dragCoefficient;
        internal void SetCrossSectionalArea(float area) => m_crossSectionalArea = area;
    }

    public static class PhysicsObjectExtensions
    {
        public static void Copy(this PhysicsObject source, PhysicsObject target)
        {
            source.ID = target.ID;
            source.pTransform = target.pTransform;
            source.UseGravity = target.UseGravity;
            source.SetMass(target.Mass);
            source.SetDragCoefficient(target.DragCoefficient);
            source.SetCrossSectionalArea(target.CrossSectionalArea);
        }
    }
}