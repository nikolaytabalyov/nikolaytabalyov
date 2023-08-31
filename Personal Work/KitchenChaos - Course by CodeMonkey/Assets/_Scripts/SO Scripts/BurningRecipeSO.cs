using UnityEngine;

namespace NikolayTabalyov
{
	[CreateAssetMenu(fileName = "BurningRecipeSO", menuName = "ScriptableObjects/BurningRecipeSO", order = 4)]
	public class BurningRecipeSO : ScriptableObject {
		
        #region Variables
        public KitchenObjectSO input;
        public KitchenObjectSO output;
		public float burningDurationMax;
		#endregion
        
		#region Methods
        
		#endregion
        
	}
}