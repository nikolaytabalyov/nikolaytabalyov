using System;
using System.Collections.Generic;
using UnityEngine;

namespace NikolayTabalyov
{
    public class PlateCounterVisual : MonoBehaviour {
    
        #region Variables
        [Header("Variables")]
        private List<Transform> _plateVisualsList = new List<Transform>();
        private Vector3 plateOffset = new Vector3(0f, 0.1f, 0f);
        #endregion
    
        #region Components
        [Header("Components")]
        [SerializeField] private PlateCounter _plateCounter;
        [SerializeField] private Transform _plateSpawnPoint;
        [SerializeField] private Transform _plateVisual;
        #endregion
    
        #region Unity Methods
        private void Start() {
            _plateCounter.OnPlateSpawned += PlateCounter_OnPlateSpawned;
        }

        private void PlateCounter_OnPlateSpawned(object sender, EventArgs e) {
            Transform spawnedPlate = Instantiate(_plateVisual, _plateSpawnPoint);
            spawnedPlate.transform.localPosition = plateOffset * _plateVisualsList.Count;
            _plateVisualsList.Add(spawnedPlate);
        }
        #endregion

        #region Other Methods

        #endregion
    }
}
