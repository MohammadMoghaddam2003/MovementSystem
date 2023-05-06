using UnityEngine;

public class LinearTranslateMovement : MovementTypeController
{
    private float _rotationValue;
    
    
    public override void AutomaticMovement(Transform movableObject, ref float moveAroundSensitivity, ref float moveForwardSpeed, ref float moveXAxis)
    {
        Move(movableObject , ref moveAroundSensitivity, ref moveForwardSpeed, ref moveXAxis);
    }

    
    public override void NonAutomaticMovement(Transform movableObject, ref float moveAroundSensitivity, ref float moveForwardSpeed, ref float moveXAxis)
    {
        if (Input.GetMouseButton(0))
        {
            Move(movableObject, ref moveAroundSensitivity, ref moveForwardSpeed, ref moveXAxis);
        }
    }
  
    
    private void Move(Transform movableObject, ref float moveAroundSensitivity, ref float moveForwardSpeed, ref float moveXAxis)
    {
        movableObject.Translate(new Vector3((moveXAxis * moveAroundSensitivity) * Time.fixedDeltaTime,
            0,  moveForwardSpeed * Time.fixedDeltaTime));
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
