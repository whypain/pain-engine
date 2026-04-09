using System;
using Pain.Physics.Core;
using UnityEngine;

namespace Pain.Physics.Objects
{
    public abstract class PainCollider : MonoBehaviour
    {
        [SerializeField] protected Vector2 m_offset;
        
        [SerializeField] private bool m_drawNormals;

        public ColliderData Data => m_data;
        private ColliderData m_data;

        private bool m_isColliding;
        
        protected abstract void ConstructShape(ref PhysVector2[] verts);

        private void Awake()
        {
            ConstructShape(ref m_data.Verts);
        }

        private void FixedUpdate()
        {
            ConstructShape(ref m_data.Verts);
        }

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

        private void DrawShape()
        {
            PhysVector2[] verts = m_data.Verts;
            for (int i = 0; i < verts.Length; i++)
            {
                Gizmos.DrawLine(verts[i], verts[(i + 1) % verts.Length]);
            }
        }

        public void SetIsColliding(bool isColliding)
        {
            m_isColliding = isColliding;
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = m_isColliding
                ? Color.red
                : Color.green;

            ref PhysVector2[] verts = ref m_data.Verts;

            if (!Application.isPlaying)
            {
                ConstructShape(ref verts);
            }
            
            DrawShape();
            if (m_drawNormals)
            {
                Gizmos.color = Color.red;
                DrawNormals();
            }
        }
    }
}