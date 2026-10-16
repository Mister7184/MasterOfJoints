using UnityEngine;

public class ProjectileSpawner : MonoBehaviour
{
    [SerializeField] private Rigidbody _projectilePrefab;
    [SerializeField] private Transform _spawnPoint;

    public Rigidbody Spawn()
    {
        return Instantiate(
            _projectilePrefab,
            _spawnPoint.position,
            _spawnPoint.rotation);
    }
}