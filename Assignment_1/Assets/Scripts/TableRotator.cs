using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class TableRotator : MonoBehaviour
{
    // Variables
    public float rotationSpeed = 30;
    public float angularLimit = 20;

    // Runs before the first frame of the script
    void Start()
    {
        GameObject roof = GameObject.Find("Roof");
        if (roof)
        {
            roof.GetComponent<MeshRenderer>().enabled = false;
        }
    }

    // Runs on every frame once
    void Update()
    {
        // Rotate the table around x and z axis
        float newZAxisRotation = transform.rotation.eulerAngles.z;
        float newXAxisRotation = transform.rotation.eulerAngles.x;

        //Debug.Log($"Z: {newZAxisRotation}");
        //Debug.Log($"{angularLimit}, {360-angularLimit}");
        if (Input.GetKey(KeyCode.LeftArrow) && (newZAxisRotation <= angularLimit || newZAxisRotation >= 360-angularLimit))
            newZAxisRotation += rotationSpeed * Time.deltaTime;
        if (Input.GetKey(KeyCode.RightArrow) && (newZAxisRotation <= angularLimit || newZAxisRotation >= 360 - angularLimit))
            newZAxisRotation -= rotationSpeed * Time.deltaTime;
        if (Input.GetKey(KeyCode.UpArrow) && (newXAxisRotation <= angularLimit || newXAxisRotation >= 360 - angularLimit))
            newXAxisRotation += rotationSpeed * Time.deltaTime;
        if (Input.GetKey(KeyCode.DownArrow) && (newXAxisRotation <= angularLimit || newXAxisRotation >= 360 - angularLimit))
            newXAxisRotation -= rotationSpeed * Time.deltaTime;

        if (newXAxisRotation < 180)
        {
            if (newXAxisRotation > angularLimit)
            {
                newXAxisRotation = angularLimit - 0.1f;
            }
        } else if (newXAxisRotation > 180)
        {
            if (newXAxisRotation < 360 - angularLimit)
            {
                newXAxisRotation = 360 - angularLimit + 0.1f;
            }
        }

        if (newZAxisRotation < 180)
        {
            if (newZAxisRotation > angularLimit)
            {
                newZAxisRotation = angularLimit - 0.1f;
            }
        }
        else if (newZAxisRotation > 180)
        {
            if (newZAxisRotation < 360 - angularLimit)
            {
                newZAxisRotation = 360 - angularLimit + 0.1f;
            }
        }

        // not possible since we are sing eulerangles which is 0-360 degrees :(
        //newZAxisRotation = Mathf.Clamp(newZAxisRotation, -angularLimit, angularLimit);
        //newXAxisRotation = Mathf.Clamp(newXAxisRotation, -angularLimit, angularLimit);
        //Debug.Log($"MIN: {-angularLimit}| MAX: {angularLimit} | CURZ: {newZAxisRotation}");
        //Debug.Log($"MIN: {-angularLimit}| MAX: {angularLimit} | CURX: {newXAxisRotation}");

        Vector3 newRotationEulars = new Vector3(newXAxisRotation, transform.rotation.eulerAngles.y, newZAxisRotation);
        transform.rotation = Quaternion.Euler(newRotationEulars);
    }
}
