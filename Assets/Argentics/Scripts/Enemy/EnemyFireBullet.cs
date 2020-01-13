using Argentics._2D;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game
{
    public class EnemyFireBullet : MonoBehaviour
    {
        [SerializeField] private GameObject explosion;
        [SerializeField] private int damage = 1;
        private PlatformerCharacter player;
        private Rigidbody rb;
        private Vector3 distance;

        private Vector3 leftLook = new Vector3(0f, 0f, 0f);
        private Vector3 rightLook = new Vector3(0f, 180f, 0f);

        private Vector3 leftVec = new Vector3(-2f, 0f, 0f);
        private Vector3 rightVec = new Vector3(2f, 0f, 0f);

        private bool left = true;

        private Coroutine coroutine = null;
        private float timeLife = 4f;

        private void OnEnable()
        {
            if (player == null)
            {
                player = ManagerMain.instance.Player;
            }
            if (rb == null)
            {
                rb = gameObject.GetComponent<Rigidbody>();
            }
            coroutine = StartCoroutine(SetActiveFalse());
        }
        private void OnDisable()
        {
            StopCoroutine(coroutine);
            coroutine = null;
        }
        private IEnumerator SetActiveFalse()
        {
            distance = player.gameObject.transform.position - gameObject.transform.position;

            if (distance.x >= 0)
            {
                transform.eulerAngles = rightLook;
                left = false;
            }
            else
            {
                transform.eulerAngles = leftLook;
                left = true;
            }

            rb.velocity = left ? leftVec : rightVec;

            yield return new WaitForSeconds(timeLife);

            GameObject explo = Instantiate(explosion, transform.position, Quaternion.identity, transform.parent);
            explo.SetActive(true);
            gameObject.SetActive(false);
        }
        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.tag == "Player")
            {
                GameObject explo = Instantiate(explosion, transform.position, Quaternion.identity, transform.parent);
                explo.SetActive(true);
                player.TakeDamage(damage);
                gameObject.SetActive(false);
            }
            else if(other.gameObject.tag != "Enemy")
            {
                GameObject explo = Instantiate(explosion, transform.position, Quaternion.identity, transform.parent);
                explo.SetActive(true);
                gameObject.SetActive(false);
            }
        }
    }
}