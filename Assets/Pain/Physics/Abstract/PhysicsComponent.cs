using Pain.Physics.Objects;
using UnityEngine;

namespace Pain.Physics.Abstract
{
    public interface IPhysicsComponent
    {
        PhysicsObject OnApply(in PhysicsObject physicsObject);
    }
    
    public abstract class PhysicsComponent : MonoBehaviour, IPhysicsComponent
    {
        public abstract PhysicsObject OnApply(in PhysicsObject physicsObject);
    }
}