using UnityEngine;

public class EnemyGangs : MonoBehaviour
{
    private int _currentGangInd = 0;

    public Transform GetCurrentGang()
    {
        if (_currentGangInd != transform.childCount)
        {
            Transform currentGang = transform.GetChild(_currentGangInd);
            return currentGang;
        }
        else
        {
            return null;
        }
    }

    public Transform GetNextGang()
    {
        _currentGangInd++;
        return GetCurrentGang();
    }
}
