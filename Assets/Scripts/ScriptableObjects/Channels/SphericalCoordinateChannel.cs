#if UNITY_EDITOR
using UnityEditor;
using Utils;
#endif
using Data;
using UnityEngine;
using UnityEngine.Events;

namespace ScriptableObjects.Channels
{
    public class SphericalCoordinateChannel : ScriptableObject
    {
        public void Call(SphericalCoordinate sc, Vector3 pos)
        {
            Event(sc, pos);
        }

        public event UnityAction<SphericalCoordinate, Vector3> Event = delegate { };
        
        #if UNITY_EDITOR
        [MenuItem("Internal/Create Spherical Coordinate Channel")]
        public static void CreateChannel()
        {
            var root = PathUtility.GetSelectedRoot();
            var instance = ScriptableObject.CreateInstance<SphericalCoordinateChannel>();
            AssetDatabase.CreateAsset(instance, $"{root}/sphericalCoordinate.asset");
            AssetDatabase.SaveAssets();
        }
        #endif
    }
}
