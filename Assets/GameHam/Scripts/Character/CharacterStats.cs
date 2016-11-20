using UnityEngine;
using System.Collections.Generic;
using System.Collections;

namespace UU.GameHam
{
    /// <summary>
    /// Keeps track of player health, energy and manages buffs/debuffs
    /// </summary>
    public class CharacterStats : MonoBehaviour
    {
        public Teams team;

        [Tooltip("Maximum health this character can have")]
        public int maxHealth = 2;

        [Tooltip("Maximum energy a character can have at any given point in time")]
        public int maxEnergy = 5;

        public float respawnTime = 10.0f;

        public CharacterType characterType;

        // current health
        public int currentHealth { get { return _currentHealth; } }
        private int _currentHealth;

        // current energy
        public int currentEnergy { get { return _currentEnergy; } }
        private int _currentEnergy;

        public bool isAlive { get { return _isAlive; } }
        private bool _isAlive = true;
        public float timeUntilRespawn { get { return _respawnTimer; } }
        private float _respawnTimer;

        private bool _isShieldActive;
        private int _currentShield;
        private float _shieldTimer;


        public int currentShield { get { return _currentShield; } }


        // current buffs and debuffs affecting the player
        private List<CharacterStatsModifier> _modifiers = new List<CharacterStatsModifier>();
        

        IEnumerator RespawnCoroutine()
        {
            _respawnTimer = respawnTime;
            while (_respawnTimer > 0.0f)
            {
                _respawnTimer -= Time.deltaTime;
                yield return null;
            }
            Spawn();
            _respawnTimer = 0.0f;
        }

        void Awake()
        {
            _currentHealth = maxHealth;
            _currentEnergy = maxEnergy;
        }

        public void SetTeam(Teams t)
        {
            team = t;
            int layer;
            if (team == Teams.Blue)
                layer = LayerMask.NameToLayer("TeamBlue");
            else
                layer = LayerMask.NameToLayer("TeamRed");

            gameObject.layer = layer;

            for (int i = 0; i < transform.childCount; i++)
                transform.GetChild(i).gameObject.layer = layer;
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

        public void AddHealth(int amount)
        {
            _currentHealth += amount;
            _currentHealth = (int)Mathf.Min(_currentHealth, maxHealth);
        }

        /// <summary>
        /// Damage the player
        /// </summary>
        /// <param name="amount"></param>
        public void Damage(int amount)
        {
            if (_isShieldActive)
            {

            }

            _currentHealth -= amount;
            DeathCheck();
        }

        /// <summary>
        /// Kills the player immediately
        /// </summary>
        public void Kill()
        {
            _isAlive = false;
			if (GetComponentInChildren<FlagManager> () != null)
				GetComponentInChildren<FlagManager> ().releaseFlag ();
            StartCoroutine(RespawnCoroutine());
            transform.position = new Vector3(0, -1000, 1);
        }

        /// <summary>
        /// Spawns the character at an available spawn point
        /// </summary>
        public void Spawn()
        {
            // reset health
            _currentEnergy = maxEnergy;
            _currentHealth = maxHealth;

            // set position back to one of the spawn points
            transform.position = GameManager.instance.GetEmptySpawnPoint(team);
            GetComponent<Rigidbody2D>().velocity = Vector2.zero;

            _isAlive = true;
        }

        private void DeathCheck()
        {
            if (_currentHealth <= 0)
                Kill();
        }

        /// <summary>
        /// Applies a specific modifier to this character
        /// </summary>
        /// <param name="mod"></param>
        public void ApplyModifier(CharacterStatsModifier mod)
        {
            // if the modifier is an instant effect then just apply it and return
            // we don't need to track what is happening to it
            if(mod.type == CharacterStatsModifier.Type.instant)
            {
                mod.OnActivate(gameObject);
                return;
            }


            var modInstance = (CharacterStatsModifier)ScriptableObject.CreateInstance(mod.GetType());
            modInstance.Copy(mod);
            Debug.Log(modInstance.duration);

            var inList = _modifiers.Find(x => x.GetType() == modInstance.GetType());
            if (inList == null)
            {

                modInstance.OnActivate(gameObject);
                _modifiers.Add(modInstance);
            }
            else
            {
                // refresh existing effect
                Debug.Log("EFFECT ALREADY PRESENT");
            }

        }

        void Update()
        {
            UpdateModifiers();

            // update shield
            if (_isShieldActive)
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
            foreach (var mod in toRemove)
            {
                // This might lead to a memory leak but the destory is causing issues
                //Destroy(mod);
            }
        }
    }
}
