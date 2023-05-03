using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectRotator : MonoBehaviour
{
    public Vector3 rotationSpeed = new Vector3(0f, 10f, 0f); // The speed of rotation along each axis

    private void Update()
    {
        // Rotate the object based on the rotation speed
        transform.Rotate(rotationSpeed * Time.deltaTime);
    }
}
