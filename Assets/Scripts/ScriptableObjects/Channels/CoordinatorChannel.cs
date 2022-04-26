#if UNITY_EDITOR
using UnityEditor;
using Utils;
#endif
using Data;
using UnityEngine;
using UnityEngine.Events;

namespace ScriptableObjects.Channels
{
    public class CoordinatorChannel : ScriptableObject
    {
        public void GetCoordinates(UnityAction<SphericalCoordinate, Vector3> callback)
        {
            OnGetCoordinates(callback);
        }
        
        public void SetSphericalCoordinate(SphericalCoordinate sphericalCoordinate)
        {
            OnSetSphericalCoordinate(sphericalCoordinate);
        }

        public event UnityAction<UnityAction<SphericalCoordinate, Vector3>> OnGetCoordinates = delegate { };
        public event UnityAction<SphericalCoordinate> OnSetSphericalCoordinate = delegate { };
        
#if UNITY_EDITOR
        [MenuItem("Internal/Create Coordinator Channel")]
        public static void CreateChannel()
        {
            var root = PathUtility.GetSelectedRoot();
            var instance = ScriptableObject.CreateInstance<CoordinatorChannel>();
            AssetDatabase.CreateAsset(instance, $"{root}/coordinatorChannel.asset");
            AssetDatabase.SaveAssets();
        }
#endif
    }
}