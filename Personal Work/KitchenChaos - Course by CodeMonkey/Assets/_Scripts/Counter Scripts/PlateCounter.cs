using System;
using UnityEngine;

namespace NikolayTabalyov
{
    public class PlateCounter : BaseCounter {
    
        public event EventHandler OnPlateSpawned;
        public event EventHandler OnPlateRemoved;
        #region Variables
        [Header("Variables")]
        private float _spawnTimer = 0f;
        private float _spawnTimerMax = 4f;
        private int _spawnedPlatesAmount;
        private int _spawnedPlatesAmountMax = 4;
        #endregion
    
        #region Components
        [Header("Components")]
        [SerializeField] private KitchenObjectSO _kitchenObjectSO;
        #endregion
    
        #region Unity Methods
        private void Update() {
            _spawnTimer += Time.deltaTime;

            if (_spawnTimer >= _spawnTimerMax) {
                _spawnTimer = 0f;
                if (_spawnedPlatesAmount < _spawnedPlatesAmountMax) {
                    _spawnedPlatesAmount++;
                    OnPlateSpawned?.Invoke(this, EventArgs.Empty);
                }
            }
        }
        #endregion
    
        #region Other Methods
        public override void Interact(Player player) {
            if (!player.HasKitchenObject()) {
                if (_spawnedPlatesAmount > 0) {
                    _spawnedPlatesAmount--;
                    KitchenObject.SpawnKitchenObject(_kitchenObjectSO, player);
                    OnPlateRemoved?.Invoke(this, EventArgs.Empty);
                }
            }
        }
        #endregion
    }
}
