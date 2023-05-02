using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    static GameManager _instance;

    //UI
    public GameObject canvas;

    public List<GameObject> liveEnemies = new List<GameObject>();
    public static GameManager Instance
    {
        get { 
                if (_instance == null)
                {
                    Debug.LogError("instance is null");
                }
                return _instance;
        }
    }
    private GameManager()
    {
        

    }

    private void Start()
    {
        //Create UI
    }
    private void Awake()
    {
        if (_instance == null)
            _instance = this;
    }

    public void OnCodeCreation(GameObject code)
    {
        liveEnemies.Add(code);
    }
    public void OnCodeDead(GameObject code)
    {
        liveEnemies.Remove(code);
    }
}
