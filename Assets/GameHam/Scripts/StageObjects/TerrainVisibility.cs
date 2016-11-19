using UnityEngine;
using System.Collections;

namespace UU.GameHam
{
	public class TerrainVisibility : MonoBehaviour
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
	}
}
