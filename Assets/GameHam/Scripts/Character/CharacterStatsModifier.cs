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
        private Motor2D _motor;

        public SpeedModifier(SpeedModifier other)
        {
        }

        public override void OnActivate(GameObject go)
        {
            base.OnActivate(go);
            Debug.Log("Activating Speed Modifier");
            _motor = go.GetComponent<Motor2D>();
            _origVel = _motor.maxVelocity;
            _motor.maxVelocity = 2.0f;
        }

        public override void OnDeactivate()
        {
            base.OnDeactivate();
            _motor.maxVelocity = _origVel;
            Debug.Log("Speed modifier ran out!");
        }
    }



    [CreateAssetMenu(menuName = "Stats Modifier/Control Reversal")]
    public class ControlReversalModifier : CharacterStatsModifier
    {
        public override void OnActivate(GameObject go)
        {
            base.OnActivate(go);
            Debug.Log("Activating Control Reversal");
        }

        public override void OnDeactivate()
        {
            base.OnDeactivate();
            Debug.Log("Control Reversal ran out!");
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
        public override void OnActivate(GameObject go)
        {
            base.OnActivate(go);
            Debug.Log("Activating Health Modifier");
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
        public override void OnActivate(GameObject go)
        {
            base.OnActivate(go);
            Debug.Log("Activating Control Reversal");
        }

        public override void OnDeactivate()
        {
            base.OnDeactivate();
            Debug.Log("Control Reversal ran out!");
        }
    }


    [CreateAssetMenu(menuName = "Stats Modifier/Stun Modifier")]
    public class StunModifier : CharacterStatsModifier
    {
        public override void OnActivate(GameObject go)
        {
            base.OnActivate(go);
            Debug.Log("Activating Control Reversal");
        }

        public override void OnDeactivate()
        {
            base.OnDeactivate();
            Debug.Log("Control Reversal ran out!");
        }
    }

}

