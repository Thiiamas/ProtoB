using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [Header("stats")]
    [Range(5, 50)]
    public float radius = 13;

    [Range(0.1f, 2f)]
    public float spawnRate = 0.5f;
    public float spawnLife = 1f;
    [Range(0f, 2f)]
    public float spawnMass = 1f;

    float timer = 0;


    public GameObject codePrefab;
    GameManager gameManager;
    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameManager.Instance;
    }

    private void FixedUpdate()
    {
        timer += Time.fixedDeltaTime;
        if(timer > spawnRate)
        {
            timer = 0;
            Spawn();
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, radius);
    }
    void Spawn() 
    {
        Vector2 spawnLocation = UnityEngine.Random.insideUnitCircle.normalized * radius;
        Enemy enemy = Instantiate(codePrefab,spawnLocation,Quaternion.identity).GetComponent<Enemy>();
        enemy.life = spawnLife;
        enemy.mass = spawnMass;
    }
}
