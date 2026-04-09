using System.Linq;
using Pain.Physics.Core;
using UnityEngine;
using UnityEngine.Serialization;

namespace Pain.Physics.Objects
{
    public class CollisionDebugger : MonoBehaviour
    {
        [SerializeField] private PainCollider collA;
        [SerializeField] private PainCollider collB;

        [SerializeField] private bool[] drawAxis;

        private PainlyPhysics m_physics;
        
        private ColliderData m_dataA;
        private ColliderData m_dataB;

        private void OnDrawGizmos()
        {
            if (collA == null || collB == null) return;

            Gizmos.color = m_physics.IsCollision(collA.Data, collB.Data, out CollisionContext[] ctx)
                ? Color.red
                : Color.green;
            
            Gizmos.DrawWireSphere(Vector3.zero, 0.5f);
            
            drawAxis ??= new bool[ctx.Length];
            
            for (int i = 0; i < ctx.Length; i++)
            {
                if (drawAxis[i]) DrawAxis(ctx[i]);
            }
        }

        private void DrawAxis(CollisionContext ctx)
        {
            PhysVector2 axisDir = ctx.AxisDir;
            float minA = ctx.MinA;
            float maxA = ctx.MaxA;
            float minB = ctx.MinB;
            float maxB = ctx.MaxB;
            
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(axisDir * minA, 0.05f);
            Gizmos.DrawWireSphere(axisDir * maxA, 0.05f);
            
            Gizmos.color = Color.brown;
            Gizmos.DrawWireSphere(axisDir * minB, 0.05f);
            Gizmos.DrawWireSphere(axisDir * maxB, 0.05f);
            
            Debug.DrawRay(Vector3.zero, axisDir * 100f, Color.red);
            Debug.DrawRay(Vector3.zero, axisDir * -100f, Color.red);
        }

        private void OnValidate()
        {
            if (collA == null || collB == null) return;
            m_physics = new PainlyPhysics(new PhysVector3(0, 9.8f, 0));
        }
    }
}