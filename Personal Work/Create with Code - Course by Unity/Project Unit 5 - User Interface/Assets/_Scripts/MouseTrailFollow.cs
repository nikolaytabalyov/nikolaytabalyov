using System.Threading;
using UnityEngine;

namespace NikolayTabalyov{
    public class MouseTrailFollow : MonoBehaviour {
        
        [Header("Variables")]
        [Header("Components")]
        private GameManager _gameManager;
        private Camera _mainCamera;
        private TrailRenderer _trailRenderer;

        #region Unity Methods
        private void Awake() {
            _gameManager = GameManager.Instance;
            _mainCamera = Camera.main;
            _trailRenderer = GetComponent<TrailRenderer>();
        }
        private void Start() {
            MoveTrail();
        }
        #endregion

        #region Other Methods
        private void MoveTrail() {
            float xPos = _mainCamera.ScreenToWorldPoint(Input.mousePosition).x;
            float yPos = _mainCamera.ScreenToWorldPoint(Input.mousePosition).y;
            transform.position = new Vector3(xPos, yPos, 0);
        }
        private void Update() {
            if (Input.GetMouseButton(0)) {
                MoveTrail();
            } 
            if (Input.GetMouseButtonUp(0)) {
                _gameManager.isSwiping = false;
                Destroy(gameObject);
            } 
        }
        #endregion
    }
}
