using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(HingeJoint))]
public class SwingController : MonoBehaviour
{
    [SerializeField] private KeyCode _swingKey = KeyCode.G;
    [SerializeField] private float _pushImpulse = 5f;

    private Rigidbody _rigidbody;
    private HingeJoint _hingeJoint;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _hingeJoint = GetComponent<HingeJoint>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(_swingKey))
        {
            Vector3 axis = transform.TransformDirection(_hingeJoint.axis);

            _rigidbody.AddTorque(axis * _pushImpulse, ForceMode.Impulse);
        }
    }
}