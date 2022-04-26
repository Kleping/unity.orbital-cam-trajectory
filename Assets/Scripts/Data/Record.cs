using System;
using System.Collections.Generic;
using UnityEngine;

namespace Data
{
    [Serializable]
    public class Record
    {
        public List<SphericalCoordinate> SphericalCoordinates = new List<SphericalCoordinate>();
        public List<PureVector> Positions = new List<PureVector>();
        public long Binary;


        public Record() { }

        
        public void Add(SphericalCoordinate sc, Vector3 pos)
        {
            SphericalCoordinates.Add(sc);
            Positions.Add(new PureVector(pos));
        }

        public void Clear()
        {
            SphericalCoordinates.Clear();
            Positions.Clear();
        }

        
        public int Count => SphericalCoordinates.Count;
    }
}
