using UnityEngine;
using UnityEditor;
using System.Collections;

namespace UU.GameHam
{

    [CustomEditor(typeof(CharacterStatsModifier))]
    public class CharacterStatsModifierEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            EditorUtility.SetDirty(target);
        }
    }

}