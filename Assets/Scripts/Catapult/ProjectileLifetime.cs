using UnityEngine;

public class ProjectileLifetime : MonoBehaviour
{
    [SerializeField] private float _lifetime = 10f;

    private void Start()
    {
        Destroy(gameObject, _lifetime);
    }
}