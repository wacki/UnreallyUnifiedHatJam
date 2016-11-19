using UnityEngine;
using System.Collections;
using DG.Tweening;

namespace UU.GameHam
{
	public class MovingPlatform : MonoBehaviour
	{


		public float duration;
		public GameObject end;
		public Ease easings = Ease.InOutSine;


		// Use this for initialization
		void Start ()
		{
			
			//Tweener object is used to animate a value, in this case it's the position of the transform
			Tweener moveTween = transform.DOMove (end.transform.localPosition, duration);
			moveTween.SetLoops (-1, LoopType.Yoyo);
			end.SetActive (false);

			//This makes the movement that we provide relative to the start position
			moveTween.SetRelative(true);

			//Smoothes out the parabola effect into a sin effect. Look for easings.net for more
			moveTween.SetEase (easings);

		}
	
		// Update is called once per frame
		void Update ()
		{
	
		}
	}
}