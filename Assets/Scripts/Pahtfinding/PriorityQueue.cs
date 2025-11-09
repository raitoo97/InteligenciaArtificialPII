using System.Collections.Generic;
using UnityEngine;
public static class PriorityQueue <T>
{
    private static Dictionary<T,float> _allElements = new Dictionary<T,float>();
    public static int Count { get => _allElements.Count; }
    public static void Enqueue(T key,float value)
    {
        if(!_allElements.ContainsKey(key))
            _allElements.Add(key, value);
        else
            _allElements[key] = value;
    }
    public static T Dequeue()
    {
        T minElement = default;
        float minValue = Mathf.Infinity;
        foreach (var element in _allElements) 
        {
            if(element.Value < minValue)
            {
                minValue = element.Value;
                minElement = element.Key;
            }
        }
        _allElements.Remove(minElement);
        return minElement;
    }
}
