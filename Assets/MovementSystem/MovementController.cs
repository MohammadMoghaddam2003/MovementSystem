using UnityEngine;


public class MovementController :  MonoBehaviour
{
    #region Fields

    #region Common fields
    
    public MovementState MovementState;

    
    [SerializeField] private Joystick joystick;
    [SerializeField] private GameObject rotatingObject;
    [SerializeField] private float rotateSensitivity;

    [Header("Movement Manage")]
    [SerializeField] private bool automaticMoveForward;
    
    [Header("Reset velocity to zero when does not touching")]
    [SerializeField] private bool resetVelocity;

    [Header("Methods Of Character Moving")] 
    public MoveMethod MoveMethods;
    
    
    #endregion

    #region Linear movement fields
    
    [Header("Input")]
    public Control ControlInput;

    [Header("Rotation Manage")]
    [SerializeField] private Quaternion defaultRotation;
    
    [Header("When does not touching, return rotation to the default")]
    [SerializeField] private bool resetRotation;
    
    
    [SerializeField] private float moveForwardSpeed;
    [SerializeField] private float moveAroundSensitivity;
    [SerializeField] private float moveAroundDelay;
    
    [Header("Clamp X Axis Position")]
    public bool Clamp;
    [SerializeField] private float min;
    [SerializeField] private float max;
    
    #endregion

    #region RPG movement fields

    [SerializeField] private float moveSpeed;


    #endregion
    
    #region Script fields

    private MovementTypeController _movementTypeController;
    private Vector3 _position;
    private Rigidbody _rigidbody;
    
    private float _moveXAxis;
    private float _moveZAxis;
    private float _delayManage;
    private float _xAxis;
    private bool _setDefaultSpeed;
    private static bool _moving;


    public static bool LuckMoveAround;
    public static bool LuckMovement;


    #endregion

    #endregion

    #region  Properties

    public static bool GetIsMoving { get { return _moving; } }
    public Transform GetMovableObjectTransform { get { return transform; } }
    public Transform GetRotatingObject { get { return rotatingObject.transform; } }
    public Rigidbody GetMovableObjectRigidbody { get { return _rigidbody; } }
    public Quaternion GetDefaultRotation { get { return defaultRotation; } }
    public ref float GetMoveAroundSensitivity { get { return ref moveAroundSensitivity; } }
    public ref float GetMoveForwardSpeed { get { return ref moveForwardSpeed; } }
    public ref float GetMoveXAxis { get { return ref _moveXAxis; } }
    public ref float GetMoveZAxis { get { return ref _moveZAxis; } }
    public ref float GetRotateSensitivity { get { return ref rotateSensitivity; } }
    public ref float GetMoveSpeed { get { return ref moveSpeed; } }

    #endregion
    
    
    #region Functions
    
    
    private void Awake()
    {
        _rigidbody = gameObject.GetComponent<Rigidbody>();
    }
    
    private void Start()
    {
        ChooseMovementType();
        ResetFields();
    }

    private void FixedUpdate()
    {
        if (!LuckMovement)
        {
            MovementManager();
        }
    }
    
    private void Update()
    {
        if (!_moving && automaticMoveForward)
        {
            MovementStatusManager();
        }
        
        
        if (Input.GetMouseButtonUp(0))
        {
            if(resetVelocity)
                ResetVelocity();
            
            MovementStatusManager();
        }
        else if(Input.GetMouseButtonDown(0))
        {
            MovementStatusManager();
        }
    }
    
    private void ChooseMovementType()
    {
        if (MovementState == MovementState.Linear)
        {
            switch (MoveMethods)
            {
                case MoveMethod.Translate:
                {
                    _movementTypeController = new LinearTranslateMovement(this);
                    break;
                }
                case MoveMethod.AddForce:
                {
                    _movementTypeController = new LinearAddForceMovement(this);
                    break;
                }
                case MoveMethod.Velocity:
                {
                    _movementTypeController = new LinearVelocityMovement(this);
                    break;
                }
            }
        }
        else
        {
            switch (MoveMethods)
            {
                case MoveMethod.Translate:
                {
                    _movementTypeController = new RPGTransformMovement(this);
                    break;
                }
                case MoveMethod.AddForce:
                {
                    _movementTypeController = new RPGAddForceMovement(this);
                    break;
                }
                case MoveMethod.Velocity:
                {
                    _movementTypeController = new RPGVelocityMovement(this);
                    break;
                }
            }
        }
    }
    
