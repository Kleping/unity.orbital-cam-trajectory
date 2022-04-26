using System.Collections;
using Data;
using ScriptableObjects.Channels;
using UnityEngine;

namespace Core.Modules
{
    public class Playback : MonoBehaviour
    {
        [Header("Channels")]
        [SerializeField] private PlaybackChannel _channel;
        [SerializeField] private CoordinatorChannel _coordinatorChannel;
        [SerializeField] private ViewerChannel _viewerChannel;
        [SerializeField] private LoaderChannel _loaderChannel;
        
        [Header("Clicks")]
        [SerializeField] private ClickChannel _playback;
        [SerializeField] private ClickChannel _stopPlayback;
        
        private Coroutine _coroutinePlayback;
        
        
        public void Awake()
        {
            _stopPlayback.OnClick += onStopPlaybackClicked;
            _playback.OnClick += onPlaybackClicked;
            _channel.OnPlay += onPlay;
            _channel.OnStop += onStop;
        }

        public void OnDestroy()
        {
            _stopPlayback.OnClick -= onStopPlaybackClicked;
            _playback.OnClick -= onPlaybackClicked;
            _channel.OnPlay -= onPlay;
            _channel.OnStop -= onStop;
        }
        

        private IEnumerator playRecord(Record record)
        {
            for (var i = 0; i < record.Count; i++)
            {
                _coordinatorChannel.SetSphericalCoordinate(record.SphericalCoordinates[i]);
                _channel.RefreshProgress((i + 1) / (float) record.Count);
                yield return new WaitForEndOfFrame();
            }

            _stopPlayback.Click();
        }

        private void play(Record record)
        {
            _viewerChannel.SetLocked(true);
            if (_coroutinePlayback != null)
            { 
                StopCoroutine(_coroutinePlayback);
                _coroutinePlayback = null;
            }
            _coroutinePlayback = StartCoroutine(playRecord(record));
        }
        
        private void stop()
        {
            if (_coroutinePlayback == null) return;
            StopCoroutine(_coroutinePlayback);
            _loaderChannel.LoadScene(SceneBuildIndexList.SampleScene);
            _viewerChannel.SetLocked(false);
            _coroutinePlayback = null;
        }
        
            
        #region Channels
        private void onStop() => stop();

        private void onStopPlaybackClicked() => stop();

        private void onPlaybackClicked() => _loaderChannel.LoadScene(SceneBuildIndexList.Playback);

        private void onPlay(Record record) => play(record);

        #endregion
    }
}
