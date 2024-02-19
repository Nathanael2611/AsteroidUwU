using System;
using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using DG.Tweening;
using Unity.VisualScripting;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class Rocket : MonoBehaviour
{

    [Header("Attributes")] public float speed = 1F;
    public float turningSpeed = 1F;

    public GameObject missile;
    
    [Header("Physics")] public float drag = 1F;
    public float angularDrag = 1F;
    public float mass = 1F;

    [Header("Bullets")] public float bulletSpeed = 20;
    public float livingBulletTime = 1F;
    
    private Rigidbody2D _rigidBody;
    private Unity.Mathematics.Random rand = new(382);

    private bool _needShoot = false;
    private float _lastShot = 0L;
    
    private void Awake()
    {
        this._rigidBody = this.AddComponent<Rigidbody2D>();
    }
    
    private void Update()
    {
        this._needShoot = Input.GetKey(KeyCode.Space);
        this._rigidBody.gravityScale = 0;
        this._rigidBody.mass = this.mass;
        this._rigidBody.drag = this.drag;
        this._rigidBody.angularDrag = this.angularDrag;
    }

    private Tweener _tweener = null;
    
    private void FixedUpdate()
    {
        this._rigidBody.AddRelativeForce(new Vector2(0, Input.GetAxis("Vertical") * this.speed));
        this._rigidBody.AddTorque(-Input.GetAxis("Horizontal") * this.turningSpeed);

        if (this._needShoot && Time.time - this._lastShot > 0.15f)
        {
            this._lastShot = Time.time;
            Bullet initialize = Bullet.Initialize(this, this.missile);
            initialize.GetComponent<Rigidbody2D>().rotation += this.rand.NextFloat(-10, 10);
            initialize.speed = this.bulletSpeed;
            initialize.timeBeforeDestroy = this.livingBulletTime;
            this.transform.DOShakeScale(0.1F);
            if (this._tweener == null || !this._tweener.IsActive() || this._tweener.IsComplete())
            {
                this._tweener = Utils.GetCameraGameObject().transform.DOShakePosition(0.4F, 0.1F);
            }
            //this._needShoot = false;
        }
    }

    public Rigidbody2D GetRigidBody()
    {
        return this._rigidBody;
    }
}
