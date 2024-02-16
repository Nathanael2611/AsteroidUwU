using System;
using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using Unity.VisualScripting;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class Rocket : MonoBehaviour
{

    [Header("Attributes")] public float speed = 1F;

    public GameObject missile;
    
    [Header("Physics")] public float drag = 1F;
    public float angularDrag = 1F;
    public float mass = 1F;

    
    
    private Rigidbody2D _rigidBody;

    private bool _needShoot = false;
    
    private void Awake()
    {
        this._rigidBody = this.AddComponent<Rigidbody2D>();
        this._rigidBody.gravityScale = 0;
        this._rigidBody.mass = this.mass;
        this._rigidBody.drag = this.drag;
        this._rigidBody.angularDrag = this.angularDrag;
    }
    
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            this._needShoot = true;
        }
    }

    private void FixedUpdate()
    {
        this._rigidBody.AddRelativeForce(new Vector2(0, Input.GetAxis("Vertical") * this.speed));
        this._rigidBody.AddTorque(-Input.GetAxis("Horizontal") * this.speed);

        if (this._needShoot)
        {
            Bullet.Initialize(this, this.missile);
            this._needShoot = false;
        }
    }

    public Rigidbody2D GetRigidBody()
    {
        return this._rigidBody;
    }
}
