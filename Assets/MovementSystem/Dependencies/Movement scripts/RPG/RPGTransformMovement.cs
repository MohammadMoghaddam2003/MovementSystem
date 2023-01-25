using UnityEngine;

public class RPGTransformMovement : MovementTypeController
{ 
        private float _rotationValue;
    
    
        public override void AutomaticMovement(Transform movableObject, ref float speed, ref float moveXAxis, ref float moveZAxis)
        {
            Move(movableObject , ref speed, ref moveXAxis, ref moveZAxis);
        }

    
        public override void NonAutomaticMovement(Transform movableObject, ref float speed, ref float moveXAxis, ref float moveZAxis)
        {
            if (Input.GetMouseButton(0))
            {
                Move(movableObject, ref speed, ref moveXAxis, ref moveZAxis);
            }
        }
  
    
        private void Move(Transform movableObject, ref float speed, ref float moveXAxis, ref float moveZAxis)
        {
            movableObject.Translate(new Vector3(moveXAxis * speed * Time.fixedDeltaTime, 0,
                moveZAxis * speed * Time.fixedDeltaTime));
        }
    

        public override void Rotate(Transform rotatingObject, ref float rotateSensitivity, ref float moveXAxis, ref float moveZAxis)
        {
            rotatingObject.LookAt(rotatingObject.position + new Vector3((moveXAxis * rotateSensitivity) * Time.fixedDeltaTime, 0, (moveZAxis * rotateSensitivity) * Time.fixedDeltaTime));
        }
}
