using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("PlayerPrefs names")] 
    public string positionX = "PositionX";
    public string positionY = "PositionY";
    
    [Header("Moving properties")]
    public float speed = 4;
    public float forceJump = 200;

    [Header("GroundChecker")] 
    public Transform groundChecker;
    public float radiusCheck = 2;
    public LayerMask whatIsGround;

    public ParticleSystem particleSystem;
    public float timeParticleEnable = 2;

    private Rigidbody2D _rigidbody;
    private bool _grounded = false;
    private Animator _animator;

    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
        
        if (!PlayerPrefs.HasKey(positionX)) PlayerPrefs.SetFloat(positionX, transform.position.x);
        if (!PlayerPrefs.HasKey(positionY)) PlayerPrefs.SetFloat(positionY, transform.position.y);

        transform.position = new Vector2(PlayerPrefs.GetFloat(positionX), PlayerPrefs.GetFloat(positionY));
    }

    private void FixedUpdate()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(groundChecker.position, radiusCheck, whatIsGround);

        _grounded = false;
        for (int i = 0; i < colliders.Length; i++)
        {
            if (colliders[i].gameObject != gameObject)
            {
                _grounded = true;
            }
        }
    }

    public void Move(float move, bool jump)
    {
        _animator.SetBool("isRunning", move != 0);
        _animator.SetBool("Jump", jump);
        
        if (move < 0) FlipToLeft(); else if (move > 0) FlipToRight();
        
        _rigidbody.velocity = new Vector2(speed * move, _rigidbody.velocity.y);
        
        if (jump && _grounded) Jump();
    }

    public void SaveGame()
    {
        PlayerPrefs.SetFloat(positionX, transform.position.x);
        PlayerPrefs.SetFloat(positionY, transform.position.y);
    }

    public void Jump()
    {
        _rigidbody.AddForce(new Vector2(0, forceJump));
        StartCoroutine(StartCooldown());
    }

    private void FlipToLeft()
    {
        transform.localScale = new Vector3(-Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
    }
    
    private void FlipToRight()
    {
        transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
    }

    private IEnumerator StartCooldown()
    {
        particleSystem.Play();
        yield return new WaitForSeconds(timeParticleEnable);
        particleSystem.Stop();
    }
}
