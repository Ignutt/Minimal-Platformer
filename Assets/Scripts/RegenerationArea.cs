using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RegenerationArea : MonoBehaviour
{
    public float timeToNextHeal = 3;

    private bool _work = false;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            _work = true;
            StartCoroutine(HealCooldown(other.transform));
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            _work = false;
            StopCoroutine(HealCooldown(other.transform));
        }
    }

    private IEnumerator HealCooldown(Transform player)
    {
        if (_work)
        {
            yield return new WaitForSeconds(timeToNextHeal);
            if (_work) player.GetComponent<PlayerHealth>().IncreaseBar();
            if (_work) StartCoroutine(HealCooldown(player));
        }

    }
}
