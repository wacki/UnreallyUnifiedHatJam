using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
namespace UU.GameHam
{

    public class StartGameTimer : MonoBehaviour
    {
        public float gameStartCountDown;

        public float currentTime;
        public Text startCountdownText;

        public string gameSceneName;
        

        public CharacterSelection[] playerSelections;

        private bool countdownStarted = false;

        void Update()
        {
            if (countdownStarted) return;

            foreach (var ps in playerSelections)
            {
                if (!ps.lockedIn)
                    return;
            }

            // if we end up here that means a ll players are locked in
            // make sure they can't do anything with their selection anymore
            foreach (var ps in playerSelections)
            {
                ps.lockInOverride = true;
            }

            countdownStarted = true;
            StartCoroutine(GameStartCountDown());

        }

        IEnumerator GameStartCountDown()
        {
            currentTime = gameStartCountDown;

            GameManager.instance.selectedTypes = new CharacterType[4];
            var types = GameManager.instance.selectedTypes;



            for(int i = 0; i < playerSelections.Length; i++)
            {
                types[playerSelections[i].playerIndex] = playerSelections[i].currentSelection;
            }

            while (currentTime > 0.0f)
            {
                Debug.Log("COUNTOWN: " + currentTime);
                startCountdownText.text = "GAME STARTS IN " + currentTime + " SECONDS";
                currentTime -= 1.0f;
                yield return new WaitForSeconds(1.0f);
            }
            startCountdownText.text = "GAME STARTS IN " + 0 + " SECONDS";
            StartGame();

            yield return null;
        }

		public void ForceStart()
		{
			StartCoroutine(GameStartCountDown());
		}

        void StartGame()
        {
            Debug.Log("START GAME!");
            SceneManager.LoadScene(gameSceneName);
        }
    }

}