using UnityEngine;

[System.Serializable]
public class MovementData
{
    [SerializeField] private float _speed;
    [SerializeField] private float _jumpForce;
    [SerializeField] private float _rotationAngle;
    [SerializeField] private float _rotationSpeed;

    public float Speed { get => _speed; private set { } }
    public float JumpForce { get => _jumpForce; private set { } }
    public float RotationAngle { get => _rotationAngle; private set { } }
    public float RotationSpeed { get => _rotationSpeed; private set { } }

    public MovementData(float speed = 1f, float jumpForce = 1f, float rotationAngle = 0, float rotationSpeed = 0)
    {
        _speed = speed;
        _jumpForce = jumpForce;
        _rotationAngle = rotationAngle;
        _rotationSpeed = rotationSpeed;
    }

}
