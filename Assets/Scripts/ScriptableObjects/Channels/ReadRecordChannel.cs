#if UNITY_EDITOR
using UnityEditor;
using Utils;
#endif
using Data;
using UnityEngine;
using UnityEngine.Events;

namespace ScriptableObjects.Channels
{
    public class ReadRecordChannel : ScriptableObject
    {
        public void Call(long binary, UnityAction<Record> callback) { Event?.Invoke(binary, callback); }

        public event UnityAction<long, UnityAction<Record>> Event = delegate { };
        
#if UNITY_EDITOR
        [MenuItem("Internal/Create Binary Channel")]
        public static void CreateChannel()
        {
            var root = PathUtility.GetSelectedRoot();
            var instance = ScriptableObject.CreateInstance<ReadRecordChannel>();
            AssetDatabase.CreateAsset(instance, $"{root}/readRecord.asset");
            AssetDatabase.SaveAssets();
        }
#endif
    }
}