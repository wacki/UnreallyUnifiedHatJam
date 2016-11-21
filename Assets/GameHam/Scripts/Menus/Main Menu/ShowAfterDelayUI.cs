using UnityEngine;
using UnityEngine.UI;
using System.Collections;

namespace UU.GameHam
{

    public class ShowAfterDelayUI : MonoBehaviour
    {
        CanvasRenderer cr;
        public float delay;

        void Awake()
        {
            cr = GetComponent<CanvasRenderer>();
            Show(false);
            StartCoroutine(ShowCoroutine());
        }
      
        IEnumerator ShowCoroutine()
        {
            bool hidden = true;
            float timer = delay;


            while(hidden) {
                timer -= Time.deltaTime;
                if(timer < 0.0f)
                {
                    hidden = false;
                    Show(true);
                    break;
                }

                yield return null;
            }

            yield return null;
        }

        private void Show(bool show)
        {
                cr.SetAlpha((show) ? 1 : 0);
        }
    }

}