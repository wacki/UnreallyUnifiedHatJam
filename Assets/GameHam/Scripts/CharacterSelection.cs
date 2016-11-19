using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class CharacterSelection : MonoBehaviour {

	public Image redPlayer1Image;
	public Image redPlayer2Image;
	public Image bluePlayer1Image;
	public Image bluePlayer2Image;

    public Text redPlayer1Text;
    public Text redPlayer2Text;
    public Text bluePlayer1Text;
    public Text bluePlayer2Text;

    public int redPlayer1; // Joy0LSY
    public int redPlayer2; // Joy1LSY
    public int bluePlayer1; // Joy2LSY
    public int bluePlayer2; // Joy3LSY

    private bool isRedPlayer1SwitchRecent = false;
    private bool isRedPlayer2SwitchRecent = false;
    private bool isBluePlayer1SwitchRecent = false;
    private bool isBluePlayer2SwitchRecent = false;

    float selectionDirectionRedPlayer1;
    float selectionDirectionRedPlayer2;
    float selectionDirectionBluePlayer1;
    float selectionDirectionBluePlayer2;



    // Use this for initialization
    void Start () {

    }
	
	// Update is called once per frame
	void Update () {

        selectionDirectionRedPlayer1 = Input.GetAxis("Joy0LSY");
        selectionDirectionRedPlayer2 = Input.GetAxis("Joy1LSY");
        selectionDirectionBluePlayer1 = Input.GetAxis("Joy2LSY");
        selectionDirectionBluePlayer2 = Input.GetAxis("Joy3LSY");

        SwitchBetweenCharacters(redPlayer1, selectionDirectionRedPlayer1, isRedPlayer1SwitchRecent);
        SwitchBetweenCharacters(redPlayer2, selectionDirectionRedPlayer2, isRedPlayer2SwitchRecent);
        SwitchBetweenCharacters(bluePlayer1, selectionDirectionBluePlayer1, isBluePlayer1SwitchRecent);
        SwitchBetweenCharacters(bluePlayer2, selectionDirectionBluePlayer2, isBluePlayer2SwitchRecent);
        
        // have selection for all the characters
        if (isRedPlayer1SwitchRecent == false)
        {
            if (selectionDirectionRedPlayer1 > 0)
            {
                if (redPlayer1 >= 4)
                {
                    redPlayer1 = 1;
                }
                else
                {
                    redPlayer1++;
                }
            }
            else if (selectionDirectionRedPlayer1 < 0)
            {
                if (redPlayer1 <= 1)
                {
                    redPlayer1 = 4;
                }
                else
                {
                    redPlayer1--;
                }
            }
            isRedPlayer1SwitchRecent = true;
        }

        if (isRedPlayer2SwitchRecent == false)
        {
            if (selectionDirectionRedPlayer2 > 0)
            {
                if (redPlayer2 >= 4)
                {
                    redPlayer2 = 1;
                }
                else
                {
                    redPlayer2++;
                }
            }
            else if (selectionDirectionRedPlayer2 < 0)
            {
                if (redPlayer2 <= 1)
                {
                    redPlayer2 = 4;
                }
                else
                {
                    redPlayer2--;
                }
            }
            isRedPlayer2SwitchRecent = true;
        }

        if (isBluePlayer1SwitchRecent == false)
        {
            if (selectionDirectionBluePlayer1 > 0)
            {
                if (bluePlayer1 >= 4)
                {
                    bluePlayer1 = 1;
                }
                else
                {
                    bluePlayer1++;
                }
            }
            else if (selectionDirectionBluePlayer1 < 0)
            {
                if (bluePlayer1 <= 1)
                {
                    bluePlayer1 = 4;
                }
                else
                {
                    bluePlayer1--;
                }
            }
            isBluePlayer1SwitchRecent = true;
        }

        if (isBluePlayer2SwitchRecent == false)
        {
            if (selectionDirectionBluePlayer2 > 0)
            {
                if (bluePlayer2 >= 4)
                {
                    bluePlayer2 = 1;
                }
                else
                {
                    bluePlayer2++;
                }
            }
            else if (selectionDirectionBluePlayer2 < 0)
            {
                if (bluePlayer2 <= 1)
                {
                    bluePlayer2 = 4;
                }
                else
                {
                    bluePlayer2--;
                }
            }
            isBluePlayer2SwitchRecent = true;
        }
        

        // Waiting for the joystick to hit back to start for next switch
        if (Input.GetAxis("Joy0LSY") == 0.0f)
        {
            isRedPlayer1SwitchRecent = false;
        }

        if (Input.GetAxis("Joy1LSY") == 0.0f)
        {
            isRedPlayer2SwitchRecent = false;
        }

        if (Input.GetAxis("Joy2LSY") == 0.0f)
        {
            isBluePlayer1SwitchRecent = false;
        }

        if (Input.GetAxis("Joy3LSY") == 0.0f)
        {
            isBluePlayer2SwitchRecent = false;
        }




        // display output and give feedback to players
        PlayerCharacterDisplay(redPlayer1, redPlayer1Image, redPlayer1Text);
        PlayerCharacterDisplay(redPlayer2, redPlayer2Image, redPlayer2Text);
        PlayerCharacterDisplay(bluePlayer1, bluePlayer1Image, bluePlayer1Text);
        PlayerCharacterDisplay(bluePlayer2, bluePlayer2Image, bluePlayer2Text);



        //print(selectionDirectionRedPlayer1);
        //print(selectionDirectionRedPlayer2);
        //print(selectionDirectionBluePlayer1);
        //print(selectionDirectionBluePlayer2);


    }



    // with respect to which controller is leaning towards which direction
    // cycle through the characters

    // NOT WORKING FOR SOME WEIRD ASS REASON
    void SwitchBetweenCharacters(int player, float selectionDirection, bool switchCheck)
    {
        // have selection for all the characters
        if (switchCheck == false)
        {
            if (selectionDirection > 0)
            {
                if (player >= 4)
                {
                    player = 1;
                }
                else
                {
                    player++;
                }
            }
            else if (selectionDirection < 0)
            {
                if (player <= 1)
                {
                    player = 4;
                }
                else
                {
                    player--;
                }
            }
            switchCheck = true;
        }
    }

    // Changes the given display (image and text) of the given player with respect to its character
    void PlayerCharacterDisplay (int player, Image playerImage, Text playerText)
    {
        if (player == 1) // LD
        {
            playerImage.GetComponent<Image>().color = Color.green;
            playerText.GetComponent<Text>().text = "Level Designer";
        }
        else if (player == 2) // Prog
        {
            playerImage.GetComponent<Image>().color = Color.blue;
            playerText.GetComponent<Text>().text = "Programmer";

        }
        else if (player == 3) // Art
        {
            playerImage.GetComponent<Image>().color = Color.red;
            playerText.GetComponent<Text>().text = "Artist";

        }
        else if (player == 4) // PM
        {
            playerImage.GetComponent<Image>().color = Color.yellow;
            playerText.GetComponent<Text>().text = "Project Manager";

        }
    }
}
