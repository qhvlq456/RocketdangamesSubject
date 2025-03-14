using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.RuleTile.TilingRuleOutput;

public static class GameUtil
{
    #region Resource Util
    public static T InstantiateGameObject<T>(GameObject _go)
    {
        T ret = UnityEngine.Object.Instantiate(_go).GetComponent<T>();
        return ret;
    }
    public static T InstantiateResource<T>(string _path) where T : UnityEngine.Object
    {
        GameObject resObj = Resources.Load<GameObject>(_path);

        if (resObj == null)
        {
            Debug.LogWarningFormat("Util GetResource resobj Warning \n resobj : {0}, path : {1}", resObj, _path);
            return null;
        }

        T copyObj = UnityEngine.Object.Instantiate(resObj).GetComponent<T>();

        return copyObj;
    }
    #endregion
    #region Attach Obj
    // �Ŀ� find �̸��� ã�� ���� ������ �� ��?
    public static T AttachObj<T>(string _name = null) where T : Component
    {
        T ret = null;

        string name = _name;

        if (string.IsNullOrEmpty(name))
        {
            name = typeof(T).Name;
        }

        GameObject _target = GameObject.Find(name);

        if (_target == null)
        {
            GameObject container = new GameObject(name);
            _target = container;
        }

        ret = _target.GetComponent<T>();

        if (ret == null)
        {
            ret = _target.AddComponent<T>();
        }

        return ret;
    }

    public static T AttachObj<T>(GameObject _go, string _name = null) where T : Component
    {
        string name = _name;

        if (string.IsNullOrEmpty(name))
        {
            name = typeof(T).Name;
        }

        _go = GameObject.Find(name);

        if (_go == null)
        {
            GameObject container = new GameObject(name);
            _go = container;
        }

        T component = _go.GetComponent<T>();

        if (component == null)
        {
            component = _go.AddComponent<T>();
        }

        return component;
    }

    #endregion Attach Obj

    #region SearchAttackRange
    public static GameObject CheckAttackRange(UnityEngine.Transform _trf, float _radius, float _range, Vector2 _rangeOffset)
    {
        //  origin: ���� �߽� ��ġ
        //  radius: ���� ������
        //  direction: ������ ����
        //  distance: ���̰� �̵��� �Ÿ�
        Vector2 origin = (Vector2)_trf.position + _rangeOffset;
        RaycastHit2D hit = Physics2D.CircleCast(origin, _radius, Vector2.right, _range);
        //RaycastHit2D hit = (Physics2D.Raycast(transform.position,Vector2.right,heroData.range));

        if (hit.collider != null)
        {
            Debug.LogError($"name : {hit.collider.name}");
            Debug.DrawLine(_trf.position, hit.point, Color.red);  // ������ ��θ� �ð�ȭ
        }

        Debug.DrawLine(origin, (Vector2)_trf.position + (Vector2.right * _range), Color.blue); // ������ ���� ������ �ð�ȭ
        GameUtil.DrawCircle(origin, _range); // �� �׸���

        return hit.collider.gameObject;
    }
    // ���� �ð�ȭ�ϴ� �Լ�
    public static void DrawCircle(Vector2 _center, float _radius)
    {
        int numSegments = 360; // ���� �׸� ���� ���׸�Ʈ ��
        float angleStep = 360f / numSegments;

        for (int i = 0; i < numSegments; i++)
        {
            // ���� ���
            float angle1 = Mathf.Deg2Rad * i * angleStep;
            float angle2 = Mathf.Deg2Rad * (i + 1) * angleStep;

            // �� �ѷ��� �� ���
            Vector2 point1 = _center + new Vector2(Mathf.Cos(angle1), Mathf.Sin(angle1)) * _radius;
            Vector2 point2 = _center + new Vector2(Mathf.Cos(angle2), Mathf.Sin(angle2)) * _radius;

            // ���� �׾� ���� �ð�ȭ
            Debug.DrawLine(point1, point2, Color.red);
        }
    }

    #endregion SearchAttackRange

    #region Health
    [System.Serializable]
    public class Health
    {
        public float currentHealth;
        public float maxHealth;

        public Health(float _max)
        {
            ResetHealth(_max);
        }

        public void ResetHealth(float _max)
        {
            maxHealth = _max;
            currentHealth = _max;
        }
        // ���� ü�� ���� (0~1)
        public float GetHealthRatio()
        {
            return currentHealth / maxHealth;
        }

        // ���� ü�� ����� (0~100%)
        public float GetHealthPercentage()
        {
            return GetHealthRatio() * 100f;
        }

        // �Ҹ�� ü�� ����� (0~100%)
        public float GetHealthLostPercentage()
        {
            return (1 - GetHealthRatio()) * 100f;
        }

        // ü�� ���� �޼���
        public void TakeDamage(float _damage)
        {
            currentHealth = Mathf.Max(currentHealth - _damage, 0);
        }

        public void Heal(float amount)
        {
            currentHealth = Mathf.Min(currentHealth + amount, maxHealth);
        }
    }

    #endregion
}
