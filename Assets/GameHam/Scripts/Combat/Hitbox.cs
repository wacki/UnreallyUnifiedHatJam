using UnityEngine;
using System.Collections;

namespace UU.GameHam
{

	public class Hitbox : MonoBehaviour
	{
	
		public bool visible = false;

		// Use this for initialization
		void Start ()
		{
			gameObject.GetComponent<SpriteRenderer> ().enabled = visible;
		}
	
		// Update is called once per frame
		void Update ()
		{
		
		}

		void OnTriggerEnter2D (Collider2D other)
		{
            var cs = other.gameObject.GetComponent<CharacterStats>();

            if (cs != null) {
                //Damage other player
                Debug.Log("DAMAGING");
                cs.Damage(1);

                var dir = (other.transform.position - transform.position).x * Vector3.right;
                other.GetComponent<Motor2D>().KnockBack(dir);
			}
		}
	}
}