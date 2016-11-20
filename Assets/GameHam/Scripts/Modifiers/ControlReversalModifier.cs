using UnityEngine;
using System.Collections;

namespace UU.GameHam
{

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

}