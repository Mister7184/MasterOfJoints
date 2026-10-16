using UnityEngine;

[RequireComponent(typeof(HingeJoint))]
public class CatapultArm : MonoBehaviour
{
    [SerializeField] private InputReader _input;
    [SerializeField] private ProjectileSpawner _projectileSpawner;
    [SerializeField] private float _angleTolerance = 5f;

    private HingeJoint _hingeJoint;
    private Rigidbody _projectile;
    private bool _isLaunching;
    private bool _isReloading;

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

    private void Awake()
    {
        _hingeJoint = GetComponent<HingeJoint>();

        _projectile = _projectileSpawner.Spawn();
    }

    private void FixedUpdate()
    {
        if (!_isLaunching && !_isReloading)
            return;

        float targetAngle = _hingeJoint.spring.targetPosition;

        float angleDifference = Mathf.Abs(
            Mathf.DeltaAngle(_hingeJoint.angle, targetAngle));

        if (angleDifference > _angleTolerance)
            return;

        if (_isLaunching)
            _isLaunching = false;

        if (_isReloading)
        {
            _isReloading = false;
            _projectile = _projectileSpawner.Spawn();
        }
    }

    private void Launch()
    {
        if (_projectile == null || _isLaunching || _isReloading)
            return;

        _projectile = null;
        _isLaunching = true;

        SetTargetAngle(_hingeJoint.limits.min);
    }

    private void Reload()
    {
        if (_projectile != null || _isLaunching || _isReloading)
            return;

        _isReloading = true;

        SetTargetAngle(_hingeJoint.limits.max);
    }

    private void SetTargetAngle(float targetAngle)
    {
        JointSpring spring = _hingeJoint.spring;
        spring.targetPosition = targetAngle;
        _hingeJoint.spring = spring;
    }
}