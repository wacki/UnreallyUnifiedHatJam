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
			if (other.gameObject.GetComponent<CharacterStats> () != null) {
				//Damage other player
			}
		}
	}
}