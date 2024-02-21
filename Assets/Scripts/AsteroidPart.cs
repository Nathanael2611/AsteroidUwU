using System;
using System.Numerics;
using DG.Tweening;
using TreeEditor;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering.VirtualTexturing;
using Random = Unity.Mathematics.Random;
using Vector2 = UnityEngine.Vector2;
using Vector3 = UnityEngine.Vector3;

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
                this._wraparoundObject.radius = 10F;
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
                        component.velocity = WraparoundObject.Rotate(new Vector2(1, 0), Mathf.Deg2Rad * (UnityEngine.Random.Range(0, 360)));
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


        public static GameObject NewAsteroidPart()
        {
            GameObject container = new GameObject();
            AsteroidPart parent = container.AddComponent<AsteroidPart>();

            int n = UnityEngine.Random.Range(2, 4);
            Vector2 lastTranslate = Vector2.zero;

            for (int i = 0; i < n; i++)
            {
                GameObject spritePart = GameObject.Instantiate(Caches.PrefabCache.Get("Prefabs/Asteroid"), parent.transform, true);
                //spritePart.transform.position = new Vector3();
                Vector2 pos = new Vector2(1, 0);
                if (lastTranslate == Vector2.zero)
                {
                    pos = Vector2.zero;
                }
                else
                {
                    pos = WraparoundObject.Rotate(pos, UnityEngine.Random.Range(0, 360) * Mathf.Deg2Rad);
                }

                spritePart.transform.Translate(lastTranslate + pos);
                lastTranslate += pos;
                AsteroidPart asteroidPart = spritePart.GetComponent<AsteroidPart>();
                asteroidPart.parent = parent;
            }
            
            return container;
        }
        
        
    }
}