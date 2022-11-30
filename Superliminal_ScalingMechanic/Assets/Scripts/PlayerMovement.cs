using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public CharacterController _controller;

    public float _speed = 12f;
    
    private void Update()
    {

        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        _controller.Move((transform.right * x + transform.forward * z) * _speed * Time.deltaTime);
    }
}
