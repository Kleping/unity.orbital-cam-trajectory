using ScriptableObjects.Channels;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Core.Modules
{
    public class Loader : MonoBehaviour
    {
        [Header("Channels")]
        [SerializeField] private LoaderChannel _channel;

        public void Awake()
        {
            _channel.OnLoadScene += onLoadScene;
        }

        public void OnDestroy()
        {
            _channel.OnLoadScene -= onLoadScene;
        }

        private static void onLoadScene(int sceneBuildIndex)
        {
            SceneManager.LoadScene(sceneBuildIndex);
        }
    }
}
