using UnityEditor;

#if UNITY_EDITOR

[CustomEditor(typeof(MovementController))]
public class MovementControllerEditor : Editor
{
    #region Fields

    #region Editor fields

    private MovementController _movementController;
    private SerializedProperty _script;

    #endregion
    
    #region Common fields

    private SerializedProperty _rotatingObject;
    private SerializedProperty _movementState;
    private SerializedProperty _joystick;
    private SerializedProperty _rotateSensitivity;
    private SerializedProperty _automaticMoveForward;
    private SerializedProperty _resetVelocity;
    private SerializedProperty _moveMethods;

    #endregion

    #region Linear movement fields

    private SerializedProperty _controlInput;
    private SerializedProperty _defaultRotation;
    private SerializedProperty _resetRotation;
    private SerializedProperty _moveForwardSpeed;
    private SerializedProperty _moveAroundSensitivity;
    private SerializedProperty _moveAroundDelay;
    private SerializedProperty _clamp;
    private SerializedProperty _min;
    private SerializedProperty _max;

    #endregion
    
    #region RPG movement fields

    private SerializedProperty _moveSpeed;
    
    #endregion

    #endregion
    
    
    #region Functions

    
    private void OnEnable()
    {
        SetFields();
    }
    
    private void SetFields()
    {
        #region Editor Fields

        _movementController = (MovementController)target;
        _script = serializedObject.FindProperty("m_Script");

        #endregion
        
        #region Common Fields

        _movementState = serializedObject.FindProperty("movementState");
        _rotatingObject = serializedObject.FindProperty("rotatingObject");
        _joystick = serializedObject.FindProperty("joystick");
        _rotateSensitivity = serializedObject.FindProperty("rotateSensitivity");
        _automaticMoveForward = serializedObject.FindProperty("automaticMoveForward");
        _resetVelocity = serializedObject.FindProperty("resetVelocity");
        _moveMethods = serializedObject.FindProperty("moveMethod");

        #endregion

        #region Linear Fields

        _controlInput = serializedObject.FindProperty("controlInput");
        
        _defaultRotation = serializedObject.FindProperty("defaultRotation");
        _resetRotation = serializedObject.FindProperty("resetRotation");
        
        _moveForwardSpeed = serializedObject.FindProperty("moveForwardSpeed");
        _moveAroundSensitivity = serializedObject.FindProperty("moveAroundSensitivity");
        _moveAroundDelay = serializedObject.FindProperty("moveAroundDelay");

        _clamp = serializedObject.FindProperty("clamp");
        _min = serializedObject.FindProperty("min");
        _max = serializedObject.FindProperty("max");

        #endregion

        #region RPG Fields

        _moveSpeed = serializedObject.FindProperty("moveSpeed");

        #endregion
    }
    
    public override void OnInspectorGUI()
    {
        serializedObject.Update();
        ShowFields();

        ApplyPropertiesChanges();
    }
    
    private void ApplyPropertiesChanges()
    {
        serializedObject.ApplyModifiedProperties();
    }
    
    private void ShowFields()
    {
        EditorGUILayout.PropertyField(_script);
        EditorGUILayout.Space();
        
        EditorGUILayout.PropertyField(_rotatingObject);
        EditorGUILayout.PropertyField(_movementState);

        if (_movementController.movementState == MovementState.Linear)
        {
            EditorGUILayout.PropertyField(_defaultRotation);
            EditorGUILayout.PropertyField(_rotateSensitivity);
            EditorGUILayout.PropertyField(_resetRotation);

            EditorGUILayout.PropertyField(_automaticMoveForward);
            EditorGUILayout.PropertyField(_moveForwardSpeed);
            EditorGUILayout.PropertyField(_moveAroundSensitivity);
            EditorGUILayout.PropertyField(_moveAroundDelay);
            EditorGUILayout.PropertyField(_clamp);
            
            if (_movementController.clamp)
            {
                EditorGUILayout.PropertyField(_min);
                EditorGUILayout.PropertyField(_max);
            }


            EditorGUILayout.PropertyField(_controlInput);
            if (_movementController.controlInput == Control.Joystick)
            {
                EditorGUILayout.PropertyField(_joystick);
            }
            
        }
        else
        {
            _movementController.clamp = false;
            EditorGUILayout.PropertyField(_joystick);
            EditorGUILayout.PropertyField(_rotateSensitivity);
            
            EditorGUILayout.PropertyField(_automaticMoveForward);
            EditorGUILayout.PropertyField(_moveSpeed);
            
        }
        
        EditorGUILayout.PropertyField(_moveMethods);

        if (_movementController.moveMethod != MoveMethod.Translate)
        {
            EditorGUILayout.PropertyField(_resetVelocity);
        }
        
        EditorGUILayout.Space();
    }
    
    

    #endregion
}

#endif
