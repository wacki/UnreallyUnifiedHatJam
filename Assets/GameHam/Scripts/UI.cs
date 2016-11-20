using UnityEngine;
using System.Collections;
using UnityEngine.UI;

namespace UU.GameHam
{
	

	public class UI : MonoBehaviour
	{

		public CharacterStats[] players = new CharacterStats[4];

		public GameObject[] playerClock = new GameObject[4];

		public GameObject[] flagText = new GameObject[2];

		// Use this for initialization
		void Start ()
		{
			players = Resources.FindObjectsOfTypeAll<CharacterStats> ();
		}
	
		// Update is called once per frame
		void Update ()
		{
			//Draw player related stuff
			for (int i = 0; i < 4; i++) {
				playerClock [i].SetActive (false);

				//Draw Chronometers if they are dead
				if (players [i].timeUntilRespawn > 0.0f) {
					playerClock [i].GetComponentInChildren<Text> ().text = Mathf.Ceil (players [i].timeUntilRespawn) + "";
					playerClock [i].SetActive (true);
				}

				//If they are alive draw health bars instead
				else {

				}
			}

			//Draw flag related stuff



		}

		public void respawnPlayer (GameObject player)
		{
			//respawn player on stage 
		}
	}
}
