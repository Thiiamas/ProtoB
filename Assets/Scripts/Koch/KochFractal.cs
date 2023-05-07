using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KochFractal : MonoBehaviour
{
    private LineRenderer lineRenderer;

    // The start and end points of the line segment
    private Vector3 startPoint;
    private Vector3 endPoint;

    // The number of iterations to use when generating the fractal
    public int iterations;

    // The length of the line segment
    public float segmentLength;

    // The angle between line segments
    public float angle;

    // Start is called before the first frame update
    void Start()
    {
        // Get the Line Renderer component attached to this game object
        lineRenderer = GetComponent<LineRenderer>();

        // Set the start and end points of the line segment
        startPoint = new Vector3(-5, 0, 0);
        endPoint = new Vector3(5, 0, 0);

        // Generate the fractal
        GenerateFractal(iterations);
    }

    // Generates the Koch fractal
    void GenerateFractal(int iterations)
    {
        // Clear the line renderer
        lineRenderer.positionCount = 0;

        // Set the initial line segment
        Vector3[] positions = new Vector3[] { startPoint, endPoint };
        lineRenderer.positionCount = positions.Length;
        lineRenderer.SetPositions(positions);

        // Generate the fractal by dividing the line segment and creating triangles
        for (int i = 0; i < iterations; i++)
        {
            List<Vector3> newPositions = new List<Vector3>();

            for (int j = 0; j < positions.Length - 1; j++)
            {
                // Divide the line segment into three equal parts
                Vector3 a = positions[j];
                Vector3 b = positions[j + 1];
                Vector3 dir = (b - a).normalized;
                float dist = Vector3.Distance(a, b);
                Vector3 p1 = a + dir * dist / 3f;
                Vector3 p2 = b - dir * dist / 3f;

                // Calculate the position of the triangle
                Vector3 delta = p2 - p1;
                Vector3 normal = Quaternion.AngleAxis(-angle, Vector3.forward) * delta;
                Vector3 c = p1 + delta / 2f + normal * Mathf.Sqrt(3) / 6f;

                // Add the new line segments and triangle to the list of positions
                newPositions.Add(a);
                newPositions.Add(p1);
                newPositions.Add(c);
                newPositions.Add(p2);
            }

            // Add the final point to the list of positions
            newPositions.Add(positions[positions.Length - 1]);

            // Set the new line segments and triangles as the line renderer's positions
            positions = newPositions.ToArray();
            lineRenderer.positionCount = positions.Length;
            lineRenderer.SetPositions(positions);
        }
    }
}
