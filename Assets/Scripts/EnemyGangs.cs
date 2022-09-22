using UnityEngine;

public class EnemyGangs : MonoBehaviour
{
    private int _currentEnemyGangInd = 0;

    public int GetNextGangEnemyCount()
    {
        _currentEnemyGangInd++;
        int enemyCount = transform.GetChild(_currentEnemyGangInd).childCount;
        return enemyCount;
    }

    public int GetCurrentGangEnemyCount()
    {
        int enemyCount = transform.GetChild(_currentEnemyGangInd).childCount;
        return enemyCount;
    }
}
