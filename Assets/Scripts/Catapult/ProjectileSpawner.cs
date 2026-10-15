using UnityEngine;

public class ProjectileSpawner : MonoBehaviour
{
    [SerializeField] private Rigidbody _projectilePrefab;
    [SerializeField] private Transform _spawnPoint;

    private Rigidbody _projectile;

    public Rigidbody Projectile => _projectile;
    public bool HasProjectile => _projectile != null;

    public void Spawn()
    {
        if (_projectile != null)
            return;

        _projectile = Instantiate(
            _projectilePrefab,
            _spawnPoint.position,
            _spawnPoint.rotation);

        _projectile.isKinematic = true;
        _projectile.useGravity = false;
    }

    public void Release(Vector3 velocity)
    {
        if (_projectile == null)
            return;

        _projectile.isKinematic = false;
        _projectile.useGravity = true;
        _projectile.velocity = velocity;

        _projectile = null;
    }

    private void FixedUpdate()
    {
        if (_projectile == null)
            return;

        _projectile.position = _spawnPoint.position;
        _projectile.rotation = _spawnPoint.rotation;
    }
}