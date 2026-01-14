using System.Collections.Generic;
using UnityEngine;


public static class CoroutineManager
{
    private static readonly Dictionary<float, WaitForSeconds> _waitTime = new Dictionary<float, WaitForSeconds>();

    // 코루틴 WaitForSeconds값을 캡슐화해서 사용
    public static WaitForSeconds GetWaitTime(float time)
    {
        if (!_waitTime.TryGetValue(time, out var value))
        {
            _waitTime.Add(time, new WaitForSeconds(time));
        }
        
        return value;
    }
}