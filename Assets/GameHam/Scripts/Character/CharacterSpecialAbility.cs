﻿using UnityEngine;
using System.Collections;

namespace UU.GameHam
{

    public class CharacterSpecialAbility : MonoBehaviour
    {
        public CharacterStatsModifier effect;
        public CharacterType characterType;

        private float cooldownTimer;
        private CharacterStats charStats;

        void Awake()
        {
            charStats = GetComponent<CharacterStats>();
        }

        public virtual void Use()
        {
            switch(characterType)
            {
                case CharacterType.Artist: UseArtist();  return;
                case CharacterType.Programmer: UseProgrammer();  return;
                case CharacterType.LevelDesigner: UseLevelDesigner();  return;
                case CharacterType.ProjectManager: UseProjectManager();  return;
            }

        }

        private bool SpendEnergyForAbilityUse()
        {
            if (charStats.currentEnergy < charStats.maxEnergy)
                return false;

            charStats.SpendEnergy();
            return true;

        }

        private void UseArtist()
        {
            if (!SpendEnergyForAbilityUse())
                return;

            if (effect == null)
                return;

            Debug.Log("ARTIST USED THEIR SPECIAL");

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


            Debug.Log("PROGRAMMER USED THEIR SPECIAL");
        }
        private void UseLevelDesigner()
        {
            if (!SpendEnergyForAbilityUse())
                return;
            Debug.Log("LEVEL DESIGNER USED THEIR SPECIAL");
        }

        private void UseProjectManager()
        {
            if (!SpendEnergyForAbilityUse())
                return;

            if (effect == null)
                return;

            var team = charStats.team;

            foreach(var player in GameManager.instance.characterInstances)
            {
                var teamMateCS = player.GetComponent<CharacterStats>();
                if (teamMateCS.team == team)
                    teamMateCS.ApplyModifier(effect);

            }
        }
    }

}
