using UnityEngine;

public class PickaxeBoomerang : MonoBehaviour {
    
    #region Variables
    [Header("Variables")]
    [SerializeField] private float _speed;
    [SerializeField] private float _maxDistance = 10f;
    private Vector2 _startPosition;
    #endregion
    
    #region Components
    //[Header("Components")]
    #endregion
    
    #region Unity Methods
    private void Awake() {
        _startPosition = transform.position;
    }

    private void Update() {
        transform.Translate(Vector2.up * _speed * Time.deltaTime);
        if (Vector2.Distance(_startPosition, transform.position) >= _maxDistance) {
            Destroy(gameObject);
        }
    }
    #endregion
    
    #region Other Methods
    
    #endregion
}
