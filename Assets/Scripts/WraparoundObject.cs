using System;
using UnityEngine;

namespace DefaultNamespace
{
    
    public class WraparoundObject : MonoBehaviour
    {

        public float radius;
        private Collider2D _mine;

        private void Start()
        {
            this._mine = this.GetComponent<Collider2D>();
        }

        public static Vector2 Rotate(Vector2 vec, float rotation)
        {
            var f = Mathf.Cos(rotation);
            var f1 = Mathf.Sin(rotation);
            var d0 = vec.x * (float)f + vec.y * (float)f1;
            var d2 = vec.y * (float)f - vec.x * (float)f1;
            return new Vector2(d0, d2);
        }
        
        public void Update()
        {
            //if (other == this.zone)
            {
                Vector3 pos = this.transform.position;
                Bounds mineBounds = this._mine.bounds;
                if (Vector2.Distance(pos, Vector2.zero) > this.radius)
                {
                    pos = Rotate(pos.normalized * (this.radius - 0.1F), Mathf.Deg2Rad * 180);
                }

                /*Bounds zoneBounds = this.zone.bounds;
                if (pos.y > zoneBounds.max.y)
                {
                    pos.y = (zoneBounds.min.y + 0.1F);
                }
                else if (pos.y < zoneBounds.min.y)
                {
                    pos.y = zoneBounds.max.y - 0.1F;
                }
                
                if (pos.x > zoneBounds.max.x)
                {
                    pos.x = (zoneBounds.min.x + 0.1F);
                }
                else if (pos.x < zoneBounds.min.x)
                {
                    pos.x = zoneBounds.max.x - 0.1F;
                }*/

                this.transform.position = pos;
            }
        }
    }
}