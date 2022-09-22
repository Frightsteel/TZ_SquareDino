using UnityEngine;
using UnityEngine.AI;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private NavMeshAgent _agent;
    
    [SerializeField] private WayPoints _wayPoints;
    [SerializeField] private EnemyGangs _enemyGangs;
    [SerializeField] private Spawner _spawner;

    [SerializeField] private Transform _shootPoint;

    private Camera _mainCamera;

    private Transform _currentWayPoint;
    private int _currentEnemyCount; //

    private void Start()
    {
        _mainCamera = Camera.main;

        _currentWayPoint = _wayPoints.GetNextWayPoint();
        transform.position = _currentWayPoint.position;
    }

    private void Update()
    {
        Move();
        Shoot();
    }

    private void Move()
    {
        if (Vector3.Distance(transform.position, _currentWayPoint.position) < 1f && _enemyGangs.GetCurrentGangEnemyCount() == 0)
        {
            _currentEnemyCount = _enemyGangs.GetNextGangEnemyCount();//

            _currentWayPoint = _wayPoints.GetNextWayPoint();
            _agent.SetDestination(_currentWayPoint.position);
        }
    }

    private void Shoot()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (Physics.Raycast(_mainCamera.ScreenPointToRay(Input.mousePosition), out RaycastHit hit))
            {
                Vector3 aimDir = (hit.point - _shootPoint.position).normalized;
                Projectile currentProjectile = _spawner.ProjectilePool.GetFreeElement();
                currentProjectile.transform.SetPositionAndRotation(_shootPoint.position, Quaternion.LookRotation(aimDir, Vector3.up));
            }
        }
    }
}
