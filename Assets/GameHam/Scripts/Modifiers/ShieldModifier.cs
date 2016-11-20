using UnityEngine;
using System.Collections;

namespace UU.GameHam
{

    [CreateAssetMenu(menuName = "Stats Modifier/Shield Modifier")]
    public class ShieldModifier : CharacterStatsModifier
    {
        public int shieldAmount;

        public override void OnActivate(GameObject go)
        {
            base.OnActivate(go);
            Debug.Log("Activating Shield Modifier");

            // activate the shield
            var cs = go.GetComponent<CharacterStats>();
            cs.ActivateShield(shieldAmount);
        }
    }

}