using Data;
using ScriptableObjects.Channels;
using UnityEngine;
using UnityEngine.Events;
using Utils;

namespace Core.Modules
{
    public class Coordinator : MonoBehaviour
    {
        [Header("Channels")]
        [SerializeField] private CoordinatorChannel _channel;

        [Header("Secondary Implementation")]
        [SerializeField] private Transform _rotationCenter;
        [SerializeField] private Camera _cam;

        private Transform _camTransform;
        private SphericalCoordinate _sphericalCoordinate;
        
        
        private void Awake()
        {
            _channel.OnGetCoordinates += onGetCoordinates;
            _channel.OnSetSphericalCoordinate += onSetSphericalCoordinate;
        }

        private void Start()
        {
            _sphericalCoordinate = new SphericalCoordinate(0F, 0F);
            _camTransform = _cam.transform;
        }

        private void OnDestroy()
        {
            _channel.OnGetCoordinates -= onGetCoordinates;
            _channel.OnSetSphericalCoordinate -= onSetSphericalCoordinate;
        }
        

        private void onSetSphericalCoordinate(SphericalCoordinate sphericalCoordinate)
        {
            _sphericalCoordinate = sphericalCoordinate;
            _rotationCenter.rotation = _sphericalCoordinate.ToQuaternion();
        }

        private void onGetCoordinates(UnityAction<SphericalCoordinate, Vector3> callback)
        {
            callback(_sphericalCoordinate, _camTransform.position);
        }
    }
}
