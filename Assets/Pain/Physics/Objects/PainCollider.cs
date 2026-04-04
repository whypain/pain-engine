using Pain.Physics.Core;
using UnityEngine;

namespace Pain.Physics.Objects
{
    public abstract class PainCollider : MonoBehaviour
    {
        [SerializeField] protected Vector2 m_offset;
        
        [SerializeField] private bool m_drawNormals;

        public ColliderData Data => m_data;
        protected ColliderData m_data;
        
        protected abstract void ConstructShape();

        private void DrawNormals()
        {
            PhysVector2[] verts = m_data.Verts;
            if (verts == null) return;
            if (verts.Length == 0) return;

            PhysVector2[] normals = PainColliderHelper.GetNormalsPolygon(m_data);

            for (int i = 0; i < normals.Length; i++)
            {
                PhysVector2 mid = (verts[(i + 1) % verts.Length] - verts[i]) / 2;
                
                Gizmos.DrawRay(verts[i] + mid, normals[i]);
            }
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.green;
            ConstructShape();
            if (m_drawNormals)
            {
                Gizmos.color = Color.red;
                DrawNormals();
            }
        }
    }
}