#if UNITY_EDITOR
using UnityEditor;
using Utils;
#endif
using Core;
using UnityEngine;
using UnityEngine.Events;

namespace ScriptableObjects.Channels
{
    public class LoaderChannel : ScriptableObject
    {
        public void LoadScene(SceneBuildIndexList scene) => OnLoadScene((int) scene);

        public event UnityAction<int> OnLoadScene = delegate { };
        
#if UNITY_EDITOR
        [MenuItem("Internal/Create Loader Channel")]
        public static void CreateChannel()
        {
            var root = PathUtility.GetSelectedRoot();
            var instance = ScriptableObject.CreateInstance<LoaderChannel>();
            AssetDatabase.CreateAsset(instance, $"{root}/loaderChannel.asset");
            AssetDatabase.SaveAssets();
        }
#endif
    }
}