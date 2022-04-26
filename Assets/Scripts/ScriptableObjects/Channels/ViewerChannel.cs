
using UnityEngine;
using UnityEngine.Events;
#if UNITY_EDITOR
using UnityEditor;
using Utils;
#endif

namespace ScriptableObjects.Channels
{
    public class ViewerChannel : ScriptableObject
    {
        public void SetLocked(bool locked)
        {
            OnSetLocked(locked);
        }
        
        public event UnityAction<bool> OnSetLocked = delegate { };
        
#if UNITY_EDITOR
        [MenuItem("Internal/Create Viewer Channel")]
        public static void CreateChannel()
        {
            var root = PathUtility.GetSelectedRoot();
            var instance = CreateInstance<ViewerChannel>();
            AssetDatabase.CreateAsset(instance, $"{root}/viewerChannel.asset");
            AssetDatabase.SaveAssets();
        }
#endif
    }
}