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

        public float meleeRange;

        private float rangeCooldownCounter = 0;
        private float meleeCooldownCounter = 0;
        public bool attacking = false;

        public GameObject projectileSpawnPoint;

        public Projectile projectilePrefab;

        public AudioClip hitSound;
        public AudioClip missSound;

        void Start()
        {

        }

        void Update()
        {
            attacking = false;
            //Reduce counters if they are above 0
            if (rangeCooldownCounter > 0)
            {
                rangeCooldownCounter -= Time.deltaTime;
                attacking = true;
            }

            if (meleeCooldownCounter > 0)
            {
                meleeCooldownCounter -= Time.deltaTime;
                attacking = true;
            }

        }

        public void Shoot()
        {
            //If you can shoot
            if (rangeCooldownCounter <= 0)
            {
                //We store clone in a variable called bulletClone
                Projectile projectileClone = (Projectile)Instantiate(projectilePrefab, projectileSpawnPoint.transform.position, projectileSpawnPoint.transform.rotation);
                //We set the layer of the newly created bullet to be the same as ours
                projectileClone.gameObject.layer = gameObject.layer;
                projectileClone.facingRight = gameObject.GetComponent<Motor2D>().facingRight;
                rangeCooldownCounter = initialRangeCooldown; //Start cooldown
                                                             //gameObject.GetComponent<AnimationController> ().CallAnimation ("Projectile"); //Call for the animation to attack


                projectileClone.owner = GetComponent<CharacterStats>();
            }
        }

        public void Attack()
        {
            var col = GetComponent<BoxCollider2D>();
            var facingRight = GetComponent<Motor2D>().facingRight;
            var myTeam = GetComponent<CharacterStats>().team;

            var min = col.bounds.min;
            var max = col.bounds.max;

            if(facingRight)
            {
                min.x = max.x;
                max.x += meleeRange;
            }
            else
            {
                max.x = min.x;
                min.x -= meleeRange;
            }

            var hitObjects = Physics2D.OverlapAreaAll(min, max);
            foreach(var hitObj in hitObjects)
            {
                var otherPlayer = hitObj.GetComponent<CharacterStats>();
                if (otherPlayer != null)
                {
                    if(otherPlayer.team != myTeam)
                    {
                        // play hit sound
                        if (hitSound != null)
                        {
                            AudioManager.instance.PlayOneShot(hitSound);
                        }

                        otherPlayer.Damage(1);
                        otherPlayer.GetComponent<Motor2D>().KnockBack(otherPlayer.transform.position - transform.position);
                        // increase energy on succesful hit
                        GetComponent<CharacterStats>().IncreaseEnergy(1);
                    }
                    else
                    {
                        // play miss sound
                        if (missSound != null)
                        {
                            AudioManager.instance.PlayOneShot(missSound);
                        }
                    }
                }
            }
        }

        //This allows us to know if the player is in the middle of the attack or projectile animation
        public bool IsAttacking()
        {
            AnimationController conto = gameObject.GetComponent<AnimationController>();
            return (conto.IsAnimationRunning("Attack") || conto.IsAnimationRunning("Projectile"));
        }

        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.magenta;

            var col = GetComponent<BoxCollider2D>();
            var facingRight = GetComponent<Motor2D>().facingRight;

            var min = col.bounds.min;
            var max = col.bounds.max;

            if (facingRight)
            {
                min.x = max.x;
                max.x += meleeRange;
            }
            else
            {
                max.x = min.x;
                min.x -= meleeRange;
            }

            Gizmos.DrawWireCube(min + (0.5f * (max - min)), min - max);
        }

    }
}