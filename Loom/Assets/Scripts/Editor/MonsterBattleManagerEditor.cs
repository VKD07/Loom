using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(MonsterBattleManager))]
public class MonsterBattleManagerEditor : Editor
{
    MonsterBattleManager battleManager;
    public override void OnInspectorGUI()
    {
        this.battleManager = (MonsterBattleManager)target;
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
