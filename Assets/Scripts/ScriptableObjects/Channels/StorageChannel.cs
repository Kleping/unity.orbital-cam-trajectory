#if UNITY_EDITOR
using UnityEditor;
using Utils;
#endif
using System.Collections.Generic;
using Data;
using UnityEngine;
using UnityEngine.Events;

namespace ScriptableObjects.Channels
{
    public class StorageChannel : ScriptableObject
    {
        public void ReadOne(long binary, UnityAction<Record> callback)
        {
            OnReadOne(binary, callback);
        }
        
        public void WriteRecord(Record record, UnityAction callback)
        {
            OnWriteRecord(record, callback);
        }
        
        public void WriteCurrentRecord(UnityAction<Record> callback)
        {
            OnWriteCurrentRecord(callback);
        }

        public void Read(UnityAction<List<Record>> callback)
        {
            OnRead(callback);
        }

        public void SegregatedRead(UnityAction<Record> callback)
        {
            OnSegregatedRead(callback);
        }

        public event UnityAction<long, UnityAction<Record>> OnReadOne = delegate { };
        public event UnityAction<Record, UnityAction> OnWriteRecord = delegate { };
        public event UnityAction<UnityAction<Record>> OnWriteCurrentRecord = delegate { };
        public event UnityAction<UnityAction<List<Record>>> OnRead = delegate { };
        public event UnityAction<UnityAction<Record>> OnSegregatedRead = delegate { };
        
        
        public void InitRecord()
        {
            OnInitRecord();
        }
        
        public void AddCoordinates(SphericalCoordinate sc, Vector3 camPos)
        {
            OnAddCoordinates(sc, camPos);
        }
        
        public void GetCurrentRecord(UnityAction<Record> callback)
        {
            OnGetCurrentRecord(callback);
        }
        
        public void SetCurrentRecord(Record record)
        {
            OnSetCurrentRecord(record);
        }
        
        public event UnityAction OnInitRecord = delegate { };
        public event UnityAction<SphericalCoordinate, Vector3> OnAddCoordinates = delegate { };
        public event UnityAction<UnityAction<Record>> OnGetCurrentRecord = delegate { };
        public event UnityAction<Record> OnSetCurrentRecord = delegate { };

        
#if UNITY_EDITOR
        [MenuItem("Internal/Create Storage Channel")]
        public static void CreateChannel()
        {
            var root = PathUtility.GetSelectedRoot();
            var instance = ScriptableObject.CreateInstance<StorageChannel>();
            AssetDatabase.CreateAsset(instance, $"{root}/storageChannel.asset");
            AssetDatabase.SaveAssets();
        }
#endif
    }
}