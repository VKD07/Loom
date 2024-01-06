using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(Enemy))]
public class EnemyEditor : Editor
{
    Enemy enemy;
    public override void OnInspectorGUI()
    {
        this.enemy = (Enemy)target;
        base.OnInspectorGUI();
        GUILayout.Space(20);
        KillEnemy();
    }

    void KillEnemy()
    {
        if(GUILayout.Button("Kill Enemy"))
        {
            enemy.health = 0;
        }
    }
}
