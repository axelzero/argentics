using System;
using UnityEngine;
using GameUI;
using System.Collections.Generic;
using System.Collections;

namespace Argentics._2D
{
    public class PlatformerCharacter : MonoBehaviour
    {
        [SerializeField] private float m_MaxSpeed = 10f;                    // The fastest the player can travel in the x axis.
        [SerializeField] private float m_JumpForce = 400f;                  // Amount of force added when the player jumps.
        [SerializeField] private bool m_AirControl = false;                 // Whether or not a player can steer while jumping;
        [SerializeField] private LayerMask m_WhatIsGround;                  // A mask determining what is ground to the character

        [SerializeField]
        private Transform m_GroundCheck;    // A position marking where to check if the player is grounded.
        const float k_GroundedRadius = .2f; // Radius of the overlap circle to determine if grounded
        private bool m_Grounded;            // Whether or not the player is grounded.
        [SerializeField]
        private Transform m_CeilingCheck;   // A position marking where to check for ceilings
        const float k_CeilingRadius = .01f; // Radius of the overlap circle to determine if the player can stand up
        private Animator m_Anim;            // Reference to the player's animator component.
        private Rigidbody m_Rigidbody;
        private bool m_FacingRight = true;  // For determining which way the player is currently facing.

        private const int  lifepoinstStart = 3;
        [SerializeField] private int lifePoints = 3;

        private void Awake()
        {
            m_Anim = GetComponent<Animator>();
            m_Rigidbody = GetComponent<Rigidbody>();
            lifePoints = lifepoinstStart;
        }


        private void FixedUpdate()
        {
            m_Grounded = false;

            Collider[] colliders = Physics.OverlapSphere(m_GroundCheck.position, k_GroundedRadius, m_WhatIsGround);
            for (int i = 0; i < colliders.Length; i++)
            {
                if (colliders[i].gameObject != gameObject)
                    m_Grounded = true;
            }
            m_Anim.SetBool("Grounded", m_Grounded);
        }


        public void Move(float move, bool jump)
        {
            if (lifePoints <= 0) return;

            //only control the player if grounded or airControl is turned on
            if (m_Grounded || m_AirControl)
            {
                // Move the character
                m_Rigidbody.velocity = new Vector3(move*m_MaxSpeed, m_Rigidbody.velocity.y);
                m_Anim.SetFloat("MoveSpeed", Mathf.Abs(move));

                // If the input is moving the player right and the player is facing left...
                if (move > 0 && !m_FacingRight)
                {
                    // ... flip the player.
                    Flip();
                }
                    // Otherwise if the input is moving the player left and the player is facing right...
                else if (move < 0 && m_FacingRight)
                {
                    // ... flip the player.
                    Flip();
                }
            }
            // If the player should jump...
            if (m_Grounded && jump && m_Anim.GetBool("Grounded"))
            {
                // Add a vertical force to the player.
                m_Grounded = false;
                m_Anim.SetBool("Grounded", false);
                m_Anim.SetTrigger("Jump");
                m_Rigidbody.AddForce(new Vector3(0f, m_JumpForce));
            }
        }


        private void Flip()
        {
            // Switch the way the player is labelled as facing.
            m_FacingRight = !m_FacingRight;

            transform.rotation = Quaternion.Euler(0, 90 * (m_FacingRight ? 1 : -1), 0);
        }

        public void TakeDamage(int damage)
        {
            lifePoints -= damage;
            UIManager.instance.LifePoints.TakeDamageUI();
        }

        public int TakeLifePoints()
        {
            if (lifePoints < 0)
            {
                lifePoints = 0;
            }
            return lifePoints;
        }
        public void Dying()
        {
            StartCoroutine(Death());
        }
        private IEnumerator Death()
        {
            int waitTime = 5;
            m_Anim.SetBool("Death", true);
            yield return new WaitForSeconds(waitTime);
            UIManager.instance.GameOver.GameOverUI();
        }

        public void AddHealth(int addHealth)
        {
            if (lifePoints < lifepoinstStart)
            {
                lifePoints++;
                UIManager.instance.LifePoints.TakeHealth();
            }
        }
    }
}
