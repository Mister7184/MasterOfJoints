using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(HingeJoint))]

public class CatapultController : MonoBehaviour
{
    [SerializeField] private KeyCode _launchKey = KeyCode.F;
    [SerializeField] private KeyCode _reloadKey = KeyCode.R;
    [SerializeField] private float _launchAngle = -55f;
    [SerializeField] private Rigidbody _projectilePrefab;
    [SerializeField] private Transform _projectileSpawnPoint;

    private HingeJoint _hinge;
    private Rigidbody _arm;
    private Rigidbody _projectile;
    private bool _isReturning;

    private void Awake()
    {
        _arm = GetComponent<Rigidbody>();
        _hinge = GetComponent<HingeJoint>();

        _hinge.useMotor = false;
        _hinge.useSpring = true;
    }

    private void Start()
    {
        SpawnProjectile();
    }

    private void Update()
    {
        if (Input.GetKeyDown(_launchKey) && _projectile != null && !_isReturning)
        {
            _hinge.useSpring = false;
            _hinge.useMotor = true;
        }

        if (Input.GetKeyDown(_reloadKey) && _projectile == null && !_isReturning)
        {
            _isReturning = true;

            _hinge.useMotor = false;
            _hinge.useSpring = true;
        }
    }

    private void FixedUpdate()
    {
        if (_projectile != null && _projectile.isKinematic)
        {
            _projectile.position = _projectileSpawnPoint.position;
            _projectile.rotation = _projectileSpawnPoint.rotation;
        }

        if (_hinge.useMotor && _projectile != null &&
            Mathf.Abs(Mathf.DeltaAngle(_hinge.angle, _launchAngle)) < 5f)
        {
            ReleaseProjectile();
            _hinge.useMotor = false;
        }

        if (_isReturning && Mathf.Abs(Mathf.DeltaAngle(
                _hinge.angle, _hinge.spring.targetPosition)) < 3f)
        {
            _isReturning = false;
            SpawnProjectile();
        }
    }

    private void ReleaseProjectile()
    {
        _projectile.velocity = _arm.GetPointVelocity(_projectile.position);
        _projectile.isKinematic = false;
        _projectile.useGravity = true;

        _projectile = null;
    }

    private void SpawnProjectile()
    {
        _projectile = Instantiate(
            _projectilePrefab,
            _projectileSpawnPoint.position,
            _projectileSpawnPoint.rotation
        );

        _projectile.isKinematic = true;
        _projectile.useGravity = false;
    }
}