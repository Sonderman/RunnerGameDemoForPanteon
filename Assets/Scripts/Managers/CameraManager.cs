using UnityEngine;
using Cinemachine;

namespace Managers
{
    public class CameraManager : MonoBehaviour
    {
        [SerializeField] private CinemachineVirtualCamera cinemachineVirtualCamera;
        private Transform _defaultLookAtTransform;

        private void Start()
        {
            _defaultLookAtTransform = cinemachineVirtualCamera.LookAt;
        }

        public void LookAt(Transform tr)
        {
            cinemachineVirtualCamera.LookAt = tr;
        }

        public void Reset()
        {
            cinemachineVirtualCamera.LookAt = _defaultLookAtTransform;
        }
    }
}