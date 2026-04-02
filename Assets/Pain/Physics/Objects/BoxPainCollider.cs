using Pain.Physics.Core;
using UnityEngine;

namespace Pain.Physics.Objects
{
    public class BoxPainCollider : PainCollider
    {
        [SerializeField] protected Vector3 m_size;
        
        public override PhysVector2[] GetEdgeNormals()
        {
            if (m_verts == null) return null;
            if (m_verts.Length == 0) return null;
            
            PhysVector2[] normals = new PhysVector2[m_verts.Length];

            for (int i = 0; i < m_verts.Length; i++)
            {
                PhysVector2 diff = m_verts[(i + 1) % m_verts.Length] - m_verts[i];       
                PhysVector2 normal = new PhysVector2(diff.y, -diff.x);
                normals[i] = normal.normalized;
            }

            return normals;
        }

        protected override void DrawCollider()
        {
            m_verts = new PhysVector2[4];
            float halfWidth = m_size.x * 0.5f;
            float halfHeight = m_size.y * 0.5f;

            Vector3 pos = transform.position + (Vector3)m_offset;
            
            m_verts[0] = new PhysVector2(pos.x + halfWidth, pos.y + halfHeight);
            m_verts[1] = new PhysVector2(pos.x - halfWidth, pos.y + halfHeight);
            m_verts[2] = new PhysVector2(pos.x - halfWidth, pos.y - halfHeight);
            m_verts[3] = new PhysVector2(pos.x + halfWidth, pos.y - halfHeight);

            for (int i = 0; i < m_verts.Length; i++)
            {
                Gizmos.DrawLine(m_verts[i], m_verts[(i + 1) % m_verts.Length]);
            }
        }

        protected override void DrawNormals()
        {
            if (m_verts == null) return;
            if (m_verts.Length == 0) return;

            PhysVector2[] normals = GetEdgeNormals();

            for (int i = 0; i < normals.Length; i++)
            {
                PhysVector2 mid = (m_verts[(i + 1) % m_verts.Length] - m_verts[i]) / 2;
                
                Gizmos.DrawRay(m_verts[i] + mid, normals[i]);
            }
        }
    }
}