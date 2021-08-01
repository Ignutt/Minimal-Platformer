using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    /*public Image image;
    public float _health = 100;

    private void Update()
    {
        image.fillAmount = _health / 100;
    }

    public void TakeDamage(int value)
    {
        _health -= value;
        if (_health <= 0) Death();
    }*/

    public GameObject healthBar;
    public GameObject heart;
    private int _health = 4;
    private List<GameObject> _hearts = new List<GameObject>();

    private void Start()
    {
        InitializeBar();
    }

    private void InitializeBar()
    {
        for (int i = 0; i < _health; i++)
        {
            GameObject newHeart = Instantiate(heart, healthBar.transform);
            _hearts.Add(newHeart);
        }
    }

    private void DecreaseBar(int value)
    {
        for (int i = 0; i < value; i++)
        {
            if (!_hearts.Any()) return;
            Destroy(_hearts[0]);
            _hearts.RemoveAt(0);
        }
    }

    public void IncreaseBar()
    {
        _health++;
        GameObject newHeart = Instantiate(heart, healthBar.transform);
        _hearts.Add(newHeart);
    }
    
    public void TakeDamage(int value)
    {
        _health -= value;
        if (_health <= 0) Death();
        DecreaseBar(value);
        
    }
    
    private void Death()
    {
        GameObject.FindWithTag("GameManager").GetComponent<GameManager>().EndGame();
    }
}
