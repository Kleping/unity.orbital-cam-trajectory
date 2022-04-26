using System.Collections;
using ScriptableObjects.Channels;
using UnityEngine;

namespace Core.Modules
{
    public class Recorder : MonoBehaviour
    {
        [Header("Channels")]
        [SerializeField] private StorageChannel _storageChannel;
        [SerializeField] private RecordListChannel _recordListChannel;
        [SerializeField] private CoordinatorChannel _coordinatorChannel;

        [Header("Clicks")]
        [SerializeField] private ClickChannel _record;
        [SerializeField] private ClickChannel _stopRecord;

        
        private Coroutine _ticker;
        private bool _started;
    
    
        private void Awake()
        {
            _record.OnClick += onRecordClicked;
            _stopRecord.OnClick += onStopRecordClicked;
        }

        private void OnDestroy()
        {
            _record.OnClick -= onRecordClicked;
            _stopRecord.OnClick -= onStopRecordClicked;
        }

        private void play()
        {
            _storageChannel.InitRecord();
            _started = true;
            StartCoroutine(record());
        }

        
        private IEnumerator record()
        {
            while (_started)
            {
                yield return new WaitForEndOfFrame();
                _coordinatorChannel.GetCoordinates((sc, camPos) =>
                {
                    _storageChannel.AddCoordinates(sc, camPos);
                });
            }
            
            _storageChannel.WriteCurrentRecord(record => _recordListChannel.AddWrittenRecord(record));
        }

        private void onStopRecordClicked()
        {
            _started = false;
        }

        private void onRecordClicked()
        {
            if (!_started) play();
        }
    }
}
