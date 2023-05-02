using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [Header("stats")]
    [Range(5, 13)]
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
        Vector2 spawnLocation = Random.insideUnitCircle.normalized * radius;
        CodeEnemy code = Instantiate(codePrefab,spawnLocation,Quaternion.identity).GetComponent<CodeEnemy>();
        code.life = spawnLife;
        code.mass = spawnMass;
    }
}
