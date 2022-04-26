#if UNITY_EDITOR
using UnityEditor;
using Utils;
#endif
using Data;
using UnityEngine;
using UnityEngine.Events;

namespace ScriptableObjects.Channels
{
    public class PlaybackChannel : ScriptableObject
    {
        public void Play(Record record)
        {
            OnPlay(record);
        }
        
        public void Stop()
        {
            OnStop();
        }

        public void RefreshProgress(float progress)
        {
            OnRefreshProgress(progress);
        }

        
        public event UnityAction<Record> OnPlay = delegate { };
        public event UnityAction<float> OnRefreshProgress = delegate { };
        public event UnityAction OnStop = delegate { };
        
#if UNITY_EDITOR
        [MenuItem("Internal/Create Playback Channel")]
        public static void CreateChannel()
        {
            var root = PathUtility.GetSelectedRoot();
            var instance = ScriptableObject.CreateInstance<PlaybackChannel>();
            AssetDatabase.CreateAsset(instance, $"{root}/playbackChannel.asset");
            AssetDatabase.SaveAssets();
        }
#endif
    }
}