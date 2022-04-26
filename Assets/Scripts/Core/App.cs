using ScriptableObjects.Channels;
using UnityEngine;

namespace Core
{
    public class App : MonoBehaviour
    {
        [Header("Channels")]
        [SerializeField] private LoaderChannel _loaderChannel;
        
        private void Start()
        {
            DontDestroyOnLoad(this);
            _loaderChannel.LoadScene(SceneBuildIndexList.SampleScene);
        }
    }
}
