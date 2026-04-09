using System.Linq;
using Pain.Physics.Core;
using UnityEngine;

namespace Pain.Physics.Objects
{
    public class CollisionDebugger : MonoBehaviour
    {
        [SerializeField] private PainCollider collA;
        [SerializeField] private PainCollider collB;

        private PainlyPhysics m_physics;
        
        private ColliderData m_dataA;
        private ColliderData m_dataB;

        private void OnDrawGizmos()
        {
            if (collA == null || collB == null) return;
            ColliderData dataA = collA.Data;
            ColliderData dataB = collB.Data;
            
            PhysVector2[] normalsA = PainColliderHelper.GetNormalsPolygon(dataA);
            PhysVector2[] normalsB = PainColliderHelper.GetNormalsPolygon(dataB);
            
            PhysVector2[] normalsAll = new PhysVector2[normalsA.Length + normalsB.Length];
            normalsA.CopyTo(normalsAll, 0);
            normalsB.CopyTo(normalsAll, normalsA.Length);

            for (int i = 0; i < normalsAll.Length; i++)
            {
                PhysVector2 normal = normalsAll[i];
                Debug.DrawRay(Vector3.zero, normal, Color.blue);
                
                // SAT (separating-axis theorem)
                PhysVector2 axisDir = new PhysVector2(normal.y, -normal.x).normalized;

                ProjectPoints(dataA.Verts, axisDir, out float minA, out float maxA);
                ProjectPoints(dataB.Verts, axisDir, out float minB, out float maxB);
                
                Gizmos.color = Color.yellow;
                Gizmos.DrawWireSphere(axisDir * minA, 0.05f);
                Gizmos.DrawWireSphere(axisDir * maxA, 0.05f);
                
                Gizmos.color = Color.brown;
                Gizmos.DrawWireSphere(axisDir * minB, 0.05f);
                Gizmos.DrawWireSphere(axisDir * maxB, 0.05f);
                
                Debug.DrawRay(Vector3.zero, axisDir * 100f, Color.red);
                Debug.DrawRay(Vector3.zero, axisDir * -100f, Color.red);
            }
        }

        private void ProjectPoints(PhysVector2[] verts, PhysVector2 axisDir, out float min, out float max)
        {
            float[] points = new float[verts.Length];
            for(int i = 0; i < points.Length; i++)
            {
                float pointAlongAxis = verts[i] * axisDir;
                points[i] = pointAlongAxis;
            }

            min = points.Min();
            max = points.Max();
        }

        private void OnValidate()
        {
            if (collA == null || collB == null) return;
            m_physics = new PainlyPhysics(new PhysVector3(0, 9.8f, 0));
        }
    }
}