using System.Collections.Generic;
using UnityEngine;
[DefaultExecutionOrder(-90)]
public class EnemyManager : MonoBehaviour
{
    public static EnemyManager instance;
    private List<Enemy> _enemies = new List<Enemy>();
    private void Awake()
    {
        if(instance == null)
            instance = this;
        else
            Destroy(this.gameObject);
    }
    public void RegisterEnemy(Enemy enemy)
    {
        if(!_enemies.Contains(enemy))
            _enemies.Add(enemy);
    }
    public void UnregisterEnemy(Enemy enemy)
    {
        if (_enemies.Contains(enemy))
            _enemies.Remove(enemy);
    }
    public void AlertAllEnemies(Enemy caller, Vector3 lastKnownPosition)
    {
        foreach (var enemy in _enemies)
        {
            enemy.UpdateLastKnownPosition(lastKnownPosition);
            if (enemy.GetStateEnemy == FSM.State.chase) continue;
            bool canSeePlayer = LineOfSight.IsOnSight(enemy.transform.position, lastKnownPosition);
            if (canSeePlayer)
            {
                enemy.OnAlerted(lastKnownPosition, true);
            }
            else
            {
                enemy.OnAlerted(lastKnownPosition, false);
            }
        }
    }
}