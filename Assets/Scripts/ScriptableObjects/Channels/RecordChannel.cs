#if UNITY_EDITOR
using UnityEditor;
using Utils;
#endif
using Data;
using UnityEngine;
using UnityEngine.Events;

namespace ScriptableObjects.Channels
{
    public class RecordChannel : ScriptableObject
    {
        public void Call(Record record)
        {
            Event(record);
        }

        public event UnityAction<Record> Event = delegate { };
        
        #if UNITY_EDITOR
        [MenuItem("Internal/Create Record Channel")]
        public static void CreateChannel()
        {
            var root = PathUtility.GetSelectedRoot();
            var instance = ScriptableObject.CreateInstance<RecordChannel>();
            AssetDatabase.CreateAsset(instance, $"{root}/recordChannel.asset");
            AssetDatabase.SaveAssets();
        }
        #endif
    }
}
