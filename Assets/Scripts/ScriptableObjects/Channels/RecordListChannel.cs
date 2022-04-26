#if UNITY_EDITOR
using UnityEditor;
using Utils;
#endif
using Data;
using UnityEngine;
using UnityEngine.Events;

namespace ScriptableObjects.Channels
{
    public class RecordListChannel : ScriptableObject
    {
        public void AddWrittenRecord(Record record)
        {
            OnAddWrittenRecord(record);
        }

        public event UnityAction<Record> OnAddWrittenRecord = delegate { };
        
#if UNITY_EDITOR
        [MenuItem("Internal/Create Record List Channel")]
        public static void CreateChannel()
        {
            var root = PathUtility.GetSelectedRoot();
            var instance = ScriptableObject.CreateInstance<RecordListChannel>();
            AssetDatabase.CreateAsset(instance, $"{root}/recordListChannel.asset");
            AssetDatabase.SaveAssets();
        }
#endif
    }
}