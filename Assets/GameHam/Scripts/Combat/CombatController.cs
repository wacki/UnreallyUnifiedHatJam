using UnityEngine;
using System.Collections;

namespace UU.GameHam
{
    /// <summary>
    /// Implements melee and range combat functions
    /// Shooting projectiles and hitting enemies
    /// </summary>
    public class CombatController : MonoBehaviour
    {

		[Tooltip("Assign the cooldown time for ranged attacks(60 = 1 minute)")]
		public float initialRangeCooldown = 0;
		[Tooltip("Assign the cooldown time for melee attacks(60 = 1 minute)")]
		public float initialMeleeCooldown = 0;

        private float rangeCooldownCounter = 0;
		private float meleeCooldownCounter = 0;
		public bool attacking = false;

		public GameObject projectileSpawnPoint;

		public Projectile projectilePrefab;

		void Start ()
		{
			
		}

		void Update ()
		{
			attacking = false; 
			//Reduce counters if they are above 0
			if (rangeCooldownCounter > 0) {
				rangeCooldownCounter-= Time.deltaTime;
				attacking = true; 
			} 

			if (meleeCooldownCounter > 0) {
				meleeCooldownCounter-= Time.deltaTime;
				attacking = true;
			} 

		}

		public void Shoot()
		{
			//If you can shoot
			if (rangeCooldownCounter <= 0)
			{
				//We store clone in a variable called bulletClone
				Projectile projectileClone = (Projectile)Instantiate (projectilePrefab, projectileSpawnPoint.transform.position, projectileSpawnPoint.transform.rotation);
				//We set the layer of the newly created bullet to be the same as ours
				projectileClone.gameObject.layer = gameObject.layer;
				projectileClone.facingRight = gameObject.GetComponent<Motor2D> ().facingRight;
				rangeCooldownCounter = initialRangeCooldown; //Start cooldown
				//gameObject.GetComponent<AnimationController> ().CallAnimation ("Projectile"); //Call for the animation to attack
			}
		}

		public void Attack()
		{
			//If you can shoot
			if (meleeCooldownCounter <= 0)
			{
				gameObject.GetComponent<AnimationController> ().CallAnimation ("Attack"); //Call for the animation to attack
				meleeCooldownCounter = initialMeleeCooldown; //Start cooldown
			}
		}

		//This allows us to know if the player is in the middle of the attack or projectile animation
		public bool IsAttacking(){
			AnimationController conto = gameObject.GetComponent<AnimationController> ();
			return (conto.IsAnimationRunning("Attack") || conto.IsAnimationRunning("Projectile"));
		}
			
    }
}