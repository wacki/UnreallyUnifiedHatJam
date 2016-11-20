using UnityEngine;
using System.Collections;

namespace UU.GameHam
{
    /// <summary>
    /// Handles projectile flying and applies a debuff on enemies that get hit
    /// </summary>
    public class Projectile : MonoBehaviour
    {
		[Tooltip("Assign the speed of the projectile")]
		public float speed; // Projectile speed
		[Tooltip("Type the string of the effect")]
		public string effect; //String of effect

        public int damage = 1;

        public bool doKnockback = false;
        public float knockbackForce;
        
        [Tooltip("projectile debuff that will be applied on hitting an enemy")]
        public CharacterStatsModifier projectileDebuff;


        public bool facingRight; //Know where it's facing

		void Start ()
		{
			if (facingRight)
				gameObject.GetComponent<Rigidbody2D> ().velocity = transform.right * speed;
			else
				gameObject.GetComponent<Rigidbody2D> ().velocity = transform.right * -speed;
		}

		void OnTriggerEnter2D(Collider2D other){

            var cs = other.gameObject.GetComponent<CharacterStats>();
            var motor = other.gameObject.GetComponent<Motor2D>();
            //If the object I collide with is a character
            if (cs != null) {
                //affect player then
                cs.ApplyModifier(projectileDebuff);
                cs.Damage(damage);

                if (doKnockback)
                    motor.KnockBack((transform.right * -speed).normalized * knockbackForce);
                
            }

            Destroy(gameObject);

        }

    }
}