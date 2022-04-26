#if UNITY_EDITOR
using UnityEditor;
using Utils;
#endif
using UnityEngine;
using UnityEngine.Events;

namespace ScriptableObjects.Channels
{
    public class ClickChannel : ScriptableObject
    {
        public void Click() { OnClick(); }

        public event UnityAction OnClick = delegate { };
        
#if UNITY_EDITOR
        [MenuItem("Internal/Create Click Channel")]
        public static void CreateChannel()
        {
            var root = PathUtility.GetSelectedRoot();
            var instance = ScriptableObject.CreateInstance<ClickChannel>();
            AssetDatabase.CreateAsset(instance, $"{root}/clickChannel.asset");
            AssetDatabase.SaveAssets();
        }
#endif
    }
}