using UnityEngine;

[RequireComponent(typeof(Rigidbody), typeof(HingeJoint), typeof(CatapultInput))]
public class CatapultArm : MonoBehaviour
{
    [SerializeField] private ProjectileSpawner _projectileSpawner;
    [SerializeField] private float _launchAngle = -55f;
    [SerializeField] private float _launchAngleTolerance = 5f;
    [SerializeField] private float _returnAngleTolerance = 5f;
    [SerializeField] private float _returnAngularSpeedThreshold = 0.2f;

    private Rigidbody _rigidbody;
    private HingeJoint _hingeJoint;
    private CatapultInput _input;
    private bool _isReloading;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _hingeJoint = GetComponent<HingeJoint>();
        _input = GetComponent<CatapultInput>();

        _hingeJoint.useMotor = false;
        _hingeJoint.useSpring = true;
    }

    private void OnEnable()
    {
        _input.LaunchPressed += Launch;
        _input.ReloadPressed += Reload;
    }

    private void OnDisable()
    {
        _input.LaunchPressed -= Launch;
        _input.ReloadPressed -= Reload;
    }

    private void Start()
    {
        _projectileSpawner.Spawn();
    }

    private void FixedUpdate()
    {
        if (_hingeJoint.useMotor && _projectileSpawner.HasProjectile)
            CheckLaunchAngle();

        if (_isReloading)
            CheckReloadCompletion();
    }

    private void Launch()
    {
        if (!_projectileSpawner.HasProjectile || _isReloading)
            return;

        _hingeJoint.useSpring = false;
        _hingeJoint.useMotor = true;
    }

    private void Reload()
    {
        if (_projectileSpawner.HasProjectile || _isReloading)
            return;

        _isReloading = true;
        _hingeJoint.useMotor = false;
        _hingeJoint.useSpring = true;
    }

    private void CheckLaunchAngle()
    {
        float angleDifference = Mathf.Abs(
            Mathf.DeltaAngle(_hingeJoint.angle, _launchAngle));

        if (angleDifference >= _launchAngleTolerance)
            return;

        Vector3 velocity = _rigidbody.GetPointVelocity(
            _projectileSpawner.Projectile.position);

        _projectileSpawner.Release(velocity);
        _hingeJoint.useMotor = false;
    }

    private void CheckReloadCompletion()
    {
        float angleDifference = Mathf.Abs(
            Mathf.DeltaAngle(
                _hingeJoint.angle,
                _hingeJoint.spring.targetPosition));

        if (angleDifference >= _returnAngleTolerance ||
            _rigidbody.angularVelocity.magnitude >= _returnAngularSpeedThreshold)
            return;

        _isReloading = false;
        _projectileSpawner.Spawn();
    }
}