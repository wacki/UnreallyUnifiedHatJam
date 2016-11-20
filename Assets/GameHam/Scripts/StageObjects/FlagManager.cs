using UnityEngine;
using System.Collections;

namespace UU.GameHam
{
	public class FlagManager : MonoBehaviour
	{
		public Teams _team;
		private CharacterStats holder;
		public BoxCollider2D boxCollider;
		private Rigidbody2D rb;

		// Use this for initialization
		void Start ()
		{
			rb = GetComponent<Rigidbody2D> ();
			if (_team == Teams.Red) {
				boxCollider.gameObject.layer = LayerMask.NameToLayer ("TeamRed");
				gameObject.layer = LayerMask.NameToLayer ("TeamRed");
			} else {
				boxCollider.gameObject.layer = LayerMask.NameToLayer ("TeamBlue");
				gameObject.layer = LayerMask.NameToLayer ("TeamBlue");
			}
		}
	
		// Update is called once per frame
		void Update ()
		{
			
		}

		void OnTriggerEnter2D (Collider2D other)
		{
			if (transform.parent == null) {
				var cs = other.gameObject.GetComponent<CharacterStats> ();
				if (cs != null) {
					if (cs.team != _team) {
						boxCollider.enabled = false;
						rb.isKinematic = true;
						transform.parent = other.transform;
						transform.localPosition = new Vector3 (0, 0);
						print ("blue got the flag");
					}
				}
			}
		}

		public void releaseFlag()
		{
			if (transform.parent != null) {
				if(transform.parent.gameObject.GetComponent<CharacterStats>().isAlive == false)
				{
					print ("blue dropped the flag");
					boxCollider.enabled = true;
					transform.parent = null;
					rb.isKinematic = false;
				}
			} 
		}

	}
}
