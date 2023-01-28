using UnityEngine;

public class LinearAddForceMovement : MovementTypeController
{
    private float _rotationValue;
    private Rigidbody _rigidbody;


    public LinearAddForceMovement(MovementController movementController) : base(movementController)
    {
        _rigidbody = MovementController.GetMovableObjectRigidbody;
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
        _rigidbody.AddForce(new Vector3(((MovementController.GetMoveXAxis * MovementController.GetMoveAroundSensitivity) * 200) * Time.fixedDeltaTime,
            0,  (MovementController.GetMoveForwardSpeed * 200) * Time.fixedDeltaTime),ForceMode.Force);
    }
    

    public override void Rotate()
    {
        if (MovementController.GetMoveXAxis != 0)
        {
            _rotationValue = Mathf.Lerp(_rotationValue,MovementController.GetMoveXAxis, 5 * Time.fixedDeltaTime);
        }
        else if (_rotationValue > 0)
        {
            _rotationValue = Mathf.Lerp(_rotationValue, 0, 20f * Time.fixedDeltaTime);
        }
        else if (_rotationValue < 0)
        {
            _rotationValue = Mathf.Lerp(_rotationValue, 0, 20f * Time.fixedDeltaTime);
        }
        
        RotatingObject.rotation = Quaternion.Euler(DefaultRotation.x,
            (DefaultRotation.y + _rotationValue) * RotateSensitivity,DefaultRotation.z);
    }
}
