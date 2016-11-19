using UnityEngine;
using System.Collections;


namespace UU.GameHam
{

    [RequireComponent(typeof(Motor2D))]
    public class PlayerController : MonoBehaviour {
        public GameObject aimDirCanvas;
        public bool debug;
        public float debugInterval;

        private Motor2D _motor;

#if UNITY_EDITOR
        Vector3 _prevPosition;
#endif

        void Awake()
        {
            _motor = GetComponent<Motor2D>();
            _prevPosition = transform.position;
        }

        void Update()
        {
            float v = Input.GetAxis("Vertical");
            float h = Input.GetAxis("Horizontal");

            if(Input.GetButtonDown("Jump"))
                _motor.StartJump();
            else if(Input.GetButtonUp("Jump"))
                _motor.StopJump();


            if(Input.GetButtonDown("Interact")) {
                //_motor.Grab();
            }
            else if(Input.GetButtonUp("Interact")) {
                //_motor.Release();
            }
            
                        
            _motor.Move(h, v);


#if UNITY_EDITOR
            if(debug) {
                //Debug.Log("DEBUGGING");
                Debug.DrawLine(_prevPosition, transform.position, Color.green, 2.0f);
                _prevPosition = transform.position;
            }
#endif
        }

    }
}