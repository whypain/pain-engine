using Pain.Physics.Core;
using UnityEngine;

namespace Pain.Physics.Objects
{
    public class PhysicsObject : MonoBehaviour
    {
        [SerializeField] private float m_mass = 1f;
        [SerializeField] private PhysVector3 m_forceOnStart;

        [HideInInspector] public int ID;
        [HideInInspector] public PhysTransform pTransform;

        private void Awake()
        {
            pTransform = new PhysTransform(m_mass, transform);
        }

        private void Start()
        {
            PhysicsSingleton.Instance.Register(this);
            pTransform.force = m_forceOnStart;
        }

        private void OnDestroy()
        {
            PhysicsSingleton.Instance.Unregister(ID);
        }

        public void OnRegister(int id)
        {
            ID = id;
        }
    }
}
