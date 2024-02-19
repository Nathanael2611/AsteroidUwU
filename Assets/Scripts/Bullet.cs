using System;
using Unity.VisualScripting;
using UnityEngine;
using Random = UnityEngine.Random;

namespace DefaultNamespace
{
    public class Bullet : MonoBehaviour
    {

        public float speed = 1F;
        public float timeBeforeDestroy = 1F;
        
        public Vector2 direction = Vector2.zero;

        private Rigidbody2D _rigidbody;
        private float _timeCreated = 0F;
        
        public void Start()
        {
            this._rigidbody = this.GetComponent<Rigidbody2D>();
            this._timeCreated = Time.time;
        }

        public void Update()
        {
            this._rigidbody.velocity = this.transform.TransformVector(new Vector3(0, 1)) * this.speed;
            if (Time.time - this._timeCreated > this.timeBeforeDestroy)
            {
                GameObject.Destroy(this.gameObject);
            }
        }

        public static Bullet Initialize(Rocket launcher, GameObject prefab)
        {
            GameObject obj = GameObject.Instantiate(prefab);
            obj.transform.position = launcher.transform.position + launcher.transform.up.normalized *
                (launcher.GetComponent<Collider2D>().bounds.extents.y + obj.GetComponent<Collider2D>().bounds.extents.y + 0.1F);
            Bullet b = obj.AddComponent<Bullet>();
            Rigidbody2D r = b.GetComponent<Rigidbody2D>();
            r.rotation = launcher.GetRigidBody().rotation;
            //r.totalTorque = launcher.GetRigidBody().totalTorque * 0.1F;
            //WraparoundObject wraparoundObject = obj.AddComponent<WraparoundObject>();
            //wraparoundObject.zone = launcher.GetComponent<WraparoundObject>().zone;
            Physics2D.IgnoreCollision(launcher.GetComponent<Collider2D>(), obj.GetComponent<Collider2D>());
            return b;
        }
        
    }
}