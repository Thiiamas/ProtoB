using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateAround : MonoBehaviour
{

    public Transform target; // The object to orbit around
    public float orbitSpeed = 1f; // The speed of the orbit
    public float scrollSpeed = 10f; // The speed of the zoom

    private float _distance; // The current distance from the camera to the target
    private Vector3 _offset; // The offset from the target to the camera

    private void Start()
    {
        _distance = Vector3.Distance(transform.position, target.position);
        _offset = transform.position - target.position;
    }

    private void LateUpdate()
    {
        // Get the horizontal and vertical inputs to orbit the camera
        float horizontal = Input.GetAxis("Horizontal") * orbitSpeed;
        float vertical = Input.GetAxis("Vertical") * orbitSpeed;

        // Rotate the camera around the target
        transform.RotateAround(target.position, Vector3.up, orbitSpeed);
        transform.RotateAround(target.position, transform.right, -orbitSpeed);

        // Get the scroll input to zoom the camera
        float scroll = Input.GetAxis("Mouse ScrollWheel") * scrollSpeed;

        // Zoom the camera towards or away from the target
        _distance -= scroll;

    }
}
