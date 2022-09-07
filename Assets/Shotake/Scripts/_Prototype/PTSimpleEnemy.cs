
using UnityEngine;

namespace Shotake
{
    class PTSimpleEnemy : MonoBehaviour
    {
        public GameObject player;
        public float speed = 3;

        void Update()
        {
            if (player)
            {
                transform.Translate(speed * Time.deltaTime * (player.transform.position - transform.position).normalized);
                transform.position = new Vector3(transform.position.x, 0, transform.position.z);
                
                //GetComponent<Rigidbody>().MovePosition(transform.position + speed * Time.deltaTime * (player.transform.position - transform.position).normalized);
            }
        }
        
        private void OnTriggerEnter(Collider other)
        {
            var e = other.GetComponent<PTSimpleEnemy>(); 
            if (e)
            {
                var movedir = (other.transform.position - transform.position).normalized;
                if (other.GetInstanceID() > GetInstanceID())
                {
                    movedir.x += 0.1f;
                }

                movedir *= Time.deltaTime * speed;
                e.transform.Translate(movedir);
            }
        }
    }
}
