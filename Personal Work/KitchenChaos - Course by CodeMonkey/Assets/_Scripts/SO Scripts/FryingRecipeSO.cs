using UnityEngine;

namespace NikolayTabalyov
{
	[CreateAssetMenu(fileName = "FryingRecipeSO", menuName = "ScriptableObjects/FryingRecipeSO", order = 3)]
	public class FryingRecipeSO : ScriptableObject {
		
        #region Variables
        public KitchenObjectSO input;
        public KitchenObjectSO output;
		public float fryingDurationMax;
		#endregion
        
		#region Methods
        
		#endregion
        
	}
}