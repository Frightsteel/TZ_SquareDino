using UnityEngine;
using UnityEngine.AI;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private NavMeshAgent _agent;
    [SerializeField] private Animator _animator;

    [SerializeField] private WayPoints _wayPoints;
    [SerializeField] private EnemyGangs _enemyGangs;
    [SerializeField] private Spawner _spawner;

    [SerializeField] private Transform _shootPoint;

    [SerializeField] private GameManager _gameManager;

    // animations IDs
    private int animIDMove;

    private Camera _mainCamera;

    private Transform _currentWayPoint;
    private Transform _currentEnemyGang;

    private void Start()
    {
        _mainCamera = Camera.main;

        _currentEnemyGang = _enemyGangs.GetCurrentGang();
        _currentWayPoint = _wayPoints.GetCurrentWayPoint();
        transform.position = _currentWayPoint.position;

        AssignAnimationIDs();
    }

    private void AssignAnimationIDs()
    {
        animIDMove = Animator.StringToHash("isMoving");
    }

    private void Update()
    {
        if (_gameManager.IsGameStarted)
        {
            Move();
            Shoot();
        }
    }

    private void Move()
    {
        if (Vector3.Distance(transform.position, _currentWayPoint.position) < 1f)
        {
            _animator.SetBool(animIDMove, false);

            if (_currentEnemyGang.childCount == 0)
            {
                _currentEnemyGang = _enemyGangs.GetNextGang();
                _currentWayPoint = _wayPoints.GetNextWayPoint();

                if (_currentEnemyGang != null || _currentWayPoint != null)
                {
                    _animator.SetBool(animIDMove, true);
                    _agent.SetDestination(_currentWayPoint.position);
                }
                else
                {
                    _gameManager.RestartScene();
                }
            }
        }
    }

    private void Shoot()
    {
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
        {
            if (Physics.Raycast(_mainCamera.ScreenPointToRay(Input.GetTouch(0).position), out RaycastHit hit))
            {
                Vector3 aimDir = (hit.point - _shootPoint.position).normalized;
                Projectile currentProjectile = _spawner.ProjectilePool.GetFreeElement();
                currentProjectile.transform.SetPositionAndRotation(_shootPoint.position, Quaternion.LookRotation(aimDir, Vector3.up));
            }
        }
    }
}
