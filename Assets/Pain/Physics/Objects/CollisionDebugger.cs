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
                PhysVector2 normal = normalsAll[i];
                Debug.DrawRay(Vector3.zero, normal, Color.blue);
                
                // SAT (separating-axis theorem)
                PhysVector2 d = new PhysVector2(normal.y, -normal.x);
                Debug.DrawRay(normal, d * 100f, Color.red);
                Debug.DrawRay(normal, d * -100f, Color.red);

                Gizmos.color = Color.yellow;
                foreach (var p in dataA.Verts)
                {
                    PhysVector2 newP = ProjectToLine(p, normal, d);
                    Gizmos.DrawWireSphere(newP, 0.1f);
                }
                
                Gizmos.color = Color.brown;
                foreach (var p in dataB.Verts)
                {
                    PhysVector2 newP = ProjectToLine(p, normal, d);
                    Gizmos.DrawWireSphere(newP, 0.1f);
                }
                // Debug.DrawLine(normal - d, normal + d, Color.red);
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