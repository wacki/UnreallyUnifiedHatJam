using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
public class MainMenuScript : MonoBehaviour {

    public GameObject superText;
    public GameObject gameText;
    public GameObject brawlText;

    public int titleCounter = 0;
    // Use this for initialization
    void Start ()
    {
        
        Cursor.lockState = CursorLockMode.None;
        
    }
	
	// Update is called once per frame
	void Update ()
    {
        titleCounter++;

        if(titleCounter == 10)
        {
            
            superText.SetActive(true);
        }

        if (titleCounter == 60)
        {
            gameText.SetActive(true);
        }

        if (titleCounter == 100)
        {
            brawlText.SetActive(true);
        }
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
