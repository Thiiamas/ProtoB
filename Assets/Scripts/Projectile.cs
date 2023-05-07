using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [Header("stats")]
    public float speed = 1000f;
    public float damage = 1f;
    public bool crit = false;
    
    public GameObject target;

    public Vector2 moveDirection;
    CodeEnemy enemy;

    //Graphics
    [Header("Graphics")]
    LineRenderer lineRenderer;
    public float lineWidth = 0.02f;
    public float radius = 1;
    public int segments = 360;







    // Start is called before the first frame update
    void Start()
    {
        if (target != null)
        {
            target.TryGetComponent<CodeEnemy>(out enemy);
        }
        TryGetComponent<LineRenderer>(out lineRenderer);
        if (lineRenderer!=null)
        {
            //make a circle
            DrawCircle();
        }

        if(enemy != null)
        {
            //Subscribe();
        }

        Destroy(gameObject, 5);
    }

    private void Update()
    {
        if(target != null)
        {
            transform.Translate(moveDirection * Time.deltaTime * speed);
        }
        else
        {
            transform.Translate(transform.right * Time.deltaTime * speed);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        var tag = other.gameObject.tag;

        switch (tag)
        {
            case "Player":
                break;
            case "Enemy":
                IEnemy enemy = other.gameObject.GetComponent<Enemy>();
                enemy.OnProjectileColision();
                break;

        }
    }

    private void DrawCircle()
    {
        lineRenderer.useWorldSpace = false;
        lineRenderer.startWidth = lineWidth;
        lineRenderer.endWidth = lineWidth;
        lineRenderer.positionCount = segments + 1;

        var pointCount = segments + 1; // add extra point to make startpoint and endpoint the same to close the circle
        var points = new Vector3[pointCount];

        for (int i = 0; i < pointCount; i++)
        {
            var rad = Mathf.Deg2Rad * (i * 360f / segments);
            points[i] = new Vector3(Mathf.Sin(rad) * radius, Mathf.Cos(rad) * radius,0);
        }

        lineRenderer.SetPositions(points);
    }
    //Observer
    /*public void Subscribe()
    {
        enemy.eventHandler += OnTargetDead;
    }*/

    public void OnTargetDead(object sender, EventArgs args)
    {
        Destroy(this.gameObject);   
    }
}
