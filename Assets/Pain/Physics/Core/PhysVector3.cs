using System;
using UnityEngine;

namespace Pain.Physics.Core
{
    [Serializable]
    public struct PhysVector3
    {
        public float x;
        public float y;
        public float z;

        public PhysVector3(float x, float y, float z)
        {
            this.x = x;
            this.y = y;
            this.z = z;
        }
        
        public static PhysVector3 one  = new PhysVector3(1f, 1f, 1f);
        public static PhysVector3 zero = new PhysVector3(0f, 0f, 0f);
        
        #region Operators
        public static implicit operator PhysVector3(Vector3 v)
        {
            return new PhysVector3(v.x, v.y, v.z);
        }
        
        public static implicit operator Vector3(PhysVector3 v)
        {
            return new Vector3(v.x, v.y, v.z);
        }
        
        public static PhysVector3 operator +(PhysVector3 a, PhysVector3 b)
        {
            return new PhysVector3(a.x + b.x, a.y + b.y, a.z + b.z);
        }
        
        public static PhysVector3 operator +(PhysVector3 a, Vector3 b)
        {
            return new PhysVector3(a.x + b.x, a.y + b.y, a.z + b.z);
        }
        
        public static PhysVector3 operator +(Vector3 a, PhysVector3 b)
        {
            return new PhysVector3(a.x + b.x, a.y + b.y, a.z + b.z);
        }
        
        public static PhysVector3 operator -(PhysVector3 a, PhysVector3 b)
        {
            return new PhysVector3(a.x - b.x, a.y - b.y, a.z - b.z);
        }

        public static PhysVector3 operator /(PhysVector3 v, float x)
        {
            return new PhysVector3(v.x / x, v.y / x, v.z / x);
        }
        
        public static PhysVector3 operator *(PhysVector3 v, float x)
        {
            return new PhysVector3(v.x * x, v.y * x, v.z * x);
        }
        #endregion

        public float magnitude => Mathf.Sqrt(x * x + y * y + z * z);
        public PhysVector3 inversed => new PhysVector3(-x, -y, -z);
        public PhysVector3 normalized
        {
            get
            {
                float mag = magnitude;
                if (mag == 0f) return zero;
                return new PhysVector3(x / mag, y / mag, z / mag);
            }
        }

        public override string ToString()
        {
            return $"({x}, {y}, {z})";
        }
    }
}