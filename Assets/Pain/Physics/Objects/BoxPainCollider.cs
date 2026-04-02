using Pain.Physics.Core;
using UnityEngine;

namespace Pain.Physics.Objects
{
    public class BoxPainCollider : PolyPainCollider
    {
        [SerializeField] protected Vector3 m_size;

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
    }
}