using Pain.Physics.Core;
using UnityEngine;

namespace Pain.Physics.Objects
{
    public abstract class PainCollider : MonoBehaviour
    {
        [SerializeField] protected Vector2 m_offset;
        
        [SerializeField] private bool m_drawNormals;
        
        protected PhysVector2[] m_verts;
        
        public abstract PhysVector2[] GetEdgeNormals();
        protected abstract void DrawCollider();
        protected abstract void DrawNormals();

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.green;
            DrawCollider();
            if (m_drawNormals)
            {
                Gizmos.color = Color.red;
                DrawNormals();
            }
        }
    }
}