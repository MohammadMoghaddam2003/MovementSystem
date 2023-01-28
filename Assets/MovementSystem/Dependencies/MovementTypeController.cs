using UnityEngine;

public abstract class  MovementTypeController
{
    public readonly Transform RotatingObject;
    public readonly MovementController MovementController;
    public readonly Quaternion DefaultRotation;
    public readonly float RotateSensitivity;
    
    public MovementTypeController(MovementController movementController)
    {
        MovementController = movementController;
        DefaultRotation = movementController.GetDefaultRotation;
        RotateSensitivity = MovementController.GetRotateSensitivity;
        RotatingObject = MovementController.GetRotatingObject;
    }

    
    public abstract void AutomaticMovement();

    public abstract void NonAutomaticMovement();

    public abstract void Rotate();
}
