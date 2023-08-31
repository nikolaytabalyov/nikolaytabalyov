using UnityEngine;

namespace NikolayTabalyov
{
    public class LookAtCamera : MonoBehaviour {
    
        #region Variables
        private enum LookAtCameraMode {
            LookAtCamera,
            LookAtCameraInverted,
            CameraForward, 
            CameraForwardInverted
        }
        [Header("Variables")]
        [SerializeField] private LookAtCameraMode _lookAtCameraMode;
        #endregion
    
        #region Components
        //[Header("Components")]
        #endregion
    
        #region Unity Methods
        private void LateUpdate() {
            switch (_lookAtCameraMode) {
                case LookAtCameraMode.LookAtCamera:
                    transform.LookAt(Camera.main.transform);
                    break;
                case LookAtCameraMode.LookAtCameraInverted:
                    Vector3 dirFromCamera = transform.position - Camera.main.transform.position;
                    transform.LookAt(transform.position + dirFromCamera);
                    break;
                case LookAtCameraMode.CameraForward:
                    transform.forward = Camera.main.transform.forward;
                    break;
                case LookAtCameraMode.CameraForwardInverted:
                    transform.forward = -Camera.main.transform.forward;
                    break;
            }
        }
        #endregion
    
        #region Other Methods
    
        #endregion
    }
}
