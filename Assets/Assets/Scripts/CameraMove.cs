using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour
{
    [SerializeField] float cameraMoveSpeed;

    public Transform CameraMoveTo;
    
    
    private float distanceController;


    private void Update()
    {
        distanceController = (CameraMoveTo.position - transform.position).sqrMagnitude;
        if (distanceController > (.1f * 2))
        {
            transform.position = Vector3.Lerp(transform.position, CameraMoveTo.position, cameraMoveSpeed * Time.deltaTime);
            transform.rotation = Quaternion.Lerp(transform.rotation , CameraMoveTo.rotation ,cameraMoveSpeed * Time.deltaTime);
        }
    }
}
