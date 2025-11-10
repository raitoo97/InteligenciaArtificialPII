using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
            if (enemy == caller)
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
