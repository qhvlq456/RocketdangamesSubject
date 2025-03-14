using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DamageText : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI text;
    [SerializeField]
    private float speed;
    [SerializeField]
    private float time;

    public void StartDamage(string _value)
    {
        text.text = _value;
        StartCoroutine(CoDamage());
    }

    IEnumerator CoDamage()
    {
        float timer = 0;

        while(timer < time)
        {
            timer += Time.deltaTime;
            transform.localScale = Vector3.Lerp(transform.localScale, Vector3.zero, speed * Time.deltaTime);
            yield return null;
        }

        // 나중엔 재활용 할것임
        Destroy(gameObject);

    }
}
