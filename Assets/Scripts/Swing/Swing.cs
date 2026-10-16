using UnityEngine;

[RequireComponent(typeof(HingeJoint))]
public class Swing : MonoBehaviour
{
    [SerializeField] private float _pushImpulse = 5f;
    [SerializeField] private InputReader _input;

    private Rigidbody _rigidbody;
    private HingeJoint _hingeJoint;

    private void OnEnable()
    {
        _input.SwingPressed += Push;
    }

    private void OnDisable()
    {
        _input.SwingPressed -= Push;
    }

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _hingeJoint = GetComponent<HingeJoint>();
    }

    private void Push()
    {
        Vector3 axis = transform.TransformDirection(_hingeJoint.axis);

        _rigidbody.AddTorque(
            axis * _pushImpulse,
            ForceMode.Impulse);
    }
}