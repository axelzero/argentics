using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Argentics._2D;

namespace Game
{
    public class AddHealthProps : MonoBehaviour
    {
        [SerializeField] int addHealth = 1;

        private void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject.tag == "Player")
            {
                var player = collision.gameObject.GetComponent<PlatformerCharacter>();
                player.AddHealth(addHealth);
                Destroy(gameObject);
            }
        }
    }
}