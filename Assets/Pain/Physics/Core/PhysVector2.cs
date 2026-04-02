using System;
using UnityEngine;

namespace Pain.Physics.Core
{
    [Serializable]
    public struct PhysVector2
    {
        public float x;
        public float y;

        public PhysVector2(float x, float y)
        {
            this.x = x;
            this.y = y;
        }
        
        public static PhysVector2 one  = new PhysVector2(1f, 1f);
        public static PhysVector2 zero = new PhysVector2(0f, 0f);
        
        #region Operators
        public static implicit operator PhysVector2(Vector2 v)
        {
            return new PhysVector2(v.x, v.y);
        }
        
        public static implicit operator Vector2(PhysVector2 v)
        {
            return new Vector2(v.x, v.y);
        }
        
        public static implicit operator Vector3(PhysVector2 v)
        {
            return new Vector3(v.x, v.y, 0);
        }
        
        public static PhysVector2 operator +(PhysVector2 a, PhysVector2 b)
        {
            return new PhysVector2(a.x + b.x, a.y + b.y);
        }
        
        public static PhysVector2 operator +(PhysVector2 a, Vector2 b)
        {
            return new PhysVector2(a.x + b.x, a.y + b.y);
        }
        
        public static PhysVector2 operator +(Vector2 a, PhysVector2 b)
        {
            return new PhysVector2(a.x + b.x, a.y + b.y);
        }
        
        public static PhysVector2 operator -(PhysVector2 a, PhysVector2 b)
        {
            return new PhysVector2(a.x - b.x, a.y - b.y);
        }

        public static PhysVector2 operator /(PhysVector2 v, float x)
        {
            return new PhysVector2(v.x / x, v.y / x);
        }
        
        public static PhysVector2 operator *(PhysVector2 v, float x)
        {
            return new PhysVector2(v.x * x, v.y * x);
        }
        #endregion

        public float magnitude => Mathf.Sqrt(x * x + y * y);
        public PhysVector2 inversed => new PhysVector2(-x, -y);
        public PhysVector2 normalized
        {
            get
            {
                float mag = magnitude;
                if (mag == 0f) return zero;
                return new PhysVector2(x / mag, y / mag);
            }
        }

        public override string ToString()
        {
            return $"({x}, {y})";
        }
    }
}