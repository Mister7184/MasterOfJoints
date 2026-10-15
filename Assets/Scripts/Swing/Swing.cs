using UnityEngine;

[RequireComponent(typeof(Rigidbody), typeof(HingeJoint), typeof(SwingInput))]
public class Swing : MonoBehaviour
{
    [SerializeField] private float _pushImpulse = 5f;

    private SwingInput _input;
    private Rigidbody _rigidbody;
    private HingeJoint _hingeJoint;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _hingeJoint = GetComponent<HingeJoint>();
        _input = GetComponent<SwingInput>();
    }

    private void OnEnable()
    {
        if (_input != null)
            _input.SwingPressed += Push;
    }

    private void OnDisable()
    {
        if (_input != null)
            _input.SwingPressed -= Push;
    }

    private void Push()
    {
        Vector3 axis = transform.TransformDirection(_hingeJoint.axis);

        _rigidbody.AddTorque(
            axis * _pushImpulse,
            ForceMode.Impulse);
    }
}