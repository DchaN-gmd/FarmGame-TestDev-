using UnityEngine;

[RequireComponent(typeof (Rigidbody))]
public class Character : MonoBehaviour
{
    [SerializeField] private FloatingJoystick _joystick;
    [SerializeField] private float _moveSpeed;
    [SerializeField] private GameObject _scythe;

    private Rigidbody _rigidbody;
    private Animator _animator;

    private void Start()
    {
        _animator = GetComponent<Animator>();
        _rigidbody = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        Move();
        ChangeAnimationState();
    }

    private void Move()
    {
        _rigidbody.velocity = new Vector3(_joystick.Horizontal * _moveSpeed, _rigidbody.velocity.y, _joystick.Vertical * _moveSpeed);

        if (_joystick.Horizontal != 0 || _joystick.Vertical != 0)
        {
            transform.rotation = Quaternion.LookRotation(_rigidbody.velocity);
        }
    }

    private void ChangeAnimationState()
    {
        if (_joystick.Horizontal != 0 || _joystick.Vertical != 0)
        {
            _animator.SetBool("isMoving", true);
        }
        else
        {
            _animator.SetBool("isMoving", false);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        print(other.gameObject.name);
        if (other.gameObject.TryGetComponent(out Wheat wheat))
        {
            _animator.SetBool("isHarvesting", true);
            _scythe.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent(out Wheat wheat))
        {
            _animator.SetBool("isHarvesting", false);
            _scythe.SetActive(false);
        }
    }
}
