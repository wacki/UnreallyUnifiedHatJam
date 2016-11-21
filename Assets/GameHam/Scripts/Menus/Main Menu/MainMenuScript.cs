using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
public class MainMenuScript : MonoBehaviour {

    //These are for the Text Objects
    public GameObject superText;
    public GameObject gameText;
    public GameObject brawlText;
    public GameObject music;

    public GameObject startButton;
    public GameObject exitButton;

    public int titleCounter = 0;
    private float timer = 0.0f;

    public float superTextTime = 0.2f;
    public float brawlTextTime = 1.0f;
    public float gameTextTime = 2.0f;
    public float buttonFadeInTime = 4.0f;
    private bool done = false;

    // Use this for initialization
    void Start ()
    {
        
        Cursor.lockState = CursorLockMode.None;
        timer = 0.0f;
    }
	
	// Update is called once per frame
	void Update ()
    {
        //if (done)
            //return;

        //Counter to keep track of time
        titleCounter++;


        ////Make the text object visable if the time starts
        //if(titleCounter == 10)
        //{
            
        //    superText.SetActive(true);
        //}

        //if (titleCounter == 60)
        //{
        //    gameText.SetActive(true);
        //}

        //if (titleCounter == 100)
        //{
        //    brawlText.SetActive(true);
        //}

        //if (titleCounter == 500)
        //{
        //    startButton.SetActive(true);
        //    exitButton.SetActive(true);
        //    music.GetComponent<AudioSource>().Play();
            
        //}

        timer += Time.deltaTime;

        if(timer > buttonFadeInTime)
        {
            startButton.SetActive(true);
            exitButton.SetActive(true);
            //music.GetComponent<AudioSource>().Play();
            music.SetActive(true);
        }
        else if (timer > gameTextTime)
        {

            brawlText.SetActive(true);
        }
        else if (timer > brawlTextTime)
        {
            gameText.SetActive(true);
        }
        else if (timer > superTextTime)
        {
            superText.SetActive(true);
        }
        else if(superText.gameObject.activeSelf)
        {
            done = true;
        }

    }
    public void StartGame()
    {
        //Loads Game
        SceneManager.LoadScene("Character select");
    }

    public void ExitGame()
    {
        //This will quit the game
        Application.Quit();
    }
}
