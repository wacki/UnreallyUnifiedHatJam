using UnityEngine;
using System.Collections;

namespace UU.GameHam
{
    public class PlayerIndicatorScript : MonoBehaviour
    {
        public PlayerController playerController;
        public Sprite[] indicators;

        private void Start()
        {
            var sr = GetComponent<SpriteRenderer>();
            sr.sprite = indicators[playerController.playerIndex];
        }
    }

}