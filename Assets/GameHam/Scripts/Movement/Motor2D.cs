using UnityEngine;
using System.Collections;
using System.Collections.Generic;

#if UNITY_EDITOR
using UnityEditor;
#endif

namespace UU.GameHam {

    [RequireComponent(typeof(BoxCollider2D), typeof(Rigidbody2D))]
    public class Motor2D : MonoBehaviour {

        public enum State {
            Jumping,    // currently jumping (jump button held)
            Falling,    // falling
            Grounded,   // moving along the ground
            Walltouch,  // touching a wall while not grounded (wall slide, wall jump etc use this)
            Climbing    // player is currently holding onto a wall
        };

        #region public editor fields

        // editable fields
        [Tooltip("Velocity for jump and walljumps")]
        public float jumpVelocity = 8.0f;

        [Tooltip("How long can the jump button be held to continue the jump")]
        public float maxJumpDuration = 0.3f;

        [Tooltip("Amount of air jumps the character is able to do")]
        public int maxAirJumps = 1;

        [Tooltip("Wheather to allow walljumps or not")]
        public bool allowWalljumps = true;

        [Tooltip("Jump trajectory angle when performing a wall jump")]
        public float wallJumpAngle = 45;

        [Tooltip("Jump velocity for wall jumps is determined by multiplying this value with jumpVelocity")]
        public float wallJumpVelocityModifier = 3;

        [Tooltip("Acceleration when grounded")]
        public float moveAccel = 40.0f;

        [Tooltip("Maximum velocity the character can achieve while running")]
        public float maxVelocity = 8.0f;

        [Tooltip("Acceleration when grounded"), Range(0.0f, 1.0f)]
        public float airControlFactor = 1.0f;

        [Tooltip("Maximum angle the character can walk up before sliding down a slope"), Range(0.0f, 90.0f)]
        public float slipAngle = 30.0f;
        
        // advanced settings below (todo: hide thos in advanced foldout or something)
        [Tooltip("Distance at which we'll check for walls")]
        public float wallCheckDistance = 0.2f;

        [Tooltip("Number of rays to use along the character's main collider to check for walls"), Range(1, 12)]
        public int numWallCheckRays = 3;

        [Tooltip("Distance at which we'll check for the ground")]
        public float groundCheckDistance = 0.2f;

        [Tooltip("Number of rays to use to check for the floor beneath the character"), Range(1, 12)]
        public int numGroundCheckRays = 3;

        [Tooltip("Wheather or not to reset air jumps after touching a wall")]
        public bool resetAirJumpsOnWallTouch = true;
        

        #endregion

        #region public properties

        // getters
        public bool isGrounded { get { return _state == State.Grounded; } }
        public bool facingRight { get { return _facingRight; } }
        public State state { get { return _state; } }
        public Vector2 velocity { get { return _rb.velocity; } }
        public Vector2 facingDir { get { return ((facingRight) ? transform.right : -transform.right); } }
        #endregion

        /// <summary> 
        /// Move the character based on user input
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        public void Move(float x, float y)
        {
            Move(new Vector2(x, y));
        }

        /// <summary>
        /// Move the character based on user input
        /// </summary>
        /// <param name="input"></param>
        public void Move(Vector2 input)
        {
            _inputDir = input;
        }

        /// <summary>
        /// Begin jumping
        /// </summary>
        public void StartJump()
        {
            if(!isGrounded && _airJumpsLeft < 1)
                return;

            // decrease number of airjumps left if we're not grounded
            if(!isGrounded && state != State.Walltouch)
                _airJumpsLeft--;

            var vel = _rb.velocity;

            if(allowWalljumps && state == State.Walltouch) {
                var axis = (_currentSurface.facingRight) ? Vector3.forward : Vector3.back;
                Vector2 dir = Quaternion.AngleAxis(90 - wallJumpAngle, axis) * _currentSurface.normal;
                dir.Normalize();
                vel = dir * jumpVelocity * wallJumpVelocityModifier;
                Debug.DrawLine(_currentSurface.hitPoint, dir * 5 + _currentSurface.hitPoint, Color.red, 1);
            }
            else {
                vel.y = jumpVelocity;
            }

            _rb.velocity = vel;



            _jumpHoldTimer = 0.0f;
            _jumping = true;
            SetState(State.Jumping);
        }

        /// <summary>
        /// Stop current jump
        /// </summary>
        public void StopJump()
        {
            _jumping = false;
        }
        
        #region private 

        /// <summary>
        /// Current state the character is in
        /// </summary>
        private State _state;

        /// <summary>
        /// Current facing direction of the character
        /// </summary>
        private bool _facingRight;

        /// <summary>
        /// Current input direction
        /// </summary>
        private Vector2 _inputDir;

