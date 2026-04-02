using Pain.Physics.Core;
using UnityEngine;

namespace Pain.Physics.Objects
{
    public class PolyPainCollider : PainCollider
    {
        [SerializeField] private Vector2[] m_points = {};
        
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
            m_verts = new PhysVector2[m_points.Length];

            for (int i = 0; i < m_points.Length; i++)
            {
                PhysVector2 pos = (Vector2)transform.position + m_offset;
                m_verts[i] = new PhysVector2(m_points[i].x, m_points[i].y) + pos;
            }

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