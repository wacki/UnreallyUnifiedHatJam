using UnityEngine;
using System.Collections;

namespace UU.GameHam
{
	public class AnimationController : MonoBehaviour
	{

		//Animation
		private Animator anim;
		AnimatorStateInfo currentState;
		private Vector3 initialScale;
		float playbackTime;

		// Use this for initialization
		void Start ()
		{
			//Get Animation
			anim = GetComponent<Animator> ();
			initialScale = gameObject.transform.localScale;
		}
	
		// Update is called once per frame
		void Update ()
		{
			//get animations
			currentState = anim.GetCurrentAnimatorStateInfo (0);
			playbackTime = currentState.normalizedTime % 1;

			if (gameObject.GetComponent<Motor2D> ().facingRight)
				transform.localScale = new Vector3 (initialScale.x, initialScale.y, initialScale.z);
			else
				transform.localScale = new Vector3 (-initialScale.x, initialScale.y, initialScale.z);

		}

		//Let others call animation into object
		public void CallAnimation (string animName)
		{
			anim.Play (animName, -1, 0f);	//If you call for the animation ATTACK, even if  a different player class calls it, as long as they both have the name "ATTACK" for their attack animation, this should work
		}

		//Check if a specific animation is running
		public bool IsAnimationRunning (string animName)
		{
			return (anim.GetCurrentAnimatorStateInfo (0).IsName (animName));
		}
	}

}
