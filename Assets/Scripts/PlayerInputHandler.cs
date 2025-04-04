using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputHandler : MonoBehaviour
{
    [SerializeField] private InputActionReference moving;
    [SerializeField] private InputActionReference looking;
    [SerializeField] private float speed;
    [SerializeField] private float cameraSpeed;

    private Rigidbody _rigidbody;
    private CharacterController _characterController;


    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _characterController = GetComponent<CharacterController>();

        Cursor.lockState = CursorLockMode.Locked;
    }

    private void FixedUpdate()
    {

        _characterController.SimpleMove(transform.forward * moving.action.ReadValue<Vector2>().y * speed + transform.right * moving.action.ReadValue<Vector2>().x * speed);

        transform.Rotate(new Vector3(0, looking.action.ReadValue<Vector2>().x * 10, 0) * cameraSpeed);

        Debug.Log(moving.action.ReadValue<Vector2>());
    }
}