        /// <summary>
        /// Reference to the attached rigidbody2D
        /// </summary>
        private Rigidbody2D _rb;

        /// <summary>
        /// Reference to the attached collider2D
        /// </summary>
        private BoxCollider2D _col;

        /// <summary>
        /// Holds current number of air jumps the player has left
        /// </summary>
        private int _airJumpsLeft;
        
        /// <summary>
        /// Current normal vector for the floor
        /// </summary>
        private Vector2 _groundNormal;

        /// <summary>
        /// True while the jump button is being held down
        /// </summary>
        private bool _jumping;

        /// <summary>
        /// Number of seconds the jump button was held for
        /// </summary>
        private float _jumpHoldTimer;
        

        class Climbable {
            public Collider2D collider;
            public Vector2 normal;
            public Vector2 hitPoint;
            public Quaternion orientation;
            public Vector2 rayOrigin;
            public bool facingRight;
        }

        private Climbable _currentSurface;


        /// <summary>
        /// Setup on start of play
        /// </summary>
        private void Awake()
        {
            _rb = GetComponent<Rigidbody2D>();
            _col = GetComponent<BoxCollider2D>();

            _currentSurface = new Climbable();
        }


        /// <summary>
        /// All of the character updates happen in here
        /// </summary>
        private void Update()
        {
            // update any movement
            UpdateFacing();

            // if we're in a jumping state then see if we need to keep on jumping
            if(state == State.Jumping) {
                if(_jumping && _jumpHoldTimer < maxJumpDuration) {
                    // while 
                    var vel = _rb.velocity;
                    vel.y = jumpVelocity;
                    _rb.velocity = vel;

                    // update jump hold timer
                    _jumpHoldTimer += Time.deltaTime;
                }
                // after leaving the jump state we should transition into falling
                else {
                    SetState(State.Falling);
                }

            }
            // if we're not jumping then do a ground check
            else {
                WallCheck();
                GroundCheck();
            }

            UpdateMovement();
        }

        private void UpdateFacing()
        {
            if(_inputDir.x > 0.0f)
                _facingRight = true;
            else if(_inputDir.x < 0.0f)
                _facingRight = false;
        }

        /// <summary>
        /// Update character movement
        /// </summary>
        private void UpdateMovement()
        {
            var vel = _rb.velocity;
            var inputX = _inputDir.x;

            // dampen current speed
            if(Mathf.Abs(inputX) < Mathf.Epsilon)
                vel.x = Mathf.Lerp(vel.x, 0.0f, Time.fixedDeltaTime * 10); // todo: explose this factor to the designer

            if(state == State.Walltouch && !isGrounded) {
                // special case, movement input while touching a wall in the air


            }
            else {
                // apply air control factor if not on the ground
                if(!isGrounded) {
                    inputX *= airControlFactor;
                }

                // update velocity
                vel.x += inputX * moveAccel * Time.fixedDeltaTime;
                vel.x = Mathf.Clamp(vel.x, -maxVelocity, maxVelocity);
            }

            _rb.velocity = vel;
        }

        /// <summary>
        /// Check if we hit a wall in our current facing direction
        /// </summary>
        private void WallCheck()
        {
            _currentSurface.collider = null;
            if(isGrounded)
                return;


            Vector2 dir = facingDir;
            Vector2 min = _col.bounds.min;
            Vector2 max = _col.bounds.max;

            // todo: put these offsets into a variable
            min.y += 0.1f;
            max.y -= 0.1f;

            if(facingRight)
                min.x = _col.bounds.max.x;
            else
                max.x = _col.bounds.min.x;

            var originLine = max - min;
            var center = min + originLine * 0.5f;

            bool odd = numWallCheckRays % 2 != 0;
            int numRaysEven = numWallCheckRays - (numWallCheckRays % 2);
            int numRayHalf = (int)Mathf.Floor((float)numRaysEven * 0.5f);
            var offset = originLine * (1.0f / (float)numRaysEven);

            // if we're using an odd number of rays then we start with a center ray
            if(odd)
                WallCheck(center, dir);

            for(int i = 0; i < numRayHalf; i++) {
                WallCheck(center + offset * (i + 1), dir);
                WallCheck(center - offset * (i + 1), dir);

                if(_currentSurface.collider != null)
                    return;
            }

            SetState(State.Falling);
        }

		public bool IsFacingRight()
		{
			return _facingRight;
		}


        private void WallCheck(Vector2 origin, Vector2 dir)
        {
            if(_currentSurface.collider != null)
                return;

            var hit = Physics2D.Raycast(origin, dir, wallCheckDistance);


            if(hit.collider != null) {
                _currentSurface.collider = hit.collider;
                _currentSurface.normal = hit.normal;
                _currentSurface.facingRight = (Vector2.Dot(hit.normal, Vector2.right) > 0.0f);
                _currentSurface.hitPoint = hit.point;
                _currentSurface.orientation = Quaternion.FromToRotation(Vector3.right, hit.normal);
                _currentSurface.rayOrigin = origin;

                Debug.DrawLine(origin, origin + dir * wallCheckDistance, Color.green);

                SetState(State.Walltouch);

                return;
            }

            Debug.DrawLine(origin, origin + dir * wallCheckDistance, Color.red);
        }


