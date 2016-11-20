using UnityEngine;
using System.Collections;

namespace UU.GameHam
{
    public class CharacterStatsModifier : ScriptableObject
    {

        public enum Type
        {
            instant,
            timed
        };
        public Type type;
        public float duration;

        private bool _isActive = false;
        public bool isActive { get { return _isActive; } }

        private float _timer;

        public virtual void OnActivate(GameObject go)
        {
            _timer = 0.0f;
            _isActive = true;
        }
        public virtual void OnDeactivate()
        {
            _isActive = false;
        }

        public virtual void OnUpdate()
        {
            if (_timer > duration)
            {
                OnDeactivate();
                return;
            }

            _timer += Time.deltaTime;
        }
        
        public virtual void Copy(CharacterStatsModifier other)
        {
            duration = other.duration;
            type = other.type;
        }   
    }

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

            if(affectAcceleration)
            {
                _motor.moveAccel = limitedAcceleration;
            }
            if(affectMaxVelocity)
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



    [CreateAssetMenu(menuName = "Stats Modifier/Control Reversal")]
    public class ControlReversalModifier : CharacterStatsModifier
    {
        private PlayerController playerController;

        public override void OnActivate(GameObject go)
        {
            base.OnActivate(go);
            Debug.Log("Activating Control Reversal");
            playerController = go.GetComponent<PlayerController>();
            playerController.reversControls = true;
        }

        public override void OnDeactivate()
        {
            base.OnDeactivate();
            Debug.Log("Control Reversal ran out!");
            playerController.reversControls = false;
        }
    }


    [CreateAssetMenu(menuName = "Stats Modifier/Shield Modifier")]
    public class ShieldModifier : CharacterStatsModifier
    {
        public int shieldAmount;
        public float shieldDuration;

        public override void OnActivate(GameObject go)
        {
            base.OnActivate(go);
            Debug.Log("Activating Shield Modifier");

            // activate the shield
            var cs = go.GetComponent<CharacterStats>();
            cs.ActivateShield(shieldAmount, shieldDuration);
        }
    }

    [CreateAssetMenu(menuName = "Stats Modifier/Health")]
    public class HealthModifier : CharacterStatsModifier
    {
        public int healthAmount;
        public override void OnActivate(GameObject go)
        {
            base.OnActivate(go);
            Debug.Log("Activating Health Modifier");
            var cs = go.GetComponent<CharacterStats>();
            cs.AddHealth(healthAmount);
        }
    }

    [CreateAssetMenu(menuName = "Stats Modifier/Energy Modifier")]
    public class EnergyModifier : CharacterStatsModifier
    {
        public override void OnActivate(GameObject go)
        {
            base.OnActivate(go);
            Debug.Log("Activating Energy Modifier");
            go.GetComponent<CharacterStats>().FillEnergy();
        }
    }
    
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

