using System;
using DG.Tweening;
using TreeEditor;
using Unity.VisualScripting;
using UnityEngine;

namespace DefaultNamespace
{
    
    [RequireComponent(typeof(Rigidbody2D))]
    public class AsteroidPart : MonoBehaviour
    {

        public AsteroidPart parent = null;
        private bool _deleteIfNoChildren;

        private WraparoundObject _wraparoundObject;
        private Rigidbody2D _rigidbody;

        private void Update()
        {
            Debug.Log(this._rigidbody.velocity);
            if (this.parent != null)
            {
                this._rigidbody.bodyType = RigidbodyType2D.Kinematic;
            }
            else
            {
                this._rigidbody.bodyType = RigidbodyType2D.Kinematic;
                this._rigidbody.velocity = this._rigidbody.velocity.normalized * 3;
            }
        }

        private void FixedUpdate()
        {
            
            if (this.parent == null && this._wraparoundObject == null)
            {
                if (this.GetComponent<Collider2D>() == null)
                {
                    CircleCollider2D circleCollider2D = this.AddComponent<CircleCollider2D>();
                    circleCollider2D.radius = 0.1F;
                }
                this._wraparoundObject = this.AddComponent<WraparoundObject>();
                this._wraparoundObject.zone = GameObject.FindGameObjectWithTag("Background").GetComponent<BoxCollider2D>();
            }

            if (this._wraparoundObject != null && this.parent != null)
            {
                GameObject.Destroy(this._wraparoundObject);
                this._wraparoundObject = null;
            }
            if (this._deleteIfNoChildren)
            {
                GameObject.Destroy(this.gameObject);
            }
        }

        public void tryDetach()
        {
            this.transform.DOShakeScale(0.1F);

            if (this.parent != null)
            {
                this.parent.tryDetach();
                return;
            }
            // TODO: détacher les childs, et retirer le parent de ceux cis les envoyer dans d'autres directions
            if (this.transform.childCount > 0)
            {
                for (int i = 0; i < this.transform.childCount; i++)
                {
                    Transform child = this.transform.GetChild(i);
                    Rigidbody2D component = child.GetComponent<Rigidbody2D>();
                    if (component)
                    {
                        component.bodyType = RigidbodyType2D.Dynamic;
                        component.velocity = new Vector3(UnityEngine.Random.Range(-1, 1), UnityEngine.Random.Range(-1, 1));
                    }

                    AsteroidPart part = child.GetComponent<AsteroidPart>();
                    if (part)
                    {
                        part.parent = null;
                    }
                }
                this.transform.DetachChildren();
                this._deleteIfNoChildren = true;
            }
            else
            {
                GameObject.Destroy(this.gameObject);
            }
        }

        private void Start()
        {
            AsteroidPart findObjectOfType = GameObject.FindObjectOfType<AsteroidPart>();
            Collider2D one = findObjectOfType.gameObject.GetComponent<Collider2D>();
            Collider2D mine = this.GetComponent<Collider2D>();
            if (mine != null)
            {
                mine.isTrigger = true;
            }
            if(one != null && mine != null) 
                Physics2D.IgnoreCollision(one, mine);
            this._rigidbody = this.GetComponent<Rigidbody2D>();
            this._rigidbody.velocity = new Vector3(UnityEngine.Random.Range(-1, 1), UnityEngine.Random.Range(-1, 1));

        }


        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.gameObject.GetComponent<Bullet>() != null)
            {
                GameObject.Destroy(other.gameObject);
                this.tryDetach();

            }        }

        private void OnCollisionEnter2D(Collision2D other)
        {
            if (other.gameObject.GetComponent<Bullet>() != null)
            {
                GameObject.Destroy(other.gameObject);
                this.tryDetach();

            }
        }
    }
}