using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;
using static Enemy;

[RequireComponent(typeof(LineRenderer))]
public class KochLineAlive : KochGenerator
{
    LineRenderer _lineRenderer;
    [Range(0, 1)]
    public float _lerpAmount;
    Vector3[] _lerpPosition;
    List<Vector2> _lerpPositionList;
    public float _generateMultiplier;

    public Material _material;
    public Color _color;
    private Material _matInstance;
    [Range(2, 4)]
    public float _emissionMultiplier;

    //For Enemy
    Enemy enemy;


    // Start is called before the first frame update
    void Start()
    {
        _lineRenderer = GetComponent<LineRenderer>();
        _lineRenderer.enabled = true;
        _lineRenderer.useWorldSpace = false;
        _lineRenderer.loop = true;
        _lineRenderer.positionCount = _position.Length;
        _lineRenderer.SetPositions(_position);
        _lerpPosition = new Vector3[_position.Length];
        //List for collider
        _lerpPositionList = new List<Vector2>();
        //apply material
        _matInstance = new Material(_material);
        _lineRenderer.material = _matInstance;

        if(transform.parent != null && transform.parent.parent != null && transform.parent.parent.parent != null)
        {
            enemy = transform.parent.parent.parent.GetComponent<Enemy>();
            if(enemy != null )
            {
                enemy.RefreshEvent += HandleRefreshEvent;
            }
        }

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("ici 2d");
    }

    // Update is called once per frame
    void Update()
    {
        DrawGenerator();
        float currentTime = Time.time;
        _emissionMultiplier = (Mathf.Sin(2 * currentTime) / 2) + 1.5f;
        _lerpAmount = Mathf.Sin(2 * Time.time) / 2 + 0.5f;
        _matInstance.SetColor("_EmissionColor", _color * _emissionMultiplier);
        if (_generationCount != 0)
        {
            _lerpPosition = new Vector3[_position.Length];
            for (int i = 0; i < _position.Length; i++)
            {
                //change lerp arcording to _lerpAmount
                _lerpPosition[i] = Vector3.Lerp(_position[i], _targetPosition[i], _lerpAmount);
            }
            if (_useBezierCurves)
            {
                _bezierPosition = BezierCurve(_lerpPosition, _bezierVertexCount);
                _lineRenderer.positionCount = _bezierPosition.Length;
                _lineRenderer.SetPositions(_bezierPosition);
            }
            else
            {
                _lineRenderer.positionCount = _lerpPosition.Length;
                _lineRenderer.SetPositions(_lerpPosition);
            }
        }

        //if (Input.GetKeyDown(KeyCode.O))
        //{
        //    Debug.Log("O");
        //    KochGenerate(_targetPosition, true, _generateMultiplier);
        //    _lerpPosition = new Vector3[_position.Length];
        //    _lineRenderer.positionCount = _position.Length;
        //    _lineRenderer.SetPositions(_position);
        //    _lerpAmount = 0;
        //}
        //if (Input.GetKeyDown(KeyCode.I))
        //{
        //    Debug.Log("i");
        //    KochGenerate(_targetPosition, false, _generateMultiplier);
        //    _lerpPosition = new Vector3[_position.Length];
        //    _lineRenderer.positionCount = _position.Length;
        //    _lineRenderer.SetPositions(_position);
        //    _lerpAmount = 0;
        //}

    }

    //Events
    public void HandleRefreshEvent(object sender, KochRefreshEventArgs a)
    {
        Debug.Log("here");
        _lerpAmount = a.lerpAmount;
        _generateMultiplier = a.generateMultiplier;
        _material = a.material;
        _color = a.color;
        _emissionMultiplier = a.emissionMultiplier;
    }

}
