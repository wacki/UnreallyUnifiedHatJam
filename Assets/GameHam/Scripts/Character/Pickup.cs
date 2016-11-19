using UnityEngine;
using System.Collections;

namespace UU.GameHam
{
    public class Pickup : MonoBehaviour
    {
		void OnTriggerEnter2D(Collider2D other){
			//If the object I collide with has a bario component
			if (other.GetComponent<CharacterStats>() != null) {



			}
		}
    }



}