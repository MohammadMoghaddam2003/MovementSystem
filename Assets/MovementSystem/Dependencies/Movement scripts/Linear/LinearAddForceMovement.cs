using UnityEngine;

public class LinearAddForceMovement : MovementTypeController
{
    private float _rotationValue;

    
    
    public override void AutomaticMovement(Rigidbody movableObjectRigidbody, ref float moveAroundSensitivity, ref float moveForwardSpeed, ref float moveXAxis)
    {
        Move(movableObjectRigidbody, ref moveAroundSensitivity, ref moveForwardSpeed, ref moveXAxis);
    }

    
    public override void NonAutomaticMovement(Rigidbody movableObjectRigidbody, ref float moveAroundSensitivity, ref float moveForwardSpeed, ref float moveXAxis)
    {
        if (Input.GetMouseButton(0))
        {
            Move(movableObjectRigidbody, ref moveAroundSensitivity, ref moveForwardSpeed, ref moveXAxis);
        }
    }
  
    
    private void Move(Rigidbody rigidbody, ref float moveAroundSensitivity, ref float moveForwardSpeed, ref float moveXAxis)
    {
        rigidbody.AddForce(new Vector3(((moveXAxis * moveAroundSensitivity) * 200) * Time.fixedDeltaTime,
            0,  (moveForwardSpeed * 200) * Time.fixedDeltaTime),ForceMode.Force);
    }
    

    public override void Rotate(Transform rotatingObject, Quaternion defaultRotation, ref float rotateSensitivity, ref float moveXAxis)
    {
        if (moveXAxis != 0)
        {
            _rotationValue = Mathf.Lerp(_rotationValue,moveXAxis, 5 * Time.fixedDeltaTime);
        }
        else if (_rotationValue != 0)
        {
            _rotationValue = Mathf.Lerp(_rotationValue, 0, 20f * Time.fixedDeltaTime);
        }
        
        rotatingObject.rotation = Quaternion.Euler(defaultRotation.x, (defaultRotation.y + _rotationValue) * rotateSensitivity, defaultRotation.z);
    }
}
