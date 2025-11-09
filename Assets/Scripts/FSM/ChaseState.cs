using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChaseState : IState
{
    public void Onstart()
    {
        Debug.Log("Enter Chase");
    }
    public void OnUpdate()
    {
        throw new System.NotImplementedException();
    }
    public void OnExit()
    {
        Debug.Log("Exit Chase");
    }

}
