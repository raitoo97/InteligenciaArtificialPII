using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChaseState : IState
{
    public void OnExit()
    {
        Debug.Log("Enter Chase");
    }
    public void Onstart()
    {
        throw new System.NotImplementedException();
    }
    public void OnUpdate()
    {
        throw new System.NotImplementedException();
    }

}
