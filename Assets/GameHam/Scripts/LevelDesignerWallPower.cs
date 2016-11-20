using UnityEngine;
using System.Collections;

namespace UU.GameHam
{
    public class LevelDesignerWallPower : MonoBehaviour
    {
        public Teams team;
        public float duration;

        public GameObject wall;
        private bool isActive;
        private float timer;

        public void Awake()
        {
            if(team == Teams.Red)
                wall.layer = LayerMask.NameToLayer("TeamRed");
            else
                wall.layer = LayerMask.NameToLayer("TeamBlue");

            wall.SetActive(false);
        }

        public void Activate()
        {
            timer = 0.0f;
            isActive = true;
            wall.SetActive(true);
        }

        public void Update()
        {
            if(isActive)
            {
                timer += Time.deltaTime;
                
                if(timer > duration)
                {
                    isActive = false;
                    Deactivate();
                }
            }
        }

        private void Deactivate()
        {
            wall.SetActive(false);
        }
    }

}