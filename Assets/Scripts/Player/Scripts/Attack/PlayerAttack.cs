using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    [Header("World objects")]
    public PlayerBullet playerBullet;
    public Transform muzzle;

    [Header("Attack properties")] 
    public float timeToNextAttack = 2f;

    private bool _canAttack = true;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            if (_canAttack) Attack();
        }
    }

    private void Attack()
    {
        PlayerBullet newBullet = Instantiate(playerBullet);
        newBullet.transform.position = muzzle.position;
        newBullet.speed = transform.localScale.x > 0 ? newBullet.speed : -newBullet.speed;

        StartCoroutine(AttackCooldown());
    }

    private IEnumerator AttackCooldown()
    {
        _canAttack = false;
        yield return new WaitForSeconds(timeToNextAttack);
        _canAttack = true;
    }
}
