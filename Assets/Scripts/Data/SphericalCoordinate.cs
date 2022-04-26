using System;

namespace Data
{
    [Serializable]
    public struct SphericalCoordinate
    {
        public float Polar, Elevation;

        public SphericalCoordinate(float polar, float elevation)
        {
            Polar = polar;
            Elevation = elevation;
        }
    }
}
