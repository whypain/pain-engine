using Pain.Physics.Core;
using UnityEngine;

namespace Pain.Physics.Objects
{
    public class BoxPainCollider : PolyPainCollider
    {
        [SerializeField] protected Vector3 m_size;

        protected override void ConstructShape()
        {
            PhysVector2[] verts = new PhysVector2[4];
            float halfWidth = m_size.x * 0.5f;
            float halfHeight = m_size.y * 0.5f;

            Vector3 pos = transform.position + (Vector3)m_offset;
            
            verts[0] = new PhysVector2(pos.x + halfWidth, pos.y + halfHeight);
            verts[1] = new PhysVector2(pos.x - halfWidth, pos.y + halfHeight);
            verts[2] = new PhysVector2(pos.x - halfWidth, pos.y - halfHeight);
            verts[3] = new PhysVector2(pos.x + halfWidth, pos.y - halfHeight);

            m_data.Verts = verts;

            for (int i = 0; i < verts.Length; i++)
            {
                Gizmos.DrawLine(verts[i], verts[(i + 1) % verts.Length]);
            }
        }
    }
}