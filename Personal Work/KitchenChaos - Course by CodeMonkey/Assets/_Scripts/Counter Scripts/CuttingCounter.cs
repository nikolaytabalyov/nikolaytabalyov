using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace NikolayTabalyov {
    public class CuttingCounter : BaseCounter, IHasProgress {
    
        public event EventHandler<IHasProgress.OnProgressChangedEventArgs> OnProgressChanged;

        public event EventHandler OnCut;


        #region Variables
        [Header("Variables")]
        private int _cuttingDuration;
        #endregion
    
        #region Components
        [Header("Components")]
        [SerializeField] private List<CuttingRecipeSO> _cuttingRecipeSOArray;
        #endregion
    
        #region Unity Methods
    
        #endregion
    
        #region Other Methods
        public override void Interact(Player player) {
            if (!HasKitchenObject()) { // if counter is empty
                if (player.HasKitchenObject() && HasCuttingRecipe(player.GetKitchenObject().GetKitchenObjectSO)) { // if player is holding something and it can be cut
                    player.GetKitchenObject().SetNewKitchenObjectParent(this);
                    _cuttingDuration = 0;

                    int maxCuttingDuration = GetCuttingRecipeSOFromInput(GetKitchenObject().GetKitchenObjectSO).cuttingDurationMax;
                    OnProgressChanged?.Invoke(this, new IHasProgress.OnProgressChangedEventArgs {
                        progressNormalized = (float)_cuttingDuration / maxCuttingDuration
                    });
                }
            } else if (!player.HasKitchenObject()){ // if counter is not empty and player is not holding anything
                GetKitchenObject().SetNewKitchenObjectParent(player);
            }
        }

        public override void InteractAlternate(Player player) {
            if (HasKitchenObject() && HasCuttingRecipe(GetKitchenObject().GetKitchenObjectSO)) {
                _cuttingDuration++;
                OnCut?.Invoke(this, EventArgs.Empty);
                
                int maxCuttingDuration = GetCuttingRecipeSOFromInput(GetKitchenObject().GetKitchenObjectSO).cuttingDurationMax;
                OnProgressChanged?.Invoke(this, new IHasProgress.OnProgressChangedEventArgs {
                    progressNormalized = (float)_cuttingDuration / maxCuttingDuration
                });

                if (_cuttingDuration >= GetCuttingRecipeSOFromInput(GetKitchenObject().GetKitchenObjectSO).cuttingDurationMax) {
                    KitchenObjectSO kitchenObjectSO = GetKitchenObject().GetKitchenObjectSO;
                    GetKitchenObject().DestroySelf();
                    KitchenObject.SpawnKitchenObject(GetOutputFromInput(kitchenObjectSO), this);
                }
            }
        }

        private bool HasCuttingRecipe(KitchenObjectSO inputKitchenObjectSO) {
            return _cuttingRecipeSOArray.Find(cuttingRecipeSO => cuttingRecipeSO.input == inputKitchenObjectSO) is not null;  
        }   // returns true if there is a cutting recipe for the input

        private KitchenObjectSO GetOutputFromInput(KitchenObjectSO inputKitchenObjectSO) {
            return _cuttingRecipeSOArray.Find(cuttingRecipeSO => cuttingRecipeSO.input == inputKitchenObjectSO).output;  
        }   

        private CuttingRecipeSO GetCuttingRecipeSOFromInput(KitchenObjectSO inputKitchenObjectSO) {
            return _cuttingRecipeSOArray.Find(cuttingRecipeSO => cuttingRecipeSO.input == inputKitchenObjectSO);  
        }
        #endregion
    }
}
