using System;
using System.Collections.Generic;
using UnityEngine;

namespace NikolayTabalyov
{
    public class PlateCompleteVisual : MonoBehaviour {
        
        [Serializable]
        public struct IngredientSOGameObject {
            public KitchenObjectSO IngredientSO;
            public GameObject GameObject;
        }

        #region Variables
        [Header("Variables")]
        [SerializeField] private List<IngredientSOGameObject> _ingredientSOGameObjectsList = new();

        #endregion
    
        #region Components
        [Header("Components")]
        [SerializeField] private PlateKitchenObject _plateKitchenObject;
        #endregion
    
        #region Unity Methods
        private void Start() {
            _plateKitchenObject.OnIngredientAdded += PlateCompleteVisual_OnIngredientAdded;
        }

        private void PlateCompleteVisual_OnIngredientAdded(object sender, PlateKitchenObject.OnIngredientAddedEventArgs e) {
            foreach (IngredientSOGameObject ingredientSOGameObject in _ingredientSOGameObjectsList) {
                if (ingredientSOGameObject.IngredientSO == e.addedIngredientSO)
                    ingredientSOGameObject.GameObject.SetActive(true);
            }
        }
        #endregion

        #region Other Methods

        #endregion
    }
}
