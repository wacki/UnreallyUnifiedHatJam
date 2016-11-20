using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

namespace UU.GameHam
{

    public enum Teams
    {
        Blue,
        Red
    };

    public class GameManager : MonoBehaviour
    {
        public static GameManager instance { get { return _instance; } }
        private static GameManager _instance = null;
		public bool started = false;

        public CharacterType[] selectedTypes;

        public GameObject[] characterInstances { get { return _characterInstances; } }

        public GameObject[] characterPrefabs;

        public GameObject[] _characterInstances = new GameObject[4];

        private GameObject[] blueTeamSpawnPoints;
        private GameObject[] redTeamSpawnPoints;

		public float redFlags = 0;
		public float blueFlags = 0;

		public string gameSceneName;

        void Awake()
        {
            if (_instance != null)
            {
                Debug.LogError("YOU'RE TRYING TO INSTANTIATE MULTIPLE CHARACTER MANAGERS YOU FOOOOOOL!!!!!");
                return;
            }

            _instance = this;
            DontDestroyOnLoad(gameObject);

            //for(int i = 0; i < _characterInstances.Length; i++)
            //{
            //    _characterInstances[i] = Instantiate(characterPrefabs[0]);
            //    _characterInstances[i].GetComponent<PlayerController>().playerIndex = i;
            //    _characterInstances[i].GetComponent<CharacterStats>().SetTeam((i % 2 == 0) ? Teams.Blue : Teams.Red);
            //    _characterInstances[i].SetActive(false);

            //}
        }

        void OnLevelWasLoaded(int level)
        {
            for (int i = 0; i < _characterInstances.Length; i++)
            {
                int prefabIndex = (int)selectedTypes[i];

                _characterInstances[i] = Instantiate(characterPrefabs[prefabIndex]);
                _characterInstances[i].GetComponent<PlayerController>().playerIndex = i;
                _characterInstances[i].GetComponent<CharacterStats>().SetTeam((i < 2) ? Teams.Blue : Teams.Red);
                _characterInstances[i].SetActive(false);

            }
            StartRound();
            NotifyUI();
        }

        void Start()
        {



        }

		private void NotifyUI()
		{
			var ui = GameObject.FindObjectOfType<UI>();
			for(int i = 0; i < _characterInstances.Length; i++) {
				ui.players[i] = _characterInstances[i].GetComponent<CharacterStats>();
			}

			//ui.gameObject.SetActive (true);
		}


        public void StartRound()
        {
			started = true;
            foreach (var player in _characterInstances)
            {
                player.transform.position = GetEmptySpawnPoint(player.GetComponent<CharacterStats>().team);
                player.SetActive(true);
            }
        }

        public Vector3 GetEmptySpawnPoint(Teams team)
        {
            var spawnPoints = Resources.FindObjectsOfTypeAll<SpawnPoint>();
            foreach(var sp in spawnPoints)
            {
                if(sp.team == team)
                {
                    return sp.transform.position;
                }
            }

            return Vector3.zero;

        }

		public void gainFlagPoint(Teams t){
			if (t == Teams.Red) {
				redFlags++;
				SceneManager.LoadScene (gameSceneName);
			} else {
				blueFlags++;
				SceneManager.LoadScene (gameSceneName);
			}
		}
        

    }

}