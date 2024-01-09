using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerDeathHandler : MonoBehaviour
{
    UnityEvent OnDeath;
    PlayerAnimManager anim;
    PlayerDataManager playerDataManager;
    private void Start()
    {
        anim = GetComponent<PlayerAnimManager>();
        playerDataManager = GetComponent<PlayerDataManager>();
    }

    public void ReceiveDamage(float damage)
    {
        playerDataManager.health -= damage;
        if (playerDataManager.health <= 0)
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
