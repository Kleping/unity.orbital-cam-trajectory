using ScriptableObjects.Channels;
using UnityEngine;
using UnityEngine.UI;

namespace View
{
    public class PlaybackView : MonoBehaviour
    {
        [Header("Channels")]
        [SerializeField] private StorageChannel _storageChannel;
        [SerializeField] private PlaybackChannel _playbackChannel;

        [Header("Secondary Implementation")]
        [SerializeField] private Image _filled;


        public void Awake()
        {
            _playbackChannel.OnRefreshProgress += onRefreshProgress;
        }
        
        public void Start()
        {
            _storageChannel.GetCurrentRecord(_playbackChannel.Play);
        }

        public void OnDestroy()
        {
            _playbackChannel.OnRefreshProgress -= onRefreshProgress;
        }

        
        private void onRefreshProgress(float progress)
        {
            _filled.fillAmount = progress;
        }
    }
}
