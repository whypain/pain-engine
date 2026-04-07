namespace Pain.Physics.Core
{
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

        public bool TestCollision(ColliderData collA, ColliderData collB)
        {
            return true;
        }
    }
}