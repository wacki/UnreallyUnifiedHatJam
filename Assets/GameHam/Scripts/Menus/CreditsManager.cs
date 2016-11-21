using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

namespace UU.GameHam
{

	public class CreditsManager : MonoBehaviour {

		public string GoBackScene;

		public float restartTimer = 10;
	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
			if (restartTimer > 0)
				restartTimer -= Time.deltaTime;
			else
				SceneManager.LoadScene (GoBackScene);
	}
}
}