    private void ResetFields()
    {
        _moveXAxis = 0;
        _moveZAxis = 0;
        _delayManage = 0;
        _xAxis = 0;
        
        _setDefaultSpeed = false;
        LuckMovement = false;
        LuckMoveAround = false;
    }

    private void ResetVelocity()
    {
        _rigidbody.velocity = Vector3.zero;
    }
    
    private void MovementManager()
    {
        if (MovementState == MovementState.Linear)
        {
            if (ControlInput == Control.Touch)
            {
                TouchMovement();
            }
            else
            {
                JoystickMovement();
            }
        
            if (resetRotation)
            {
                ResetRotation();
            }   
        }
        else
        {
            RpgJoystickMovement();
        }


        if(Clamp)
        {
            ClampXAxis();
        }
    }
    
    private void TouchMovement()
    {
        if (!LuckMoveAround && Input.GetMouseButton(0))
        {
            if (moveAroundDelay == 0)
            {
                _moveXAxis = Input.GetAxis("Horizontal");
                _moveXAxis /= 100;
            }
            else
            {
                Delay((Input.GetAxis("Horizontal") / 100));
            }
        }


        CallMovements();
    }
    
    private void JoystickMovement()
    {
        if (!LuckMoveAround && Input.GetMouseButton(0))
        {
            if (moveAroundDelay == 0)
            {
                _moveXAxis = joystick.Horizontal;
            }
            else
            {
                Delay(joystick.Horizontal);
            }
        }

        CallMovements();
    }
    
    private void RpgJoystickMovement()
    {
        if (automaticMoveForward)
        {
            if (!LuckMoveAround && Input.GetMouseButton(0))
            {
                _moveXAxis = joystick.Horizontal;
                _moveZAxis = joystick.Vertical;        
            }
            else if(!_setDefaultSpeed)
            {
                _setDefaultSpeed = true;
                _moveZAxis = 1;
            }
        }
        else if(!LuckMoveAround && Input.GetMouseButton(0))
        {
            _moveXAxis = joystick.Horizontal;
            _moveZAxis = joystick.Vertical;     
        }
        

        CallMovements();
    }
    
    private void CallMovements()
    {
        if (automaticMoveForward) 
        { 
            _movementTypeController.AutomaticMovement(); }
        else 
        {
            _movementTypeController.NonAutomaticMovement();
        }  
        
        
        _movementTypeController.Rotate();
    }
    
    private void Delay(float value)
    {
        _delayManage = value;
        
        
        if (value != 0)
        {
            _moveXAxis = Mathf.Lerp(_moveXAxis,_delayManage, (1 / moveAroundDelay) * Time.deltaTime);
        }
        else if (_moveXAxis > 0)
        {
            _moveXAxis = Mathf.Lerp(_moveXAxis, 0, 20f * Time.deltaTime);
        }
        else if (_moveXAxis < 0)
        {
            _moveXAxis = Mathf.Lerp(_moveXAxis, 0, 20f * Time.deltaTime);
        }
    }
    
    private void ClampXAxis()
    {
        _position = transform.position;
        _xAxis = Mathf.Clamp(_position.x, min, max);
        transform.position = new Vector3(_xAxis, _position.y, _position.z);
    }

    private void ResetRotation()
    {
        if (!Input.GetMouseButton(0) && _moveXAxis != 0)
        {
            _moveXAxis = 0;
        }
    }

    private void MovementStatusManager()
    {
        if (_moving && !automaticMoveForward)
        {
            _moving = false;
        }
        else if(!_moving)
        {
            _moving = true;
        }
    }
    
    #endregion
}


public enum MovementState
{
    Linear,
    RPG
}

public enum Control
{
    Joystick,
    Touch
}

public enum MoveMethod
{
    Translate,
    AddForce,
    Velocity
}
