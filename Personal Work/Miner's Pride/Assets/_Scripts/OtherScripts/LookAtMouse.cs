using UnityEngine;

public class LookAtMouse : MonoBehaviour {
    
    #region Variables
    [Header("Variables")]
    private Vector3 _mousePosition;
    private Vector3 _lookDirection;
    
    #endregion
    
    #region Components
    //[Header("Components")]
    #endregion
    
    #region Unity Methods
    private void Update() {
        _mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        
        _lookDirection = new Vector2(_mousePosition.x - transform.position.x, _mousePosition.y - transform.position.y);
        transform.up = _lookDirection;
        
    }
    #endregion
    
    #region Other Methods
    
    #endregion
}
