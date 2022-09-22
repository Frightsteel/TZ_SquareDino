using UnityEngine;
using UnityEngine.AI;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private NavMeshAgent _agent;

    private SceneController scene = new SceneController();
    
    [SerializeField] private WayPoints _wayPoints;
    [SerializeField] private EnemyGangs _enemyGangs;
    [SerializeField] private Spawner _spawner;

    [SerializeField] private Transform _shootPoint;

    private Camera _mainCamera;

    private Transform _currentWayPoint;
    private Transform _currentEnemyGang;

    private void Start()
    {
        _mainCamera = Camera.main;

        _currentEnemyGang = _enemyGangs.GetCurrentGang();
        _currentWayPoint = _wayPoints.GetCurrentWayPoint();
        transform.position = _currentWayPoint.position;
    }

    private void Update()
    {
        Move();
        Shoot();
    }

    private void Move()
    {
        if (Vector3.Distance(transform.position, _currentWayPoint.position) < 1f && _currentEnemyGang.childCount == 0)
        {   
            _currentEnemyGang = _enemyGangs.GetNextGang();
            _currentWayPoint = _wayPoints.GetNextWayPoint();

            if (_currentEnemyGang != null || _currentWayPoint != null)
            {
                _agent.SetDestination(_currentWayPoint.position);
            }
            else
            {
                scene.RestartScene();
            }
            
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