        /// <summary>
        /// Check if we're on the ground
        /// todo: get rid of repetition in WallCheck and GroundCheck
        /// </summary>
        private void GroundCheck()
        {
            float min = -_col.size.x * 0.5f;
            float max = min * -1;

            var distance = max - min;

            bool odd = numGroundCheckRays % 2 != 0;
            int numRaysEven = numGroundCheckRays - (numGroundCheckRays % 2);
            int numRayHalf = (int)Mathf.Floor((float)numRaysEven * 0.5f);
            float offset = distance * (1.0f / (float)numRaysEven);

            // if we're using an odd number of rays then we start with a center ray
            if(odd) {
                if(GroundCheck(0.0f))
                    return;
            }

            for(int i = 0; i < numRayHalf; i++) {
                if(GroundCheck(offset * (i + 1)))
                    return;
                if(GroundCheck(-offset * (i + 1)))
                    return;
            }

            // if we're still grounded at this point then we need to correct that
            if(isGrounded)
                SetState(State.Falling);
        }

        /// <summary>
        /// Todo: fix the ray origin calculation, this only works if we're parallel to the ground!
        /// </summary>
        /// <param name="offsetX"></param>
        /// <returns></returns>
        private bool GroundCheck(float offsetX)
        {
            var offset = new Vector2(offsetX, 0.1f);
            var origin = (Vector2)transform.position + offset;
            var hitInfo = Physics2D.Raycast(origin, -transform.up, groundCheckDistance);
            if(hitInfo.collider != null) {

                // if the origin of our ground check ray was inside the hit body we can't be sure 
                // if it's not glitching inside a wall and giving us a false positive
                if(hitInfo.collider.bounds.Contains(origin))
                    return false;

                SetState(State.Grounded);
                _groundNormal = hitInfo.normal;

                Debug.DrawRay((Vector2)transform.position + offset, -transform.up * groundCheckDistance, Color.green);

                return true;
            }            

            Debug.DrawRay((Vector2)transform.position + offset, -transform.up * groundCheckDistance, Color.red);
            return false;
        }

        /// <summary>
        /// Reset number of airjumps
        /// </summary>
        private void ResetAirJumps()
        {
            _airJumpsLeft = maxAirJumps;
        }

        /// <summary>
        /// poor man's state machine
        /// </summary>
        /// <param name="state"></param>
        void SetState(State state)
        {
            // don't do anything if we're already in the same state
            if(state == _state)
                return;

            // handle exit and enter events
            OnExitState(_state, state);
            OnEnterState(state, _state);

            // actually switch the state
            _state = state;
        }

        /// <summary>
        /// Properly handle entering a new state
        /// </summary>
        /// <param name="state"></param>
        /// <param name="prevState"></param>
        void OnEnterState(State state, State prevState)
        {
            switch(state) {
                case State.Grounded:
                    ResetAirJumps();
                    return;
                case State.Climbing:
                    return;
                case State.Walltouch:
                    if(resetAirJumpsOnWallTouch)
                        ResetAirJumps();
                    return;
            }
        }

        /// <summary>
        /// properly handle exiting an old state
        /// </summary>
        /// <param name="state"></param>
        /// <param name="nextState"></param>
        void OnExitState(State state, State nextState)
        {
            switch(state) {
                case State.Climbing:
                    return;
                case State.Walltouch:
                    return;
            }
        }


        #region editor

#if UNITY_EDITOR
        void OnDrawGizmos()
        {

        }

        void OnDrawGizmosSelected()
        {

        }

        public string DebugOut()
        {
            string result = "State: " + state + "\n";
            result += "Vel: " + _rb.velocity + "\n";
            result += "Air jumps left: " + _airJumpsLeft + "\n"; 
            result += "Ground: " + _groundNormal + "\n";
            result += "Ground normal: " + "todo" + "\n\n";

            if(_currentSurface.collider == null) {
                result += "Wall: none\n";
            }
            else {
                result += "Wall: " + _currentSurface.collider.name + "\n";
                result += "  facing: " + (_currentSurface.facingRight ? "right" : "left") + "\n";
                result += "  normal: " + _currentSurface.normal + "\n";
                result += "  angle: " + _currentSurface.orientation + "\n";
                result += "  hitPoint: " + _currentSurface.hitPoint + "\n";
            }

            return result;
        }
#endif

        #endregion

        #endregion

    }

}