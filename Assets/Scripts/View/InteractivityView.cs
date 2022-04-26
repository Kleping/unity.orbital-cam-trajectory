using ScriptableObjects.Channels;
using UnityEngine;

namespace View
{
    public class InteractivityView : MonoBehaviour
    {
        [Header("Clicks")]
        [SerializeField] private ClickChannel _stopRecord;
        [SerializeField] private ClickChannel _playback;
        [SerializeField] private ClickChannel _record;

        [Header("Secondary Implementation")]
        [SerializeField] private GameObject _buttonStopRecord;
        [SerializeField] private GameObject _buttonPlayback;
        [SerializeField] private GameObject _buttonRecord;


        private void Awake()
        {
            setState(false, false, true);

            _stopRecord.OnClick += onStopRecordClicked;
            _playback.OnClick += onPlaybackClicked;
            _record.OnClick += onRecordClicked;
        }

        private void OnDestroy()
        {
            _stopRecord.OnClick -= onStopRecordClicked;
            _playback.OnClick -= onPlaybackClicked;
            _record.OnClick -= onRecordClicked;
        }
        
        
        private void onStopRecordClicked()
        {
            setState(false, true, true);
        }

        private void onPlaybackClicked()
        {
            setState(false, false, false);
        }

        private void onRecordClicked()
        {
            setState(true, false, false);
        }
        
        
        private void setState(bool isStopRecord, bool isPlayback, bool isRecord)
        {
            _buttonStopRecord.SetActive(isStopRecord);
            _buttonPlayback.SetActive(isPlayback);
            _buttonRecord.SetActive(isRecord);
        }
    }
}
