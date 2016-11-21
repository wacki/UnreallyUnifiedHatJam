using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class MainMenuScript : MonoBehaviour {

    //These are for the Text Objects
    public GameObject superText;
    public GameObject gameText;
    public GameObject brawlText;
    public GameObject music;

    public GameObject startButton;
    public GameObject exitButton;

    public AudioClip superGameBrawlAudioClip;
    public GameObject superGameBrawlAudioSourceObject;

    public int titleCounter = 0;
    private float timer = 0.0f;

    public float superTextTime = 0.2f;
    public float brawlTextTime = 1.0f;
    public float gameTextTime = 2.0f;
    public float buttonFadeInTime = 4.0f;
    private bool done = false;
    private bool firstUpdate = true;

    public Text timertext;

    // Use this for initialization
    void Start ()
    {
        
        Cursor.lockState = CursorLockMode.None;
        timer = 0.0f;
        //superGameBrawlAudioSourceObject.SetActive(true);
        brawlText.GetComponent<CanvasRenderer>().SetAlpha(0);
        gameText.GetComponent<CanvasRenderer>().SetAlpha(0);
        superText.GetComponent<CanvasRenderer>().SetAlpha(0);
    }
	
	// Update is called once per frame
	void Update ()
    {
        if (Time.time < 1.0f)
            return;

        if (done)
            return;
        
        if(firstUpdate)
        {
            firstUpdate = false;
            GetComponent<AudioSource>().PlayOneShot(superGameBrawlAudioClip);
        }


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
        //timertext.text = timer.ToString();

        if (timer > buttonFadeInTime)
        {
            startButton.SetActive(true);
            exitButton.SetActive(true);
            //exitButton.GetComponent<CanvasRenderer>().SetAlpha(1);
            //music.GetComponent<AudioSource>().Play();
            music.SetActive(true);
            done = true;
        }
        else if (timer > gameTextTime)
        {
            brawlText.GetComponent<CanvasRenderer>().SetAlpha(1);
            //brawlText.SetActive(true);
        }
        else if (timer > brawlTextTime)
        {
            gameText.GetComponent<CanvasRenderer>().SetAlpha(1);
            //gameText.SetActive(true);
        }
        else if (timer > superTextTime)
        {
            superText.GetComponent<CanvasRenderer>().SetAlpha(1);
            //superText.SetActive(true);
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
