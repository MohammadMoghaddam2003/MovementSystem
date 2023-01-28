using UnityEngine;

public class RPGTransformMovement : MovementTypeController
{ 
        private float _rotationValue;
        private float _speed;
        private Transform _movableObject;

        public RPGTransformMovement(MovementController movementController) : base(movementController)
        {
            _movableObject = MovementController.GetMovableObjectTransform;
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
            _movableObject.Translate(new Vector3(MovementController.GetMoveXAxis * _speed * Time.fixedDeltaTime, 0,
                MovementController.GetMoveZAxis * _speed * Time.fixedDeltaTime));
        }
    

        public override void Rotate()
        {
            RotatingObject.LookAt(RotatingObject.position + new Vector3((MovementController.GetMoveXAxis * RotateSensitivity) * Time.fixedDeltaTime,
                0, (MovementController.GetMoveZAxis * RotateSensitivity) * Time.fixedDeltaTime));
        }
}
