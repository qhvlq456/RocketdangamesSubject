using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static BaseObject;

public abstract class BaseObject : MonoBehaviour
{
    public enum AnimState { Idle, Attack, Dead }
    [SerializeField]
    private Animator animator;

    [SerializeField]
    protected GameUtil.Health health;
    public bool isDie;
    public bool isAttack;
    public virtual void Load() { }
    public virtual void MoveMent(Vector2 _destinaition) { }
    public virtual void TakeDamage(int _damage) { }
    public virtual void Dispose() { }
    public virtual void Anim(AnimState _animState)
    {
        // AnimatorStateInfo stateInfo = animator.GetCurrentAnimatorStateInfo(0); // 0은 기본 레이어의 인덱스

        // 현재 애니메이션의 이름을 가져오기
        // string currentAnimationName = stateInfo.IsName("isIdle") ? "isIdle" : stateInfo.IsName("isAttacking") ? "isAttacking" : stateInfo.IsName("isDead") ? "isDead" : "Unknown";

        // Debug.Log("Current Animation: " + currentAnimationName);

        string value = "";

        switch (_animState)
        {
            case AnimState.Idle:
                value = "isIdle";
                // animator.SetBool("IsIdle", true);
                break;
            case AnimState.Attack:
                value = "isAttack";
                // animator.SetBool("isAttacking", true);
                break;
            case AnimState.Dead:
                value = "isDead";
                break;
        }

        animator.SetTrigger(value);
    }
}
