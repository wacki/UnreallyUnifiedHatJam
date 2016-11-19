using UnityEngine;
using System.Collections;

namespace UU.GameHam
{

    public enum Teams
    {
        Blue,
        Red
    };

    public class CharacterManager : MonoBehaviour
    {
        public static CharacterManager instance { get { return _instance; } }
        private static CharacterManager _instance = null;

        public GameObject[] characterPrefabs;

        private GameObject[] _characterInstances = new GameObject[4];

        private GameObject[] blueTeamSpawnPoints;
        private GameObject[] redTeamSpawnPoints;

        void Awake()
        {
            if (_instance != null)
            {
                Debug.LogError("YOU'RE TRYING TO INSTANTIATE MULTIPLE CHARACTER MANAGERS YOU FOOOOOOL!!!!!");
                return;
            }

            _instance = this;
            DontDestroyOnLoad(gameObject);

            for(int i = 0; i < _characterInstances.Length; i++)
            {
                _characterInstances[i] = Instantiate(characterPrefabs[0]);
                _characterInstances[i].SetActive(false);
                _characterInstances[i].GetComponent<PlayerController>().playerIndex = i;
                _characterInstances[i].GetComponent<CharacterStats>().team = (i % 2 == 0) ? Teams.Blue : Teams.Red;
            }
        }

        public void StartRound()
        {
            foreach (var player in _characterInstances)
            {
                player.transform.position = GetEmptySpawnPoint(player.GetComponent<CharacterStats>().team);
            }
        }

        Vector3 GetEmptySpawnPoint(Teams team)
        {
            return Vector3.zero;
        }
        

    }

}