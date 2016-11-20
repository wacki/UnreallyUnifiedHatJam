using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
public class MainMenuScript : MonoBehaviour {

	// Use this for initialization
	void Start ()
    {
        
        Cursor.lockState = CursorLockMode.None;
    }
	
	// Update is called once per frame
	void Update ()
    {
	
	}
    public void StartGame()
    {
        //Loads Game
        SceneManager.LoadScene("CharacterSelect");
    }

    public void ExitGame()
    {
        //This will quit the game
        Application.Quit();
    }
}
