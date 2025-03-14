using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public BaseObject mObject;

    public sWeaponData data;
    public bool isAttack;

    [SerializeField]
    protected float delayTimer;

    [SerializeField]
    protected Vector2 rangeOffset;

    [SerializeField]
    protected CircleCollider2D mCollider;
    [SerializeField]
    private List<string> exceptionTagList = new List<string>();
    public virtual void Load() { }

    private void OnTriggerStay2D (Collider2D _collision)
    {
        if (_collision != null)
        {
            BaseObject baseObj = _collision.gameObject.GetComponent<BaseObject>();

            if (baseObj != null 
                && !exceptionTagList.Contains(_collision.tag) 
                && !baseObj.isDie 
                && isAttack)
            {
                Attack(baseObj);
                StartCoroutine(AttackDelay());
            }
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        isAttack = false;
    }
    IEnumerator AttackDelay()
    {
        isAttack = false; // 공격 중지
        yield return new WaitForSeconds(data.delay); // 공격 딜레이 적용
        isAttack = true; // 다시 공격 가능
    }

    public virtual void Attack(BaseObject _baseObject) 
    { 

    }
}
