using UnityEngine;
using System.Collections;

namespace UU.GameHam
{

    public class CharacterSpecialAbility : MonoBehaviour
    {
        public CharacterStatsModifier effect;
        public CharacterType characterType;
        public ArtistGhostSpecial ghostPrefab;


        public AudioClip specialActivateSound;

        private float cooldownTimer;
        private CharacterStats charStats;

        void Awake()
        {
            charStats = GetComponent<CharacterStats>();
        }

        public virtual void Use()
        {
            switch (characterType)
            {
                case CharacterType.Artist: UseArtist(); return;
                case CharacterType.Programmer: UseProgrammer(); return;
                case CharacterType.LevelDesigner: UseLevelDesigner(); return;
                case CharacterType.ProjectManager: UseProjectManager(); return;
            }

        }

        private bool SpendEnergyForAbilityUse()
        {
            if (charStats.currentEnergy < charStats.maxEnergy)
                return false;

            charStats.SpendEnergy();

			var animController = gameObject.GetComponent<AnimationController>();
			if(animController != null)
				animController.CallAnimation ("Special"); //Call for the animation to attack
			
            if (specialActivateSound != null)
                AudioManager.instance.PlayOneShot(specialActivateSound);


            return true;

        }

        private void UseArtist()
        {
            if (!SpendEnergyForAbilityUse())
                return;

            var team = charStats.team;
            

            float distance = -1;
            CharacterStats closestEnemy = null;
            foreach (var player in GameManager.instance.characterInstances)
            {
                var enemyCS = player.GetComponent<CharacterStats>();
                if (enemyCS.team != team)
                {
                    if (distance < 0.0f)
                    {
                        distance = Vector3.Distance(player.transform.position, transform.position);
                        closestEnemy = enemyCS;
                    }
                    else
                    {
                        if (Vector3.Distance(player.transform.position, transform.position) < distance)
                            closestEnemy = enemyCS;
                    }
                }
            }

            var ghostInstance = Instantiate(ghostPrefab, transform.position, Quaternion.identity) as ArtistGhostSpecial;
            ghostInstance.target = closestEnemy.gameObject;

        }
        private void UseProgrammer()
        {
            if (!SpendEnergyForAbilityUse())
                return;

            if (effect == null)
                return;

            

            var team = charStats.team;

            foreach (var player in GameManager.instance.characterInstances)
            {
                var enemyCS = player.GetComponent<CharacterStats>();
                if (enemyCS.team != team)
                    enemyCS.ApplyModifier(effect);
            }
        }
        private void UseLevelDesigner()
        {
            if (!SpendEnergyForAbilityUse())
                return;

            

            var walls = FindObjectsOfType(typeof(LevelDesignerWallPower)) as LevelDesignerWallPower[];

            foreach (var wall in walls)
            {
                if (wall.team == charStats.team)
                {
                    wall.Activate();
                }
            }
        }

        private void UseProjectManager()
        {
            if (!SpendEnergyForAbilityUse())
                return;

            if (effect == null)
                return;
            

            var team = charStats.team;

            foreach (var player in GameManager.instance.characterInstances)
            {
                var teamMateCS = player.GetComponent<CharacterStats>();
                if (teamMateCS.team == team)
                    teamMateCS.ApplyModifier(effect);

            }
        }
    }

}
