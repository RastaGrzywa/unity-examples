using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    private Controls _controls;
    private Vector2 _moveAxis;

    private Rigidbody _meshRigidBody;

    private void Awake()
    {
        _meshRigidBody = GetComponentInChildren<Rigidbody>();
        _controls = new Controls();
        _controls.Player.Jump.performed += HandleJump;
        _controls.Player.Movement.performed += HandleMove;
        _controls.Player.Movement.canceled += HandleMove;
    }

    private void OnEnable()
    {
        _controls.Player.Jump.Enable();
        _controls.Player.Movement.Enable();
    }

    private void OnDisable()
    {
        _controls.Player.Jump.Disable();
        _controls.Player.Movement.Disable();
    }

    private void OnDestroy()
    {
        _controls.Player.Jump.performed -= HandleJump;
        _controls.Player.Movement.performed -= HandleMove;
    }

    private void Update()
    {
        transform.position += new Vector3(_moveAxis.x * Time.deltaTime * 5f, 0, _moveAxis.y * Time.deltaTime * 5f);
    }

    private void HandleJump(InputAction.CallbackContext obj)
    {
        _meshRigidBody.AddForce(Vector3.up * 10, ForceMode.Impulse);
        _meshRigidBody.AddTorque(transform.forward * 5);
    }

    private void HandleMove(InputAction.CallbackContext obj)
    {
        _moveAxis = obj.ReadValue<Vector2>();
    }
}
