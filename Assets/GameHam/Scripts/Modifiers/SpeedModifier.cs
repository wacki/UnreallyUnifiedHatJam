using UnityEngine;
using System.Collections;

namespace UU.GameHam
{
    // todo: add acceleration and speed modifiers as well as checkboxes
    [CreateAssetMenu(menuName = "Stats Modifier/Speed Modifier")]
    public class SpeedModifier : CharacterStatsModifier
    {
        private float _origVel;
        private float _origAcc;
        private Motor2D _motor;
        public bool affectMaxVelocity = true;
        public float limtedMaxVelocity;
        public bool affectAcceleration = true;
        public float limitedAcceleration;

        public SpeedModifier(SpeedModifier other)
        {
        }

        public override void Copy(CharacterStatsModifier other)
        {
            base.Copy(other);

            var otherCast = (SpeedModifier)other;
            affectMaxVelocity = otherCast.affectMaxVelocity;
            limtedMaxVelocity = otherCast.limtedMaxVelocity;
            affectAcceleration = otherCast.affectAcceleration;
            limitedAcceleration = otherCast.limitedAcceleration;

        }

        public override void OnActivate(GameObject go)
        {
            base.OnActivate(go);
            Debug.Log("Activating Speed Modifier");
            _motor = go.GetComponent<Motor2D>();
            _origVel = _motor.maxVelocity;
            _origAcc = _motor.moveAccel;

            if (affectAcceleration)
            {
                _motor.moveAccel = limitedAcceleration;
            }
            if (affectMaxVelocity)
            {
                _motor.maxVelocity = limtedMaxVelocity;
            }
        }

        public override void OnDeactivate()
        {
            base.OnDeactivate();
            _motor.maxVelocity = _origVel;
            Debug.Log("Speed modifier ran out!");

            _motor.maxVelocity = _origVel;
            _motor.moveAccel = _origAcc;

        }
    }

}