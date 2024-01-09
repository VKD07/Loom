using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(BattleManager))]
public class MonsterBattleManagerEditor : Editor
{
    BattleManager battleManager;
    public override void OnInspectorGUI()
    {
        this.battleManager = (BattleManager)target;
        base.OnInspectorGUI();
        GUILayout.Space(20);
        KillEnemy();
    }

    void KillEnemy()
    {
        if (GUILayout.Button("Kill Enemy"))
        {
            battleManager.currentMonsterData.health = 0;
        }
    }
}
