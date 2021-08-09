using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    [SerializeField]
    GameObject target;

    [SerializeField]
    float distanceFromTarget = 10f;

    [SerializeField]
    float angleAboveTarget = 30f;

    [SerializeField]
    float magnetism = 1f;

    float magnitude;
    Vector3 dirVector = Vector3.zero;
    Vector3 targetPosition = Vector3.zero;


    // Update is called once per frame
    void Update()
    {
        
    }

    private void LateUpdate()
    {
        CalculatePosition();
        CalculateForce();
        ApplyForce();
    }

    private void CalculatePosition()
    {
        float radians = angleAboveTarget * Mathf.Deg2Rad;
        targetPosition = target.transform.position + 
            new Vector3(0,
            Mathf.Cos(radians), 
            -Mathf.Sin(radians)) *
            distanceFromTarget;
    }

    private void CalculateForce()
    {
        //First get the dir to target
        dirVector = target.transform.position - targetPosition;
        magnitude = dirVector.magnitude;
        dirVector = dirVector.normalized;
    }

    private void ApplyForce()
    {
        transform.position = targetPosition;
        transform.LookAt(target.transform);
    }
}
