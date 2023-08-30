using UnityEngine;

namespace NikolayTabalyov
{
	[CreateAssetMenu(fileName = "CuttingRecipeSO", menuName = "ScriptableObjects/CuttingRecipeSO", order = 2)]
	public class CuttingRecipeSO : ScriptableObject {
		
        #region Variables
        public KitchenObjectSO input;
        public KitchenObjectSO output;
		public int cuttingDurationMax;
		#endregion
        
		#region Methods
        
		#endregion
        
	}
}