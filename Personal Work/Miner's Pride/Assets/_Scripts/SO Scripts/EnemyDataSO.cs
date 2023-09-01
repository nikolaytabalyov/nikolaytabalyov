using UnityEngine;

	[CreateAssetMenu(fileName = "EnemyDataSO", menuName = "ScriptableObjects/EnemyDataSO", order = 2)]
	public class EnemyDataSO : ScriptableObject {
		
        #region Variables
		public string enemyName;
        public int maxHealth;
		public int attackDamage;
		public float attackRange;
		public float attackSpeed;
		public float movementSpeed;
		#endregion
        
		#region Methods
        
		#endregion
        
	}