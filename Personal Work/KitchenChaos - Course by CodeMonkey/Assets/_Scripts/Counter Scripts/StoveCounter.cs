using System;
using System.Collections.Generic;
using UnityEngine;

namespace NikolayTabalyov
{
    public class StoveCounter : BaseCounter, IHasProgress {
        
        public event EventHandler<IHasProgress.OnProgressChangedEventArgs> OnProgressChanged;
        public event EventHandler<OnStateChangedEventArgs> OnStateChanged;
        public class OnStateChangedEventArgs : EventArgs {
            public State state;
        }
        public enum State {
            Empty, 
            Frying,
            Fried,
            Burned
        }
        
        #region Variables
        [Header("Variables")]
        private State _currentState;
        [SerializeField] private List<FryingRecipeSO> _fryingRecipeSOList;
        [SerializeField] private List<BurningRecipeSO> _burningRecipeSOList;
        private float _fryingTimer;
        private float _burningTimer;
        #endregion
    
        #region Components
        [Header("Components")]
        private FryingRecipeSO _fryingRecipeSO;
        private BurningRecipeSO _burningRecipeSO;
        #endregion
    
        #region Unity Methods
        private void Start() {
            _currentState = State.Empty;
        }
        private void Update() {
            if (HasKitchenObject()) {
                switch (_currentState) {
                    case State.Empty:
                        _currentState = State.Frying;
                        break;
                    case State.Frying:
                        _fryingTimer += Time.deltaTime;

                        OnProgressChanged?.Invoke(this, new IHasProgress.OnProgressChangedEventArgs {
                            progressNormalized = _fryingTimer / _fryingRecipeSO.fryingDurationMax
                        });
                        if (_fryingTimer >= _fryingRecipeSO.fryingDurationMax) {
                            GetKitchenObject().DestroySelf();
                            KitchenObject.SpawnKitchenObject(_fryingRecipeSO.output, this);
                            _currentState = State.Fried;
                            _burningTimer = 0f;

                            OnProgressChanged?.Invoke(this, new IHasProgress.OnProgressChangedEventArgs {
                                progressNormalized = _fryingTimer / _fryingRecipeSO.fryingDurationMax
                            });

                            _burningRecipeSO = GetBurningRecipeSOFromInput(_fryingRecipeSO.output);

                            OnStateChanged?.Invoke(this, new OnStateChangedEventArgs {
                                state = _currentState
                            });

                        }
                        break;
                    case State.Fried:
                        _burningTimer += Time.deltaTime;

                        OnProgressChanged?.Invoke(this, new IHasProgress.OnProgressChangedEventArgs {
                            progressNormalized = _burningTimer / _burningRecipeSO.burningDurationMax
                        });
                        if (_burningTimer >= _burningRecipeSO.burningDurationMax) {
                            GetKitchenObject().DestroySelf();
                            KitchenObject.SpawnKitchenObject(_burningRecipeSO.output, this);
                            _currentState = State.Burned;

                            OnStateChanged?.Invoke(this, new OnStateChangedEventArgs {
                                state = _currentState
                            });

                            OnProgressChanged?.Invoke(this, new IHasProgress.OnProgressChangedEventArgs {
                                progressNormalized = 0f
                            });
                        }
                        break;
                    case State.Burned:
                        break;
                }
            }
        }

        #endregion

        #region Other Methods
        public override void Interact(Player player) {
            if (!HasKitchenObject()) { // if counter is empty
                if (player.HasKitchenObject() && HasFryingRecipe(player.GetKitchenObject().GetKitchenObjectSO)) { 
                    // if player is holding something and it can be fried
                    player.GetKitchenObject().SetNewKitchenObjectParent(this);

                    _fryingTimer = 0f;
                    _currentState = State.Frying;
                    OnStateChanged?.Invoke(this, new OnStateChangedEventArgs {
                        state = _currentState
                    });

                    _fryingRecipeSO = GetFryingRecipeSOFromInput(GetKitchenObject().GetKitchenObjectSO);
                }
            } else if (!player.HasKitchenObject()){ // if counter is not empty and player is not holding anything
                GetKitchenObject().SetNewKitchenObjectParent(player);
                _currentState = State.Empty;
                OnStateChanged?.Invoke(this, new OnStateChangedEventArgs {
                    state = _currentState
                });
                
                OnProgressChanged?.Invoke(this, new IHasProgress.OnProgressChangedEventArgs {
                    progressNormalized = 0f
                });
            }
        }

        private bool HasFryingRecipe(KitchenObjectSO inputKitchenObjectSO) {
            return _fryingRecipeSOList.Find(fryingRecipeSO => fryingRecipeSO.input == inputKitchenObjectSO) is not null;  
        }   // returns true if there is a frying recipe for the input

        private KitchenObjectSO GetOutputFromInput(KitchenObjectSO inputKitchenObjectSO) {
            return _fryingRecipeSOList.Find(fryingRecipeSO => fryingRecipeSO.input == inputKitchenObjectSO).output;  
        }   

        private FryingRecipeSO GetFryingRecipeSOFromInput(KitchenObjectSO inputKitchenObjectSO) {
            return _fryingRecipeSOList.Find(fryingRecipeSO => fryingRecipeSO.input == inputKitchenObjectSO);  
        }

        private BurningRecipeSO GetBurningRecipeSOFromInput(KitchenObjectSO inputKitchenObjectSO) {
            return _burningRecipeSOList.Find(burningRecipeSO => burningRecipeSO.input == inputKitchenObjectSO);  
        }
        #endregion
    }
}
