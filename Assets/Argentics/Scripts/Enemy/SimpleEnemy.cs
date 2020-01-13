using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Argentics._2D;

namespace Game
{
    public class SimpleEnemy : MonoBehaviour
    {
        private PlatformerCharacter player;
        [SerializeField] private int damage = 1;
        [SerializeField] private bool isEnemyStatic = false;
        [SerializeField] private float distanceToAttack_X = 5f;
        [SerializeField] private float distanceToAttack_Y = 1.5f;
                         private Vector3 distance;
                         private float distanceX;
                         private float distanceY;
        [SerializeField] private Transform leftPos;
        [SerializeField] private Transform rightPos;
                         private Rigidbody rb;
                         private Transform transform;
                         private Vector3 leftLook = new Vector3(0f, -90f, 0f);
                         private Vector3 rightLook = new Vector3(0f, 90f, 0f);
                         private bool left = true;
                         private Vector3 leftVec = new Vector3(-2f,0f,0f);
                         private Vector3 rightVec = new Vector3(2f,0f,0f);

                         private Animator anim;

        [SerializeField] private List<GameObject> poolBullets = new List<GameObject>();
                         private Coroutine fireCor = null;

        private void Start()
        {
            anim = gameObject.GetComponent<Animator>();
            player = ManagerMain.instance.Player;
            rb = GetComponent<Rigidbody>();
            transform = gameObject.GetComponent<Transform>();
            transform.rotation = new Quaternion(0f, 0f, 0f, 0f);
        }
        private void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject.tag == "Player")
            {
                player.TakeDamage(damage);
            }
        }
        void FixedUpdate()
        {
            Action();
        }
        private void Action()
        {
            if (isEnemyStatic) return;

            distance = player.gameObject.transform.position - gameObject.transform.position;
            distanceX = Mathf.Abs(distance.x);
            distanceY = Mathf.Abs(distance.y);


            if (distanceX < distanceToAttack_X && distanceY < distanceToAttack_Y)
            {
                if (distance.x >= 0)
                {
                    transform.eulerAngles = rightLook;
                }
                else
                {
                    transform.eulerAngles = leftLook;
                }

                if (fireCor == null)
                {
                    fireCor = StartCoroutine(Fire());
                }
                
                anim.SetBool("Attack", true);
                return;
            }
            else
            {
                anim.SetBool("Attack", false);
            }

            LookAtTarget();

            transform.eulerAngles = left ?  leftLook : rightLook;
            rb.velocity = left ? leftVec : rightVec;
        }
        private void LookAtTarget()
        {
            if (transform.position.x <= leftPos.position.x)
            {
                left = false;
            }
            else if (transform.position.x >= rightPos.position.x)
            {
                left = true;
            }
        }
        private IEnumerator Fire()
        {
            AnimatorClipInfo[] clipInfo = anim.GetCurrentAnimatorClipInfo(0);
            float clipDuration = clipInfo.Length;
            CreateFireBullet();
            yield return new WaitForSeconds(clipDuration);
            fireCor = null;
        }

        private void CreateFireBullet()
        {
            bool findedInPool = false;
            int bulletsCount = poolBullets.Count;
            Vector3 modify = new Vector3(0f,0.5f);
            if (bulletsCount <= 0)
            {
                GameObject fireB = Instantiate(ManagerMain.instance.FireBulletPrefab, transform.position + modify, Quaternion.identity, transform.parent);
                poolBullets.Add(fireB);
                return;
            }
            for (int i = 0; i < bulletsCount; i++)
            {
                if (poolBullets[i].activeSelf == false)
                {
                    poolBullets[i].transform.position = gameObject.transform.position + modify;
                    poolBullets[i].SetActive(true);
                    findedInPool = true;
                    break;
                }
            }
            if (!findedInPool)
            {
                GameObject fireB = Instantiate(ManagerMain.instance.FireBulletPrefab, transform.position + modify, Quaternion.identity, transform.parent);
                poolBullets.Add(fireB);
            }
        }
    }
}