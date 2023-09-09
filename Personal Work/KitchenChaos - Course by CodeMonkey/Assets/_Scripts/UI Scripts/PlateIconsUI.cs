using System;
using UnityEngine;

namespace NikolayTabalyov
{
    public class PlateIconsUI : MonoBehaviour {
    
        #region Variables
        //[Header("Variables")]
        #endregion
    
        #region Components
        [Header("Components")]
        [SerializeField] private PlateKitchenObject _plateKitchenObject;
        [SerializeField] private Transform _plateIconTemplate;
        #endregion
    
        #region Unity Methods
        private void Awake() {
            _plateIconTemplate.gameObject.SetActive(false);
        }
        #endregion
    
        #region Other Methods
        private void Start() {
            _plateKitchenObject.OnIngredientAdded += PlateIconsUI_OnIngredientAdded;
        }

        private void PlateIconsUI_OnIngredientAdded(object sender, PlateKitchenObject.OnIngredientAddedEventArgs e) {
            UpdateVisuals();
        }

        private void UpdateVisuals() {
            foreach (Transform child in transform) {
                if (child != _plateIconTemplate)
                    Destroy(child.gameObject);
            }
            foreach (KitchenObjectSO kitchenObjectSO in _plateKitchenObject.GetIngredientsSOList) {
                Transform iconTransform = Instantiate(_plateIconTemplate, transform);
                iconTransform.gameObject.SetActive(true);
                iconTransform.GetComponent<PlateIconSingleUI>().SetIconImageFromKitchenObjectSO(kitchenObjectSO);
            }
        }
        #endregion
    }
}
