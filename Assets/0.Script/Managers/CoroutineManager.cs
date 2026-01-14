using System.Collections.Generic;
using UnityEngine;


public class CoroutineManager : MonoBehaviour
{
    private Dictionary<int, WaitForSeconds> _waitTime;

    private void Awake()
    {
        _waitTime = new Dictionary<int, WaitForSeconds>();
    }

    // 코루틴 WaitForSeconds값을 캡슐화해서 사용
    public WaitForSeconds GetWaitTime(int time)
    {
        if (_waitTime.TryGetValue(time, out var value))
        {
            return value;
        }
        
        _waitTime.Add(time, new WaitForSeconds(time));
        return _waitTime[time];
    }
}