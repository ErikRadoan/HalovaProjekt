using UnityEngine;

namespace Player
{
    public class PlayerMovement : MonoBehaviour
    {
        public float mouseSensitivity = 2.0f;

        private float _rotationX;
        private float _rotationY;
        private Camera _mainCamera;

        private CharacterController _controller;
        private MeshRenderer _meshRenderer;
        private Vector3 _playerVelocity;
        private bool _groundedPlayer;
        [SerializeField] private float playerSpeed = 2.0f;
        [SerializeField] private float jumpHeight = 1.0f;
        private const float GravityValue = -9.81f;
        
        [SerializeField] private float groundCheckDistance = 0.1f;

        private void Start()
        {
            _meshRenderer = GetComponent<MeshRenderer>();
            _controller = GetComponent<CharacterController>();
            _mainCamera = Camera.main;
        }

        private void Update()
        {
            RotatePlayer();

            _groundedPlayer = Physics.Raycast(transform.position, -Vector3.up, _meshRenderer.bounds.size.y/2 + groundCheckDistance);
            if (_groundedPlayer && _playerVelocity.y < 0)
            {
                _playerVelocity.y = 0f;
            }

            Vector3 move = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
            move = _mainCamera.transform.TransformDirection(move);
            _controller.Move(move * Time.deltaTime * playerSpeed);

            // Changes the height position of the player..
            if (Input.GetButtonDown("Jump") && _groundedPlayer)
            {
                _playerVelocity.y += Mathf.Sqrt(jumpHeight * -3.0f * GravityValue);
            }

            _playerVelocity.y += GravityValue * Time.deltaTime;
            _controller.Move(_playerVelocity * Time.deltaTime);
        }

        private void RotatePlayer()
        {
            float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity;
            float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity;

            _rotationX -= mouseY;
            _rotationX = Mathf.Clamp(_rotationX, -90f, 90f);

            _rotationY += mouseX;

            transform.localRotation = Quaternion.Euler(0f, _rotationY, 0f);
            _mainCamera.transform.localRotation = Quaternion.Euler(_rotationX, 0f, 0f);
        }
    }
}
