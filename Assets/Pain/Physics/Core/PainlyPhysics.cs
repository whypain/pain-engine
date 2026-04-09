using System.Linq;

namespace Pain.Physics.Core
{
    public struct CollisionContext
    {
        public readonly PhysVector2 AxisDir;
        public readonly float MinA;
        public readonly float MaxA;
        public readonly float MinB;
        public readonly float MaxB;

        public CollisionContext(PhysVector2 axisDir, float minA, float maxA, float minB, float maxB)
        {
            AxisDir = axisDir;
            MinA = minA;
            MaxA = maxA;
            MinB = minB;
            MaxB = maxB;
        }
    }
    public class PainlyPhysics
    {
        private readonly PhysVector3 m_gravity;

        public PainlyPhysics(PhysVector3 gravity)
        {
            m_gravity = gravity;
        }
        
        /// <summary>
        /// calculates the object's velocity based on mass and force
        /// </summary>
        /// <param name="p"></param>
        /// <param name="dt"></param>
        /// <returns></returns>
        public PhysTransform CalculateVelocity(PhysTransform p, float dt)
        {
            // a = F/m
            PhysVector3 newAcceleration = p.force * p.invMass;
            if (p.useGravity) newAcceleration += m_gravity;
            
            p.velocity += newAcceleration * dt;

            p.force = PhysVector3.zero;
            return p;
        }

        /// <summary>
        /// pair test using SAT
        /// </summary>
        /// <param name="dataA">collider 1</param>
        /// <param name="dataB">collider 2</param>
        /// <returns>true if colliding, false otherwise</returns>
        public bool IsCollision(ColliderData dataA, ColliderData dataB)
        {
            return IsCollision(dataA, dataB, out CollisionContext[] _);
        }

        /// <summary>
        /// pair test using SAT
        /// </summary>
        /// <param name="dataA">collider 1</param>
        /// <param name="dataB">collider 2</param>
        /// <param name="collisionContext">data about the axis and min/max point</param>
        /// <returns>true if colliding, false otherwise</returns>
        public bool IsCollision(ColliderData dataA, ColliderData dataB, out CollisionContext[] collisionContext)
        {
            bool isColliding = true;
            
            PhysVector2[] normalsA = PainColliderHelper.GetNormalsPolygon(dataA);
            PhysVector2[] normalsB = PainColliderHelper.GetNormalsPolygon(dataB);
            
            PhysVector2[] normalsAll = new PhysVector2[normalsA.Length + normalsB.Length];
            normalsA.CopyTo(normalsAll, 0);
            normalsB.CopyTo(normalsAll, normalsA.Length);

            collisionContext = new CollisionContext[normalsA.Length + normalsB.Length];
            for (int i = 0; i < normalsAll.Length; i++)
            {
                PhysVector2 normal = normalsAll[i];
                
                // SAT (separating-axis theorem)
                ProjectPoints(dataA.Verts, normal, out float minA, out float maxA);
                ProjectPoints(dataB.Verts, normal, out float minB, out float maxB);

                collisionContext[i] = new CollisionContext(normal, minA, maxA, minB, maxB);
                if (maxA < minB || maxB < minA)
                {
                    isColliding = false;
                }
            }
                
            return isColliding;
        }

        // project verts onto the axisDir and find the min/max
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
    }
}