using System;
using UnityEngine;

namespace DefaultNamespace
{
    
    public class WraparoundObject : MonoBehaviour
    {

        public BoxCollider2D zone;
        private Collider2D _mine;

        private void Start()
        {
            this._mine = this.GetComponent<Collider2D>();
        }

        public void OnTriggerExit2D(Collider2D other)
        {
            if (other == this.zone)
            {
                Vector3 pos = this.transform.position;
                Bounds zoneBounds = this.zone.bounds;
                Bounds mineBounds = this._mine.bounds;
                if (pos.y > zoneBounds.max.y)
                {
                    pos.y = (zoneBounds.min.y -  mineBounds.extents.y);
                }
                else if (pos.y < zoneBounds.min.y)
                {
                    pos.y = zoneBounds.max.y + mineBounds.extents.y;
                }
                
                if (pos.x > zoneBounds.max.x)
                {
                    pos.x = (zoneBounds.min.x -  mineBounds.extents.x);
                }
                else if (pos.x < zoneBounds.min.x)
                {
                    pos.x = zoneBounds.max.x + mineBounds.extents.x;
                }

                this.transform.position = pos;
            }
        }
    }
}