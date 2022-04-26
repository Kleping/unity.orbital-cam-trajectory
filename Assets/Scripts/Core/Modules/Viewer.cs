using Data;
using ScriptableObjects.Channels;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Core.Modules
{
    public class Viewer : MonoBehaviour
    {
        [Header("Channels")]
        [SerializeField] private ViewerChannel _channel;
        [SerializeField] private CoordinatorChannel _coordinatorChannel;
        
        [Header("Secondary Implementation"), Range(0, 1F)]
        [SerializeField] private float _speedCoefficient;
        [SerializeField] private bool _locked;

        private const string AxisHorizontal = "Mouse X";
        private const string AxisVertical   = "Mouse Y";
    
        private const float ExtraSpeed = 900F;
        private const float BaseSpeed  = 100F;

        private void Awake()
        {
            _channel.OnSetLocked += onSetLocked;
        }

        private void OnDestroy()
        {
            _channel.OnSetLocked -= onSetLocked;
        }
        
        private void Update()
        {
            if (!isAvailable()) return;
            _coordinatorChannel.GetCoordinates((sc, camPos) =>
            {
                if (!isAvailable()) return;
                sc = calculateSphericalCoordinate(sc, calculateSpeed(_speedCoefficient));
                _coordinatorChannel.SetSphericalCoordinate(sc);
            });
        }

        #region Responsibility
        private static SphericalCoordinate calculateSphericalCoordinate(SphericalCoordinate sc, float speed)
        {
            sc.Polar += calculateAxis(getAxis(AxisHorizontal), speed);
            sc.Elevation -= calculateAxis(getAxis(AxisVertical),   speed);
            sc.Elevation  = Mathf.Clamp(sc.Elevation, 0F, 90F);
            return sc;
        }

        private static float calculateAxis(float axis, float speed)
        {
            return axis * speed * Mathf.Deg2Rad;
        }
    
        private static float calculateSpeed(float speedCoefficient)
        {
            return BaseSpeed + ExtraSpeed * speedCoefficient;
        }

        private static float getAxis(string axisName)
        {
            if (Input.touchCount <= 0) return Input.GetAxis(axisName);

            // Probably this body could be failed on a mobile platform.
            {
                var dp = Input.touches[0].deltaPosition;
                return axisName == AxisHorizontal ? dp.x : dp.y;   
            }
        }

        private bool isAvailable()
        {
            var pointedToUI = EventSystem.current.IsPointerOverGameObject();
            if (!Input.anyKey || pointedToUI) return false;
            return !_locked;
        }
        #endregion

        #region Channels
        private void onSetLocked(bool locked)
        {
            _locked = locked;
        }
        #endregion
    }
}
