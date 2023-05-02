using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.PlayerSettings;

public class Tower : MonoBehaviour
{
    public enum MODE
    {
        Idle,
        AutoFire,
        Target
    }

    GameManager gameManager;
    public MODE mode = MODE.Idle;
    [Header("Stats")]
    public float projSpeed = 10;
    [Range(0f, 1f)]
    public float fireRate = 0.5f;
    public float damage = 1;
    public float critChance = 0.05f;
    public float critFactor = 1.20f;

    float fireTimer = 0f;

    public GameObject projectilePrefab;
    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameManager.Instance;
    }

    private void Update()
    {
        fireTimer += Time.deltaTime;
        if(fireTimer > fireRate)
        {
            fireTimer = 0f;
            if (mode == MODE.Target)
            {
                GameObject target = GetClosestEnemie();
                if (target != null)
                {
                    TargetFire(target);
                    Vector2 direction = (target.transform.position - transform.position).normalized;
                    Vector2 spawnPosition = (Vector2)transform.position + direction;
                    Projectile proj = CreateProjectile(spawnPosition, projSpeed, damage, false);
                    proj.target= target;
                    proj.moveDirection= direction;
                }
            }else if(mode == MODE.AutoFire)
            {
                Fire(transform.forward);
            }

 
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log(collision.gameObject.tag);
    }

    void TargetFire(GameObject target)
    {
        Vector2 direction = (target.transform.position - transform.position).normalized;
        Vector2 spawnPosition = (Vector2)transform.position + direction;
        Projectile proj = CreateProjectile(spawnPosition, projSpeed, damage, false);
        proj.target = target;
        proj.moveDirection = direction;
    }

    void Fire(Vector2 pos)
    {
        GameObject projectile = Instantiate(projectilePrefab, pos, Quaternion.identity);
    }

    Projectile CreateProjectile(Vector2 spawnPos, float speed, float damage, bool crit)
    {
        Projectile proj= Instantiate(projectilePrefab, spawnPos, Quaternion.identity).GetComponent<Projectile>();
        proj.speed = speed;
        proj.damage = damage;
        proj.crit = crit;



        return proj;
    }
    GameObject GetClosestEnemie()   
    {
        GameObject bestTarget = null;
        float closestDistanceSqr = Mathf.Infinity;
        Vector3 currentPosition = transform.position;
        foreach (GameObject potentialTarget in gameManager.liveEnemies)
        {
            Vector3 directionToTarget = potentialTarget.transform.position - currentPosition;
            float dSqrToTarget = directionToTarget.sqrMagnitude;
            if (dSqrToTarget < closestDistanceSqr)
            {
                closestDistanceSqr = dSqrToTarget;
                bestTarget = potentialTarget;
            }
        }

        return bestTarget;
    }
}
