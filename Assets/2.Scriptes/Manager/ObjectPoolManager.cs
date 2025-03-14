using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class ObjectPooling
{
    private readonly string path;
    protected Transform root;

    public ObjectPooling(Transform _parent, string _path = null)
    {
        root = _parent;
        path = _path;
    }
    public virtual Transform Pooling(string _objName, string _subPath)
    {
        if(path == null)
        {
            Debug.LogError("Not exist Path!!");
            return null;
        }

        Transform ret = null;
        // start 후에 이 부분만 partial하여 resmanager를 만들어서 관리
        string combinePath = string.Format("{0}/{1}/{2}", path, _subPath, _objName);
        ret = Create(combinePath);

        return ret;
    }
    public virtual GameObject Pooling(GameObject _go)
    {
        GameObject ret = null;
        ret = Create(_go);
        return ret;
    }
    public virtual Transform Pooling(string _objName)
    {
        if (path == null)
        {
            Debug.LogError("Not exist Path!!");
            return null;
        }

        Transform ret = null;
        // start 후에 이 부분만 partial하여 resmanager를 만들어서 관리
        string combinePath = string.Format("{0}/{1}", path, _objName);
        ret = Create(combinePath);

        return ret;
    }
    private void SetParent(Transform _child)
    {
        if (root.childCount == 0)
        {
            _child.parent = root;
            _child.gameObject.SetActive(false);
        }
        else
        {
            _child = root.GetChild(0);
        }
    }
    private GameObject Create(GameObject _go)
    {
        Transform ret = null;
        ret = GameUtil.InstantiateGameObject<Transform>(_go);
        SetParent(ret);
        ret.gameObject.SetActive(true);

        return ret.gameObject;
    }
    private Transform Create(string _combinePath)
    {
        Transform ret = null;
        ret = GameUtil.InstantiateResource<Transform>(_combinePath);
        SetParent(ret);

        ret.gameObject.SetActive(true);

        return ret;
    }
    public virtual void Retrieve(Transform _trf)
    {
        _trf.parent = root;
        _trf.localPosition = Vector3.zero;
        _trf.localRotation = Quaternion.identity;
        _trf.localScale = Vector3.one;
        _trf.gameObject.SetActive(false);
    }
}
public class ObjectPoolManager : Singleton<ObjectPoolManager>
{
    // object pool 상위 부모 자식들의 갯수에 따라 생성 가능과 회수를 결정한다
    private Dictionary<eObjectType, ObjectPooling> parentPoolingMap = new Dictionary<eObjectType, ObjectPooling>();

    public Transform Pooling(eObjectType _type, string _resName, string _subPath)
    {
        Transform ret = null;
        ret = GetPooling(_type).Pooling(_resName, _subPath);

        return ret;
    }

    public Transform Pooling(eObjectType _type, string _resName)
    {
        Transform ret = null;
        ret = GetPooling(_type).Pooling(_resName);

        return ret;
    }
    public T Pooling<T>(eObjectType _type, string _resName)
    {
        Transform ret = null;
        ret = GetPooling(_type).Pooling(_resName);

        return ret.GetComponent<T>();
    }
    public GameObject Pooling(eObjectType _type, GameObject _go)
    {
        GameObject ret = null;
        ret = GetPooling(_type).Pooling(_go);

        return ret;
    }
    private ObjectPooling GetPooling(eObjectType _type)
    {
        ObjectPooling ret = null;

        if (parentPoolingMap.ContainsKey(_type))
        {
            ret = parentPoolingMap[_type];
        }
        else
        {
            var name = Enum.GetName(typeof(eObjectType), _type);

            GameObject go = new GameObject(name);
            go.transform.parent = transform;
            go.transform.localPosition = Vector3.zero;

            ObjectPooling objectPooling = new ObjectPooling(go.transform, null);

            parentPoolingMap.Add(_type, objectPooling);

            ret = objectPooling;
        }

        return ret;
    }
    /// <summary>
    /// 회수 함수 
    /// </summary>
    public void Retrieve(eObjectType _type, Transform _trf)
    {
        if (parentPoolingMap.ContainsKey(_type))
        {
            parentPoolingMap[_type].Retrieve(_trf);
        }
        else
        {
            var name = Enum.GetName(typeof(eObjectType), _type);

            GameObject go = new GameObject(name);
            go.transform.parent = transform;
            go.transform.localPosition = Vector3.zero;

            ObjectPooling objectPooling = new ObjectPooling(go.transform, null);

            parentPoolingMap.Add(_type, objectPooling);
            parentPoolingMap[_type].Retrieve(_trf);

        }
    }
}
