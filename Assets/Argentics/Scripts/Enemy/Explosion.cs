using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game
{
    public class Explosion : MonoBehaviour
    {

        private float expTime = 0f;
        private void OnEnable()
        {
            StartCoroutine(Explo());
        }
        private IEnumerator Explo()
        {
            expTime = gameObject.GetComponent<ParticleSystem>().main.duration;
            yield return new WaitForSeconds(expTime);
            Destroy(gameObject);
        }
    }
}