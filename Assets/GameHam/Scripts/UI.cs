using UnityEngine;
using System.Collections;
using UnityEngine.UI;

namespace UU.GameHam
{
	

	public class UI : MonoBehaviour
	{

		public GameObject[] players = new GameObject[4];

		public GameObject[] playerClock = new GameObject[4];

		public float[] playerClockTimers = new float[4];

		public GameObject[] flagText = new GameObject[2];

		// Use this for initialization
		void Start ()
		{
			
		}
	
		// Update is called once per frame
		void Update ()
		{
			//Draw player related stuff
			for (int i = 0; i < 4; i++) {
				playerClock [i].SetActive (false);

				//Draw Chronometers if they are dead
				if (playerClockTimers [i] > 0) {
					playerClockTimers [i] -= Time.deltaTime;
					playerClock [i].GetComponentInChildren<Text> ().text = Mathf.Ceil (playerClockTimers [i]) + "";
					playerClock [i].SetActive (true);

					if (playerClockTimers [i] <= 0) {
						respawnPlayer (players [i]);
					}

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
