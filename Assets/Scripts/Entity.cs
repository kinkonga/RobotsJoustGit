using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Entity : MonoBehaviour
{
    [Header("ENTITY")]
    [SerializeField] float speed = 4f;
    [SerializeField] float rotationSpeed = 125;
    [SerializeField] float rotationDelta = 1;
    private Vector3 targetPosition;
    private Quaternion targetRotation;

    public float Speed { get => speed; set => speed = value; }
    public float RotationSpeed { get => rotationSpeed; set => rotationSpeed = value; }
    public float RotationDelta { get => rotationDelta; set => rotationDelta = value; }
    public Vector3 TargetPosition { get => targetPosition; set => targetPosition = value; }
    public Quaternion TargetRotation { get => targetRotation; set => targetRotation = value; }

    //UPDATE
    protected void DoForward()
    {
        if (!IsArrived())
        {
            transform.position = Vector3.MoveTowards(transform.position, TargetPosition, Speed * Time.deltaTime);
        }
    }
    protected void DoRotation()
    {
        if (!IsRotFinish())
        {
            transform.Rotate(new Vector3(0, RotationDelta * RotationSpeed * Time.deltaTime, 0));
        }
    }
    protected bool IsArrived()
    {
        if (transform.position == TargetPosition)
        {
            return true;
        }
        return false;
    }
    protected bool IsRotFinish()
    {
        if (transform.rotation == TargetRotation)
        {
            return true;
        }
        return false;
    }
    protected Vector3 getFowardVector()
    {
        switch (Mathf.Round(transform.rotation.eulerAngles.y))
        {
            case 0:
                return new Vector3(0, 0, 1);
            case 90:
                return new Vector3(1, 0, 0);
            case 180:
                return new Vector3(0, 0, -1);
            case 270:
                return new Vector3(-1, 0, 0);
            default:
                Debug.Log(this + " no orientation");
                return new Vector3(0, 0, 0);
        }
    }
    
}
