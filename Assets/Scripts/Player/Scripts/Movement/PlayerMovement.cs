using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private PlayerController _playerController;
    public Joystick joystick;
    private float _move;
    private bool _jump = false;

    private void Start()
    {
        _playerController = GetComponent<PlayerController>();
    }

    private void Update()
    {
        _move = joystick.Horizontal; //Input.GetAxis("Horizontal");
        //if (Input.GetKeyDown(KeyCode.Space)) _jump = true;
        //if (joystick.Vertical > 0) _jump = true;
        
        _playerController.Move(_move, _jump);

        _jump = false;
    }
}
