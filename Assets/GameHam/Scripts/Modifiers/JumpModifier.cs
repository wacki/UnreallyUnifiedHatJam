using UnityEngine;
using System.Collections;

namespace UU.GameHam
{

    [CreateAssetMenu(menuName = "Stats Modifier/Jump Modifier")]
    public class JumpModifier : CharacterStatsModifier
    {
        public float modifiedJumpSpeed;
        private Motor2D motor;
        private float origJumpSpeed;

        public override void Copy(CharacterStatsModifier other)
        {
            base.Copy(other);
            var otherCast = (JumpModifier)other;

            modifiedJumpSpeed = otherCast.modifiedJumpSpeed;
        }

        public override void OnActivate(GameObject go)
        {
            base.OnActivate(go);
            Debug.Log("Activating Control Reversal");
            motor = go.GetComponent<Motor2D>();
            origJumpSpeed = motor.jumpVelocity;
            motor.jumpVelocity = modifiedJumpSpeed;
        }

        public override void OnDeactivate()
        {
            base.OnDeactivate();
            Debug.Log("Control Reversal ran out!");
            motor.jumpVelocity = origJumpSpeed;
        }
    }
}