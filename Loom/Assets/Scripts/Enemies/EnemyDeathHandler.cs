using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EnemyDeathHandler : MonoBehaviour
{
    UnityEvent OnDeath;
    EnemyAnimManager anim;
    MonsterDataManager monsterData;
    private void Start()
    {
        anim = GetComponent<EnemyAnimManager>();
        monsterData = GetComponent<MonsterDataManager>();
    }

    public void ReceiveDamage(float damage)
    {
        monsterData.health -= damage;
        if (monsterData.health <= 0)
        {
            Death();
        }
        else
        {
            anim.SwitchAnim(anim.HIT);
        }
    }

    void Death()
    {
        anim.SwitchAnim(anim.DEATH);
        //OnDeath.Invoke();
    }
}
