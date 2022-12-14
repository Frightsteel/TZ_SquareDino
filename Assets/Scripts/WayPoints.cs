using UnityEngine;

public class WayPoints : MonoBehaviour
{
    private int _currentWayPointInd = 0;

    public Transform GetCurrentWayPoint()
    {
        if (_currentWayPointInd != transform.childCount)
        {
            Transform currentPoint = transform.GetChild(_currentWayPointInd);
            return currentPoint;
        }
        else
        {
            return null;
        }
    }

    public Transform GetNextWayPoint()
    {
        _currentWayPointInd++;
        return GetCurrentWayPoint();
    }

    private void OnDrawGizmos()
    {
        foreach (Transform point in transform)
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawWireSphere(point.position, 1f);
        }

        Gizmos.color = Color.red;
        for (int i = 0; i < transform.childCount - 1; i++)
        {
            Gizmos.DrawLine(transform.GetChild(i).position, transform.GetChild(i + 1).position);
        }
    }
}
