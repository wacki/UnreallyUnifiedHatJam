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

        public float timeLeft { get { return duration - _timer; } }

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

        public void ForceStop()
        {
            _isActive = false;
            OnDeactivate();
        }

        public void Refresh()
        {
            _timer = 0.0f;
            Debug.Log(" I GOT REFRESHED!");
        }
        
        public virtual void Copy(CharacterStatsModifier other)
        {
            duration = other.duration;
            type = other.type;
        }   
    }

    




}

