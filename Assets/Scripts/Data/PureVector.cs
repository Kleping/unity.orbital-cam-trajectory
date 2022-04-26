using UnityEngine;

namespace Data
{
    public readonly struct PureVector
    {
        public readonly float x, y, z;

        public PureVector(Vector3 vector)
        {
            x = vector.x;
            y = vector.y;
            z = vector.z;
        }

        public readonly Vector3 ToVector3()
        {
            return new Vector3(x, y, z);
        }
    }
}
