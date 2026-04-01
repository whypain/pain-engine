using Pain.Physics.Core;
using UnityEngine;

namespace Pain.Physics.Objects
{
    public class PhysicsObject : MonoBehaviour
    {
        [SerializeField] private float m_mass = 1f;
        public bool UseGravity = true;

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
    }
}