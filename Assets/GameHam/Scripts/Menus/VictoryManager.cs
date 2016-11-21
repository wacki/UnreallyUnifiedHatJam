using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

namespace UU.GameHam
{

public class VictoryManager : MonoBehaviour {

	public GameObject blueWin;
	public GameObject redWin ;

		public string CreditsScene;

		public float restartTimer = 4;
	// Use this for initialization
	void Start () {
			if (GameManager.instance.redWon ())
				
				redWin.SetActive (true);
			else
				blueWin.SetActive (true);
	}
	
	// Update is called once per frame
	void Update () {
			if (restartTimer > 0)
				restartTimer -= Time.deltaTime;
			else
				SceneManager.LoadScene (CreditsScene);
	}
}
}
