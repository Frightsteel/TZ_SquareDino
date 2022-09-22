using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] private Projectile _projectilePrefab;

    [SerializeField] private int _poolSize;
    [SerializeField] private bool _autoExpand;

    public PoolMono<Projectile> ProjectilePool { get; private set; }

    private void Start()
    {
        ProjectilePool = new PoolMono<Projectile>(_projectilePrefab, _poolSize, transform);
        ProjectilePool.AutoExpand = _autoExpand;
    }
}
