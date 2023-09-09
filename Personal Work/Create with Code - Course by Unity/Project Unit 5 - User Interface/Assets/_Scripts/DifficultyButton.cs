using UnityEngine;
using UnityEngine.UI;

namespace NikolayTabalyov {
    public class DifficultyButton : MonoBehaviour {

        [Header("Components")]
        private Button _difficultyButton;
        private GameManager _gameManager;
        [Header("Variables")]
        [SerializeField] private int _difficulty;

        #region Unity Methods
        private void Awake() {
            _difficultyButton = GetComponent<Button>();
            _difficultyButton.onClick.AddListener(SetDifficulty);
        }

        private void Start() {
            _gameManager = GameManager.Instance;
        }
        #endregion

        #region Other Methods
        private void SetDifficulty() {
            _gameManager.StartGame(_difficulty);
        }
        #endregion
    }
}
