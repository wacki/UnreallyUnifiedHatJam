using UnityEngine;
using System.Collections;

namespace UU.GameHam
{
    public class SpawnPoint : MonoBehaviour
    {
        public Teams team;

        void OnDrawGizmos()
        {
            if (team == Teams.Red)
                Gizmos.color = Color.red;
            else
                Gizmos.color = Color.blue;

            Gizmos.DrawWireSphere(transform.position, 0.5f);
        }
    }
}