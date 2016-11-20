using UnityEngine;
using System.Collections;

namespace UU.GameHam
{
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

}