using UnityEngine;

public class RPGVelocityMovement : MovementTypeController
{
    private float _rotationValue;
    
    
    public override void AutomaticMovement(Rigidbody movableObjectRigidbody, ref float speed, ref float moveXAxis, ref float moveZAxis)
    {
        Move(movableObjectRigidbody , ref speed, ref moveXAxis, ref moveZAxis);
    }

    
    public override void NonAutomaticMovement(Rigidbody movableObjectRigidbody, ref float speed, ref float moveXAxis, ref float moveZAxis)
    {
        if (Input.GetMouseButton(0))
        {
            Move(movableObjectRigidbody, ref speed, ref moveXAxis, ref moveZAxis);
        }
    }
  
    
    private void Move(Rigidbody rigidbody, ref float speed, ref float moveXAxis, ref float moveZAxis)
    {
        rigidbody.velocity = (new Vector3(((moveXAxis * speed) * 200) * Time.fixedDeltaTime, 0,
            ((moveZAxis * speed) * 200) * Time.fixedDeltaTime));
    }
    

    public override void Rotate(Transform rotatingObject, ref float rotateSensitivity, ref float moveXAxis, ref float moveZAxis)
    {
        rotatingObject.LookAt(rotatingObject.position + new Vector3((moveXAxis * rotateSensitivity) * Time.fixedDeltaTime, 0, (moveZAxis * rotateSensitivity) * Time.fixedDeltaTime));
    }
}
