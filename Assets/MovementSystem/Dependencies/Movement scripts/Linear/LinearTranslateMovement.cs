using UnityEngine;

public class LinearTranslateMovement : MovementTypeController
{
    private float _rotationValue;
    private Transform _movableObject;

    public LinearTranslateMovement(MovementController movementController) : base(movementController)
    {
        _movableObject = MovementController.GetMovableObjectTransform;
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
        _movableObject.Translate(new Vector3((MovementController.GetMoveXAxis * MovementController.GetMoveAroundSensitivity) * Time.fixedDeltaTime,
            0,  MovementController.GetMoveForwardSpeed * Time.fixedDeltaTime));
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
        
        RotatingObject.rotation = Quaternion.Euler(DefaultRotation.x, (DefaultRotation.y + _rotationValue) * RotateSensitivity, DefaultRotation.z);
    }
}
