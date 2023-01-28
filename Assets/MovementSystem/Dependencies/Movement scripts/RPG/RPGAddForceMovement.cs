using UnityEngine;

public class RPGAddForceMovement : MovementTypeController
{
    private float _rotationValue;
    private float _speed;
    private Rigidbody _rigidbody;

    public RPGAddForceMovement(MovementController movementController) : base(movementController)
    {
        _rigidbody = MovementController.GetMovableObjectRigidbody;
        _speed = MovementController.GetMoveSpeed;
    }

    public override void AutomaticMovement()
    {
        Move();
    }

    
    public override void NonAutomaticMovement()
    {
        if (Input.GetMouseButton(0))
        {
            Move();
        }
    }
  
    
    private void Move()
    {
        _rigidbody.AddForce(new Vector3(((MovementController.GetMoveXAxis * _speed) * 200) * Time.fixedDeltaTime, 0,
            ((MovementController.GetMoveZAxis * _speed) * 200) * Time.fixedDeltaTime));
    }
    

    public override void Rotate()
    {
        RotatingObject.LookAt(RotatingObject.position + new Vector3((MovementController.GetMoveXAxis * RotateSensitivity) * Time.fixedDeltaTime,
            0, (MovementController.GetMoveZAxis * RotateSensitivity) * Time.fixedDeltaTime));
    }
}
