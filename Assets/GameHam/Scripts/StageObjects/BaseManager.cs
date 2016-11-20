using UnityEngine;
using System.Collections;

namespace UU.GameHam
{
	public class BaseManager : MonoBehaviour
	{
		public Teams _team;
		// Use this for initialization
		void Start ()
		{
	
		}
	
		// Update is called once per frame
		void Update ()
		{
	
		}

		void OnTriggerEnter2D (Collider2D other)
		{
				var cs = other.gameObject.GetComponent<FlagManager> ();
				if (cs != null) {
				if (cs._team != _team) {
					GameManager.instance.gainFlagPoint (_team);
					}
				}
			}
	}
}
