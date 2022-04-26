using Data;
using UnityEngine;

namespace Utils
{
    public static class SphericalCoordinateUtility
    {
        public static Quaternion ToQuaternion(this SphericalCoordinate sc)
        {
            var q = Quaternion.AngleAxis(sc.Elevation, Vector3.right);
            q = Quaternion.AngleAxis(sc.Polar, Vector3.up) * q;
            return q;
        }
    }
}
