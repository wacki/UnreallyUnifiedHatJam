using UnityEngine;
using System.Collections;

namespace UU.GameHam
{
    public class ModifierStatusBar : MonoBehaviour
    {
        public GameObject stunIcon;
        public GameObject reverseIcon;
        public GameObject speedIcon;
        public GameObject slowIcon;
        public GameObject nojumpIcon;

        public CharacterStats charStats;
        public Motor2D motor2D;

        private Canvas canvas;

        void Start()
        {
            canvas = GetComponent<Canvas>();
        }

        private void DeactivateAll()
        {
            stunIcon.SetActive(false);
            reverseIcon.SetActive(false);
            speedIcon.SetActive(false);
            slowIcon.SetActive(false);
            nojumpIcon.SetActive(false);
        }

        private void Update()
        {
            DeactivateAll();
            int visibleIcons = 0;

            foreach (var mod in charStats.modifiers)
            {
                if (mod.GetType() == typeof(ControlReversalModifier))
                {
                    reverseIcon.SetActive(true);
                    visibleIcons++;
                }
                else if (mod.GetType() == typeof(SpeedModifier))
                {
                    if (((SpeedModifier)mod).limtedMaxVelocity < 5)
                    {
                        slowIcon.SetActive(true);
                        visibleIcons++;
                    }
                    else
                    {
                        speedIcon.SetActive(true);
                        visibleIcons++;
                    }


                }
                else if (mod.GetType() == typeof(JumpModifier))
                {
                    nojumpIcon.SetActive(true);
                    visibleIcons++;
                }

            }

            if(motor2D.isStunned)
            {
                stunIcon.SetActive(true);
                visibleIcons++;
            }
            
            //canvas.GetComponent<RectTransform>().sizeDelta = new Vector2(visibleIcons*200, 200);
        }
    }
}