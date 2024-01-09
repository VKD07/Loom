using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] float attackMoveSpeed = 5f;
    [SerializeField] float stoppingDistance = 1f;

    [Header("Physics Check")]
    [SerializeField] Transform attackPoint;
    [SerializeField] float sphereRad = 1f;
    [SerializeField] LayerMask opponentLayer;

    Vector3 initPos;
    bool attack;
    bool returnToPos;
    bool initPosRecorded;
    float addedDamage;

    PlayerAnimManager anim;
    PlayerDataManager playerData;

    private void Start()
    {
        anim = GetComponent<PlayerAnimManager>();
        playerData = GetComponent<PlayerDataManager>();
    }

    private void Update()
    {
        AttackLerp();
    }

    private void AttackLerp()
    {
        if (attack)
        {
            RecordInitPos();
            Transform target = BattleManager.instance.currentMonsterData.transform;
            if (Vector3.Distance(transform.position, target.position) >= stoppingDistance)
            {
                transform.position = Vector3.Lerp(transform.position, target.position, attackMoveSpeed * Time.deltaTime);
            }
            else
            {
                anim.SwitchAnim(anim.SWORDATTACK1);
                attack = false;
                initPosRecorded = false;
            }
        }
        else
        {
            ReturnToInitPos();
        }
    }

    void RecordInitPos()
    {
        if (!initPosRecorded)
        {
            initPosRecorded = true;
            initPos = transform.position;
        }
    }
    void ReturnToInitPos()
    {
        if (Vector3.Distance(initPos, transform.position) <= .1f)
        {
            returnToPos = false;
        }
        else if (Vector3.Distance(initPos, transform.position) > 0 && returnToPos)
        {
            anim.SwitchAnim(anim.IDLE);
            transform.position = Vector3.Lerp(transform.position, initPos, attackMoveSpeed * Time.deltaTime);
        }
    }

    public void AddDamage(float damage)
    {
        addedDamage = damage;
        playerData.totalDamage += addedDamage;
    }

    #region Animation event
    public void DealDamageToOpponent()
    {
        Collider[] opponents = Physics.OverlapSphere(attackPoint.position, sphereRad, opponentLayer);

        if (opponents.Length > 0)
        {
            PlayerDeathHandler playerDeathHandler = opponents[0].GetComponent<PlayerDeathHandler>();
            EnemyDeathHandler enemyDeathHandler = opponents[0].GetComponent<EnemyDeathHandler>();
            if (playerDeathHandler != null)
            {
                playerDeathHandler.ReceiveDamage(playerData.totalDamage);
                playerData.totalDamage -= addedDamage;
            }
            else if (enemyDeathHandler != null)
            {
                print("Opponent detected");
                enemyDeathHandler.ReceiveDamage(playerData.totalDamage);
                playerData.totalDamage -= addedDamage;
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackPoint.position, sphereRad);
    }

    public void TriggerReturn()
    {
        if (!returnToPos)
        {
            returnToPos = true;
        }
    }
    public bool HasReturned => returnToPos;

    public void JumpAttack()
    {
        attack = true;
    }
    #endregion
}
