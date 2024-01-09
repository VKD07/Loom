using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimManager : MonoBehaviour
{
    [Header("Animations")]
    public string IDLE = "Unamed-Idle";
    public string MOVE = "RunForward";
    public string HIT = "Hit";
    public string DEATH = "Death";
    public string SWORDATTACK1 = "SwordAttack1";


    Animator animator;
    string currentAnim;

    void Start()
    {
        animator = GetComponent<Animator>();
        SwitchAnim(IDLE);
    }

    public void SwitchAnim(string anim)
    {
        if(currentAnim == anim) { return; }
        currentAnim = anim;
        animator.Play(anim);
    }
}
