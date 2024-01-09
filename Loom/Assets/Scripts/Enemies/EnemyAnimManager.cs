using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAnimManager : MonoBehaviour
{
    Animator animator;
    string currentAnim;

    [Header("Animations")]
    public string IDLE;
    public string ATTACK;
    public string HIT;
    public string DEATH;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    public void SwitchAnim(string anim)
    {
        if (currentAnim == anim) { return; }
        currentAnim = anim;
        animator.Play(anim);
    }
}
