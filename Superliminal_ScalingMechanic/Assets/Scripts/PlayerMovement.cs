using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public CharacterController _controller;

    public float _speed = 12f;
    public float _gravity = -9.81f;

    public Transform _groundCheck;
    public float _groundDistance = 0.4f;
    public LayerMask _groundMask;
    private bool _isGrounded = false;

    private Vector3 _velocity;
    
    private void Update()
    {
        _isGrounded = Physics.CheckSphere(_groundCheck.position, _groundDistance, _groundMask);

        if (_isGrounded && _velocity.y < 0) _velocity.y = -2f;

        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 movement = transform.right * x + transform.forward * z;

        _controller.Move(movement * _speed * Time.deltaTime);

        _velocity.y += _gravity * Time.deltaTime;

        _controller.Move(_velocity * Time.deltaTime);
    }
}
