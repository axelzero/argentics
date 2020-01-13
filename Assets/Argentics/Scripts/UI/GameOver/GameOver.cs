using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace GameUI
{
    public class GameOver : MonoBehaviour
    {
        [SerializeField] private Image imgBackground;
        [SerializeField] private TextMeshProUGUI txtGameOver;

        public void Init()
        {
            imgBackground.gameObject.SetActive(false);
            txtGameOver.gameObject.SetActive(false);
        }
        public void GameOverUI()
        {
            StartCoroutine(SlowDark());
        }
        private IEnumerator SlowDark()
        {
            var tempColor = imgBackground.color;
            float step = 0f;
            imgBackground.gameObject.SetActive(true);
            while (step <= 1)
            {
                step += 0.01f;
                tempColor.a = step;
                imgBackground.color = tempColor;
                yield return null;
            }
            txtGameOver.gameObject.SetActive(true);
            yield return new WaitForSeconds(1.5f);
            UIManager.instance.Restarter.Restart();
        }
    }
}