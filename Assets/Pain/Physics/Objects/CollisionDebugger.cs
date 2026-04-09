using System.Linq;
using Pain.Physics.Core;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Pain.Physics.Objects
{
    public class CollisionDebugger : MonoBehaviour
    {
        [SerializeField] private PainCollider collA;
        [SerializeField] private PainCollider collB;
        [SerializeField] private InputActionReference m_nextAction;

        private PainlyPhysics m_physics;
        
        private ColliderData m_dataA;
        private ColliderData m_dataB;

        private void OnEnable()
        {
            m_nextAction.action.Enable();
            m_nextAction.action.performed += GoNext;
        }

        private void OnDisable()
        {
            m_nextAction.action.Disable();
            m_nextAction.action.performed -= GoNext;
        }

        private void GoNext(InputAction.CallbackContext _)
        {
            
        }

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
                PhysVector2 normal = normalsAll[0];
                Debug.DrawRay(Vector3.zero, normal, Color.blue);
                
                // SAT (separating-axis theorem)
                PhysVector2 axisDir = new PhysVector2(normal.y, -normal.x).normalized;
                Debug.DrawRay(normal, axisDir * 100f, Color.red);
                Debug.DrawRay(normal, axisDir * -100f, Color.red);

                Gizmos.color = Color.yellow;

                float[] pointsA = new float[dataA.Verts.Length];
                for(int j = 0; j < pointsA.Length; j++)
                {
                    float pointAlongAxis = dataA.Verts[j] * axisDir;
                    pointsA[j] = pointAlongAxis;   
                }

                PhysVector2 minPointA = axisDir * pointsA.Min();
                PhysVector2 maxPointA = axisDir * pointsA.Max();
                
                Gizmos.DrawWireSphere(minPointA, 0.05f);
                Gizmos.DrawWireSphere(maxPointA, 0.05f);
                
                
                Gizmos.color = Color.brown;
                float[] pointsB = new float[dataB.Verts.Length];
                for(int j = 0; j < pointsB.Length; j++)
                {
                    float pointAlongAxis = dataB.Verts[j] * axisDir;
                    pointsB[j] = pointAlongAxis;
                }
                
                PhysVector2 minPointB = axisDir * pointsB.Min();
                PhysVector2 maxPointB = axisDir * pointsB.Max();
                
                Gizmos.DrawWireSphere(minPointB, 0.05f);
                Gizmos.DrawWireSphere(maxPointB, 0.05f);
            }
        }

        private PhysVector2 ProjectToLine(PhysVector2 point, PhysVector2 through, PhysVector2 dir)
        {
            return through + (point - through) * dir / (dir * dir) * dir;
        }

        private void OnValidate()
        {
            if (collA == null || collB == null) return;
            m_physics = new PainlyPhysics(new PhysVector3(0, 9.8f, 0));
        }
    }
}