using UnityEngine;
using System.Collections;
using UnityEngine.UI;

namespace UU.GameHam
{
	

	public class UI : MonoBehaviour
	{

		public CharacterStats[] players = new CharacterStats[4];

		public GameObject[] playerClock = new GameObject[4];

		public GameObject[] playerIdText = new GameObject[4];

		public GameObject[] HP = new GameObject[4];
		public Sprite[] HeartSprites;
		public Image[] HeartUI = new Image[4];

		public GameObject[] Pow = new GameObject[4];
		public Sprite[] PowSprites;
		public Image[] PowUI = new Image[4];

		public GameObject[] shield = new GameObject[4];

		public GameObject[] flagText = new GameObject[2];

		public GameObject redFlagText;
		public GameObject blueFlagText;

		// Use this for initialization
		void Start ()
		{
			//GameManager sends its list to this UI
		}
	
		// Update is called once per frame
		void Update ()
		{

			//Draw player related stuff
			for (int i = 0; i < 4; i++) {
				playerClock [i].SetActive (false);
				appearPlayerUIStuff (i);
			//	print(players [i].timeUntilRespawn + "");

				//Draw Chronometers if they are dead
				if (players [i].timeUntilRespawn > 0.0f) {
					implodePlayerUIStuff (i);
					playerClock [i].GetComponentInChildren<Text> ().text = Mathf.Ceil (players [i].timeUntilRespawn) + "";
					playerClock [i].SetActive (true);
				}

				//If they are alive draw health stuff instead
				else {
					
					//HP [i].gameObject.GetComponent<Text> ().text = "HP " + players [i].currentHealth + "/" + players [i].maxHealth;
					HeartUI[i].sprite = HeartSprites[players [i].currentHealth];
					//Pow [i].gameObject.GetComponent<Text> ().text = "POW " + players [i].currentEnergy + "/" + players [i].maxEnergy;
					PowUI[i].sprite = PowSprites[players [i].currentEnergy];
					shield [i].gameObject.GetComponent<Text> ().text = "Shield " + players [i].currentShield;
				}
			}

			//Draw flag related stuff

			redFlagText.gameObject.GetComponent<Text> ().text = GameManager.instance.redFlags + "";
			blueFlagText.gameObject.GetComponent<Text> ().text = GameManager.instance.blueFlags+ "";

		}

		public void respawnPlayer (GameObject player)
		{
			//respawn player on stage 
		}

		void implodePlayerUIStuff(int i)
		{
			playerIdText [i].gameObject.GetComponent<Text> ().text = "Respawning";
			HeartUI[i].sprite = HeartSprites[players [i].currentHealth];
			PowUI[i].sprite = PowSprites[players [i].currentEnergy];
			HP [i].SetActive (false);
			shield [i].SetActive (false);
			Pow [i].SetActive (false);
			shield[i].SetActive (false);

		}

		void appearPlayerUIStuff(int i)
		{
			playerIdText [i].gameObject.GetComponent<Text> ().text = "P" + (i + 1);
			HP [i].SetActive (true);
			shield [i].SetActive (true);
			Pow [i].SetActive (true);
			shield[i].SetActive (true);
		}
	}
}
