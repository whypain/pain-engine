using Pain.Physics.Core;
using UnityEngine;

namespace Pain.Physics.Objects
{
    public class PolyPainCollider : PainCollider
    {
        [SerializeField] private Vector2[] m_points = {};

        protected override void ConstructShape()
        {
            PhysVector2[] verts = new PhysVector2[m_points.Length];

            for (int i = 0; i < m_points.Length; i++)
            {
                PhysVector2 pos = (Vector2)transform.position + m_offset;
                verts[i] = new PhysVector2(m_points[i].x, m_points[i].y) + pos;
            }

            m_data.Verts = verts;

            for (int i = 0; i < verts.Length; i++)
            {
                Gizmos.DrawLine(verts[i], verts[(i + 1) % verts.Length]);
            }
        }
    }
}