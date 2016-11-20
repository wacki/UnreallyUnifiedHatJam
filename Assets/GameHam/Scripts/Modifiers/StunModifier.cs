using UnityEngine;
using System.Collections;


namespace UU.GameHam
{
    [CreateAssetMenu(menuName = "Stats Modifier/Stun Modifier")]
    public class StunModifier : CharacterStatsModifier
    {

        public override void OnActivate(GameObject go)
        {
            base.OnActivate(go);
            Debug.Log("Activating Control Reversal");
            var motor = go.GetComponent<Motor2D>();
            motor.Stun(duration);
        }

    }

}