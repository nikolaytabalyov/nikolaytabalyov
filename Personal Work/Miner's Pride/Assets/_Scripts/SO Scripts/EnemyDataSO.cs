using UnityEngine;

	[CreateAssetMenu(fileName = "EnemyDataSO", menuName = "ScriptableObjects/EnemyDataSO", order = 2)]
	public class EnemyDataSO : ScriptableObject {
		
        #region Variables
		public Enemy.EnemyType enemyType;
		public string enemyName;
        public int maxHealth;
		public int attackDamage;
		public float attackRange;
		public float attackSpeed;
		public float movementSpeed;
		public float shootingMovementSpeed;
		public float idleMovementSpeed;
		#endregion
        
		#region Methods
        
		#endregion
        
	}