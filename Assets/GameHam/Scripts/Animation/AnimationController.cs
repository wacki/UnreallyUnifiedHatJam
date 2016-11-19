using UnityEngine;
using System.Collections;

public class AnimationController : MonoBehaviour {

	//Animation
	private Animator anim;
	AnimatorStateInfo currentState;
	float playbackTime;

	// Use this for initialization
	void Start () {
		//Get Animation
		anim = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
		//get animations
		currentState = anim.GetCurrentAnimatorStateInfo (0);
		playbackTime = currentState.normalizedTime % 1;

	}

	//Let others call animation into object
	public void CallAnimation(string animName){
		anim.Play (animName, -1, 0f);	//If you call for the animation ATTACK, even if  a different player class calls it, as long as they both have the name "ATTACK" for their attack animation, this should work
	}

	//Check if a specific animation is running
	public bool IsAnimationRunning(string animName)
	{
		return (anim.GetCurrentAnimatorStateInfo (0).IsName (animName));
	}

}
