using System;
using Unity.VisualScripting;
using UnityEngine;

namespace DefaultNamespace
{
    public class Bullet : MonoBehaviour
    {
        
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
            this._rigidbody.velocity = this.transform.TransformVector(new Vector3(0, 1)) * 30;
            if (Time.time - this._timeCreated > 3)
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
            WraparoundObject wraparoundObject = obj.AddComponent<WraparoundObject>();
            wraparoundObject.zone = launcher.GetComponent<WraparoundObject>().zone;
            return b;
        }
        
    }
}