﻿using UnityEngine;
using System.Collections;

namespace UU.GameHam
{
    public class ArtistGhostSpecial : MonoBehaviour
    {
        public GameObject target;
        public CharacterStatsModifier debuff;
        public int damageAmount;
        public bool instaKill = false;
        public float dampenFactor;
        public int duration;
        public float acceleration;

        public Color redTint = Color.red;
        public Color blueTint = Color.blue;

        private Vector2 _velocity;
        private float timer;

        private void Update()
        {
            Vector2 dir = target.transform.position - transform.position;
            dir.Normalize();

            timer += Time.deltaTime;

            _velocity = Vector2.Lerp(_velocity, Vector2.zero, Time.deltaTime * dampenFactor);

            _velocity += acceleration * dir * Time.deltaTime;
            transform.position += (Vector3)_velocity * Time.deltaTime;

            var targetCs = target.GetComponent<CharacterStats>();

            var spriteRenderer = GetComponent<SpriteRenderer>();
            if (targetCs.team == Teams.Red)
            {
                spriteRenderer.color = blueTint;

            }
            else
            {

                spriteRenderer.color = redTint;
            }

            if (_velocity.x > 0)
                spriteRenderer.flipX = true;
            else if(_velocity.x < 0)
                spriteRenderer.flipX = false;

            if (!targetCs.isAlive)
            {
                Destroy(gameObject);
                return;
            }

            if (timer > duration)
                Destroy(gameObject);

        }


        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.gameObject != target)
                return;

            var targetCs = other.GetComponent<CharacterStats>();

            if (targetCs == null)
                return;


            if (!instaKill)
                targetCs.Damage(damageAmount);
            else
                targetCs.Kill();

            if (debuff != null)
                targetCs.ApplyModifier(debuff);


            Destroy(gameObject);
        }

    }

}
