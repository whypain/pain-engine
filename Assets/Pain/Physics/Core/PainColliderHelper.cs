using Pain.Physics.Objects;

namespace Pain.Physics.Core
{
    public struct ColliderData
    {
        public PhysVector2[] Verts;
    }
    
    public class PainColliderHelper
    {
        public static PhysVector2[] GetNormalsPolygon(ColliderData coll)
        {
            PhysVector2[] verts = coll.Verts;
            if (verts == null) return null;
            if (verts.Length == 0) return null;
            
            PhysVector2[] normals = new PhysVector2[verts.Length];

            for (int i = 0; i < verts.Length; i++)
            {
                PhysVector2 diff = verts[(i + 1) % verts.Length] - verts[i];
                PhysVector2 normal = new PhysVector2(diff.y, -diff.x);
                normals[i] = normal.normalized;
            }

            return normals;
        }
    }
}