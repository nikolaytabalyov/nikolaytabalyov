using UnityEngine;

namespace NikolayTabalyov {
	
	[CreateAssetMenu(fileName = "KitchenObjectSO", menuName = "ScriptableObjects/KitchenObjectSO", order = 1)]
	public class KitchenObjectSO : ScriptableObject {
		
        #region Variables
		public Transform prefab;
		public Sprite sprite;
		public string objectName;
        
		#endregion
        
		#region Methods
        
		#endregion
        
	}
}