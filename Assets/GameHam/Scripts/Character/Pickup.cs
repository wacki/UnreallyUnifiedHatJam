using UnityEngine;
using System.Collections;

namespace UU.GameHam
{
    [RequireComponent(typeof(BoxCollider2D))]
    public class Pickup : MonoBehaviour
    {
        public float respawnDuration;
        public CharacterStatsModifier pickupEffect;
        private bool onCooldown = false;
        private float respawnTimer;
        private SpriteRenderer spriteRenderer;

        public bool removeDebuffsOnPickup = false;

        void Awake()
        {
            spriteRenderer = GetComponent<SpriteRenderer>();
        }

        void Update()
        {
            if (onCooldown)
            {
                respawnTimer -= Time.deltaTime;
                if (respawnTimer <= 0.0f)
                {
                    onCooldown = false;
                    spriteRenderer.enabled = true;
                }
            }
        }

        void OnTriggerStay2D(Collider2D other)
        {
            if (onCooldown)
                return;

            var cs = other.GetComponent<CharacterStats>();
            if (other.GetComponent<CharacterStats>() != null)
            {
                if (removeDebuffsOnPickup)
                    cs.ClearModifiers();

                cs.ApplyModifier(pickupEffect);
                StartCooldown();
            }
        }

        void StartCooldown()
        {
            onCooldown = true;
            respawnTimer = respawnDuration;
            spriteRenderer.enabled = false;
        }
    }



}