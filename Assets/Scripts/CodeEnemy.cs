using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CodeEnemy : MonoBehaviour
{
    //stats
    public float life = 1f;
    public float mass = 1f;

    //Components
    Rigidbody2D rb;

    GameManager gameManager;

    //ObserverDesign Patern
    public event EventHandler eventHandler;
    // Start is called before the first frame update
    void Start()
    {
        //Components
        rb = GetComponent<Rigidbody2D>();

        //Stats
        rb.mass = mass;

        gameManager = GameManager.Instance;
        if(gameManager.Equals(null))
        {
            Debug.Log("Game Manager is null");
        }
        gameManager.OnCodeCreation(this.gameObject);
    }

    private void Awake()
    {
        //gameManager = GameManager.Instance;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        var tag = collision.gameObject.tag;

        switch (tag)
        {
            case "Player":
                Die();
                break;
            case "Projectile":
                Projectile proj = collision.gameObject.GetComponent<Projectile>();
                life -= proj.damage;
                if (life <= 0f)
                {
                    Die();
                }
                break;
        }
    }

    private void Die()
    {
        //Notify projectile of death
        //NotifyOfDeath();
        Debug.Log(gameManager);
        gameManager.OnCodeDead(this.gameObject);

        Destroy(this.gameObject);
    }

    //Observer Design Pattern
    public void NotifyOfDeath() 
    {
        /*if(eventHandler != null)
        {
            eventHandler(this, EventArgs.Empty);
        }*/
    }
}
