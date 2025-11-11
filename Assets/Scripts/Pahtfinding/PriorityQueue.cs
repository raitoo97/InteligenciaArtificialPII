using System.Collections.Generic;
using UnityEngine;
public class PriorityQueue <T>
{
    private Dictionary<T,float> _allElements = new Dictionary<T,float>();
    public int Count { get => _allElements.Count; }
    public void Enqueue(T key,float value)
    {
        if(!_allElements.ContainsKey(key))
            _allElements.Add(key, value);
        else
            _allElements[key] = value;
    }
    public T Dequeue()
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