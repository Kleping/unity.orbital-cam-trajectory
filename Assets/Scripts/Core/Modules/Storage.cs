using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Data;
using Newtonsoft.Json;
using ScriptableObjects.Channels;
using UnityEngine;
using UnityEngine.Events;

namespace Core.Modules
{
    public class Storage : MonoBehaviour
    {
        [Header("Channels")]
        [SerializeField] private StorageChannel _channel;

        private Record _currentRecord;

        
        private void Awake()
        {
            _channel.OnReadOne += readOne;
            _channel.OnWriteRecord += writeRecord;
            _channel.OnRead += read;
            _channel.OnSegregatedRead += segregatedRead;
            _channel.OnInitRecord += onInitRecord;
            _channel.OnWriteCurrentRecord += onWriteCurrentRecord;
            _channel.OnAddCoordinates += onAddCoordinates;
            _channel.OnGetCurrentRecord += onGetCurrentRecord;
            _channel.OnSetCurrentRecord += onSetCurrentRecord;
        }

        private void Start()
        {
            _currentRecord = new Record();
        }

        private void OnDestroy()
        {
            _channel.OnReadOne -= readOne;
            _channel.OnWriteRecord -= writeRecord;
            _channel.OnRead -= read;
            _channel.OnSegregatedRead -= segregatedRead;
            _channel.OnInitRecord -= onInitRecord;
            _channel.OnWriteCurrentRecord -= onWriteCurrentRecord;
            _channel.OnAddCoordinates -= onAddCoordinates;
            _channel.OnGetCurrentRecord -= onGetCurrentRecord;
            _channel.OnSetCurrentRecord -= onSetCurrentRecord;

        }
        
#if UNITY_EDITOR
        private void OnDrawGizmos()
        {
            if (_currentRecord == null) return;
            var positions = _currentRecord.Positions;
            for (var i = 1; i < positions.Count; i++)
            {
                var i1 = i - 1;
                var a = positions[i1].ToVector3();
                var b = positions[i].ToVector3();
                Gizmos.DrawLine(a, b);
            }
        }
#endif

        
        private static async void writeRecord(Record record, UnityAction callback)
        {
            var binary = DateTime.Now.ToBinary();
            record.Binary = binary;
            var data = JsonConvert.SerializeObject(record, new JsonSerializerSettings
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore
            });
            var path = $"{Application.persistentDataPath}/{binary}.record";
            await using var w = new StreamWriter(path);
            await w.WriteAsync(data);
            callback();
        }

        private static async void readOne(long binary, UnityAction<Record> callback)
        {
            var record = await readRecord(binary);
            callback(record);
        }

        private static async void read(UnityAction<List<Record>> callback)
        {
            var records = new List<Record>();
            await segregatedReadInternal(record => records.Add(record));
            callback(records);
        }

        private static async void segregatedRead(UnityAction<Record> callback)
        {
            await segregatedReadInternal(callback);
        }
        
        private static async Task segregatedReadInternal(UnityAction<Record> callback)
        {
            var files = new DirectoryInfo(Application.persistentDataPath).GetFiles();
            var binaries = files.Select(f => 
                long.Parse(f.Name.Replace(".record", string.Empty))).ToList();

            foreach (var binary in binaries)
            {
                var record = await readRecord(binary);
                if (record == null) continue;
                callback(record);
            };
        }
        
        private static async Task<Record> readRecord(long binary)
        {
            try
            {
                var path = $"{Application.persistentDataPath}/{binary}.record";
                using var r = new StreamReader(path);
                
                var data = await r.ReadToEndAsync();
                // while (!task.IsCompletedSuccessfully) await Task.Yield();
                // var data = task.Result;
            
                return JsonConvert.DeserializeObject<Record>(data);
            }
            catch
            {
                return null;
            }
        }

        
        private void onWriteCurrentRecord(UnityAction<Record> callback)
        {
            writeRecord(_currentRecord, () => callback(_currentRecord));
        }

        private void onAddCoordinates(SphericalCoordinate sc, Vector3 camPos)
        {
            _currentRecord.Add(sc, camPos);
        }
        
        private void onInitRecord()
        {
            _currentRecord.Clear();
        }

        private void onGetCurrentRecord(UnityAction<Record> callback)
        {
            callback(_currentRecord);
        }
        
        private void onSetCurrentRecord(Record record)
        {
            _currentRecord = record;
        }
    }
}
