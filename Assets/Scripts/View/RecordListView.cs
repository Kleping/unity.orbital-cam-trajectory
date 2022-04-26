using System.Collections.Generic;
using Core;
using Data;
using ScriptableObjects.Channels;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace View
{
    public class RecordListView : MonoBehaviour
    {
        [Header("Channels")]
        [SerializeField] private RecordListChannel _channel;
        [SerializeField] private StorageChannel _storageChannel;
        [SerializeField] private LoaderChannel _loaderChannel;

        [Header("Secondary Implementation")]
        [SerializeField] private GameObject _refRecordItem;
        [SerializeField] private ScrollRect _scrollRect;

        private readonly List<IRecordItem> _items = new List<IRecordItem>();


        private void Awake()
        {
            _channel.OnAddWrittenRecord += onAddWrittenRecord;
        }
        
        private void Start()
        {
            _storageChannel.SegregatedRead(record => _items.Add(addItem(record, false)));
        }

        private void OnDestroy()
        {
            _channel.OnAddWrittenRecord -= onAddWrittenRecord;
        }
        

        private void onRecordItemClicked(Record record)
        {
            _storageChannel.SetCurrentRecord(record);
            _loaderChannel.LoadScene(SceneBuildIndexList.Playback);
        }

        private IRecordItem addItem(Record record, bool setAsFirstSibling)
        {
            var clone = Instantiate(_refRecordItem, _scrollRect.content).transform;
            if (setAsFirstSibling) clone.SetAsFirstSibling();
            var item = clone.GetComponent<IRecordItem>();
            item.Init(record.Binary, () => onRecordItemClicked(record));
            return item;
        }

        private void onAddWrittenRecord(Record record) => _items.Insert(0, addItem(record, true));
    }
}
