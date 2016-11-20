using UnityEngine;
using System.Collections;

namespace UU.GameHam
{
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
}