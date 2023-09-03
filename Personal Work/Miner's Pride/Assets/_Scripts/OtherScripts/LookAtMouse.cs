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
    private void FixedUpdate() {
        // _mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        
        // _lookDirection = new Vector2(_mousePosition.x - transform.position.x, _mousePosition.y - transform.position.y);
        
        _lookDirection = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        float angle = (Mathf.Atan2(_lookDirection.y, _lookDirection.x) * Mathf.Rad2Deg) - 90f;
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
    }
    #endregion
    
    #region Other Methods
    
    #endregion
}
