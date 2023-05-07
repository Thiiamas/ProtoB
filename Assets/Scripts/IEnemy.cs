using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IEnemy
{

    public void OnPlayerColision();
    public void OnProjectileColision();
    public void OnSpawn();
    public void OnDeath();


}
