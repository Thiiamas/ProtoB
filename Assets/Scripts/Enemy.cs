using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour, IEnemy
{
    //A FAIRE
    //FINIR RECUPERER LES KOCHLINES
    //FINIR EVENT (PUBLISHER = this ; Subscrisbers = tout les kochline de la structure)
    public class KochRefreshEventArgs : EventArgs
    {
        public float lerpAmount;
        public float generateMultiplier;

        public Material material;
        public Color color;
        public float emissionMultiplier;
    }

    //stats
    [Header("Stats")]
    public float life = 1f;
    public float mass = 1f;

    [Header("KochLine")]
    public float lerpAmount;
    public float generateMultiplier;

    public Material material;
    public Color color;
    public float emissionMultiplier;
    public bool _refresh;
    public EventHandler<KochRefreshEventArgs> RefreshEvent;


    


    //Components
    Rigidbody rb;

    public int tiersCount;
    GameManager gameManager;

    //Graphics
    EnemyVisual graphics;

    // Start is called before the first frame update
    void Start()
    {
        graphics= transform.GetChild(0).GetComponent<EnemyVisual>();
        tiersCount = graphics.tiersCount;
        life = (float)tiersCount;
        rb = GetComponent<Rigidbody>();

        //Stats
        rb.mass = mass;

        gameManager = GameManager.Instance;
        if (gameManager.Equals(null))
        {
            Debug.Log("Game Manager is null");
        }
        OnSpawn();
    }

    // Update is called once per frame
    void Update()
    {
        if(_refresh)
        {
            KochRefreshEventArgs args = new KochRefreshEventArgs
            {
                lerpAmount = lerpAmount,
                generateMultiplier = generateMultiplier,
                material = material,
                color = color,
                emissionMultiplier = emissionMultiplier
            };
            OnRefresh(args);
            _refresh = false;
        }
    }

    //a method to suscribe to the event
    public void SuscribeToRefreshEvent(EventHandler<KochRefreshEventArgs> method)
    {
        RefreshEvent += method;
    }
    void OnRefresh(KochRefreshEventArgs e)
    {
        EventHandler<KochRefreshEventArgs> handler = RefreshEvent;
        if (handler != null)
        {
            handler(this, e);
        }
    }
    //IEnemy
    public void OnSpawn()
    {
        gameManager.OnCodeCreation(gameObject);
    }
    public void OnDeath()
    {
        gameManager.OnCodeDead(gameObject);
    }

    public void OnPlayerColision()
    {
        throw new System.NotImplementedException();
    }

    public void OnProjectileColision()
    {
        life -= 1f;
        //DestroyTier
        if(life <= 0f)
        {
            OnDeath();
            Destroy(gameObject);
        }
    }


}
