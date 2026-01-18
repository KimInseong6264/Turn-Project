using System;
using System.Collections.Generic;
using Mono.Cecil;
using UnityEngine;

// 유니티 오브젝트를 위한 오브젝트풀
public class UnityObjectPull<T> where T : Component
{
    private readonly Queue<T> _pull;
    private readonly T _prefab;
    private readonly Transform _parent;

    public UnityObjectPull(T prefab, int count,  Transform parent)
    {
        _pull = new Queue<T>();
        _prefab = prefab;
        _parent = parent;
        
        for(int i = 0; i < count; i++)
            Create();
    }

    // 오브젝트 부족시, 생성
    private T Create()
    {
        if (_prefab == null)
        {
            Debug.LogError("풀링 프리팹이 없습니다.");
            return null;
        }
        
        var obj = GameObject.Instantiate(_prefab, _parent);
        obj.gameObject.SetActive(false);
        _pull.Enqueue(obj);
        return obj;
    }

    // 오브젝트 가져오기
    public T GetPull()
    {
        var obj = _pull.Count > 0 ? _pull.Dequeue() : Create();
        obj.gameObject.SetActive(true);
        return obj;
    }

    // 오브젝트 parent 하위로 활성화
    public T GetPull(Transform parent)
    {
        T obj = GetPull();
        obj.transform.parent = parent;
        return obj;
    }

    // 오브젝트 비활성화
    public void Release(T obj)
    {
        if (obj.transform.parent != _parent)
            obj.transform.SetParent(_parent, false);
        
        obj.gameObject.SetActive(false);
        _pull.Enqueue(obj);
    }
    
}

// 커맨드 패턴용 오브젝트풀
public class CommandObjectPull<T> where T : class, new()
{
    private readonly Stack<T> _pull;

    public T GetPull => _pull.Count > 0 ? _pull.Pop() : new T();
    public void Release(T obj) =>  _pull.Push(obj);
}