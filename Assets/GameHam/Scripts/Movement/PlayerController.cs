using UnityEngine;
using System.Collections;


namespace UU.GameHam
{

    [RequireComponent(typeof(Motor2D))]
    public class PlayerController : MonoBehaviour {
        public bool debug;
        public float debugInterval;

        public int playerIndex = 0;

        private Motor2D _motor;
		private CombatController _combat;

#if UNITY_EDITOR
        Vector3 _prevPosition;
#endif

        void Awake()
        {
			_combat = GetComponent<CombatController>();
			_motor = GetComponent<Motor2D>();
            _prevPosition = transform.position;

        }

        void Update()
        {            

            float v = GetAxis("LSY");
            float h = GetAxis("LSX");

            if (GetButtonDown("Button0"))
                _motor.StartJump();
            else if (GetButtonUp("Button0"))
                _motor.StopJump();

			if (GetButtonDown ("Button1"))
				_combat.Shoot ();

			if (GetButtonDown ("Button2"))
				_combat.Attack ();

            _motor.Move(h, v);


#if UNITY_EDITOR
            if (debug) {
                //Debug.Log("DEBUGGING");
                Debug.DrawLine(_prevPosition, transform.position, Color.green, 2.0f);
                _prevPosition = transform.position;
            }
#endif
        }



        private float GetAxis(string name)
        {
            return Input.GetAxis("Joy" + playerIndex + name);
        }
        private bool GetButtonDown(string name)
        {
            return Input.GetButtonDown("Joy" + playerIndex + name);
        }
        private bool GetButtonUp(string name)
        {
            return Input.GetButtonUp("Joy" + playerIndex + name);
        }
        private bool GetButton(string name)
        {
            return Input.GetButton("Joy" + playerIndex + name);
        }

    }
}