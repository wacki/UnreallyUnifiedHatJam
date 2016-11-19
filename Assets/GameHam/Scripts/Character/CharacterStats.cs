using UnityEngine;
using System.Collections.Generic;

namespace UU.GameHam
{
    /// <summary>
    /// Keeps track of player health, energy and manages buffs/debuffs
    /// </summary>
    public class CharacterStats : MonoBehaviour
    {        

        [Tooltip("Maximum health this character can have")]
        public int maxHealth = 3;
        
        [Tooltip("Maximum energy a character can have at any given point in time")]
        public int maxEnergy = 5;

        // current health
        public int currentHealth { get { return _currentHealth; } }
        private int _currentHealth;

        // current energy
        public int currentEnergy { get { return _currentEnergy; } }
        private int _currentEnergy;

        private bool _isShieldActive;
        private int _currentShield;
        private float _shieldTimer;


        public int currentShield { get { return _currentShield; } }

        // current respawn timer
        private float _respawnTimer;

        // current buffs and debuffs affecting the player
        private List<CharacterStatsModifier> _modifiers = new List<CharacterStatsModifier>();

        public CharacterStatsModifier tempTestBuff;

        void Awake()
        {
            // Test buff system temporarily
            ApplyModifier(tempTestBuff);
        }

        public void ActivateShield(int shieldAmount, float shieldTimer)
        {
            _isShieldActive = true;
            _currentShield = shieldAmount;
            _shieldTimer = shieldTimer;
        }

        public void FillEnergy()
        {
            _currentEnergy = maxEnergy;
        }

        /// <summary>
        /// Damage the player
        /// </summary>
        /// <param name="amount"></param>
        public void Damage(int amount)
        {
            if(_isShieldActive)
            {

            }

        }

        /// <summary>
        /// Kills the player immediately
        /// </summary>
        public void Kill()
        {

        }

        /// <summary>
        /// Spawns the character at an available spawn point
        /// </summary>
        public void Spawn()
        {

        }
        
        /// <summary>
        /// Applies a specific modifier to this character
        /// </summary>
        /// <param name="mod"></param>
        public void ApplyModifier(CharacterStatsModifier mod)
        {
            mod.OnActivate(gameObject);
            _modifiers.Add(mod);
        }

        void Update()
        {
            UpdateModifiers();

            // update shield
            if(_isShieldActive)
            {
                _shieldTimer -= Time.deltaTime;
                if (_shieldTimer <= 0.0f)
                    _isShieldActive = false;
            }

        }

        void UpdateModifiers()
        {
            var toRemove = new List<CharacterStatsModifier>();
            foreach (var mod in _modifiers)
            {
                mod.OnUpdate();

                // flag for removal
                if (!mod.isActive)
                    toRemove.Add(mod);
            }

            // remove buffs that ran out
            _modifiers.RemoveAll(x => toRemove.Contains(x));
        }
    }
}
