using UnityEngine;

public abstract class  MovementTypeController
{
    #region Automatic movement overloads

    public virtual void AutomaticMovement(Transform movableObject, ref float moveAroundSensitivity, ref float moveForwardSpeed, ref float moveXAxis) { }
    public virtual void AutomaticMovement(Rigidbody movableObjectRigidbody, ref float moveAroundSensitivity, ref float moveForwardSpeed, ref float moveXAxis) { }

    
    #endregion

    
    #region Non_Automatic movement overloads

    public virtual void NonAutomaticMovement(Transform movableObject, ref float moveAroundSensitivity, ref float moveForwardSpeed, ref float moveXAxis) { }
    public virtual void NonAutomaticMovement(Rigidbody movableObjectRigidbody, ref float moveAroundSensitivity, ref float moveForwardSpeed, ref float moveXAxis) { }

    
    #endregion


    #region Rotate overloads

    public virtual void Rotate(Transform rotatingObject, Quaternion defaultRotation, ref float rotateSensitivity, ref float moveXAxis) { }
    public virtual void Rotate(Transform rotatingObject, ref float rotateSensitivity, ref float moveXAxis, ref float moveZAxis) { }

    
    #endregion
    
}
