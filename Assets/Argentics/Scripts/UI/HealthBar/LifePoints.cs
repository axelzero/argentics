using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Argentics._2D;

namespace GameUI
{
    [System.Serializable]
    public class LifePoints : MonoBehaviour
    {
        [SerializeField] private List<Image> imgLifePoints;
        [SerializeField] private Color colorLife;
        [SerializeField] private Color colorLifeDamage;
        [SerializeField] private PlatformerCharacter player;
                         private int lifePoints = 0;

        public void InitUI()
        {
            int lifePointsCount = imgLifePoints.Count;
            for (int i = 0; i < lifePointsCount; i++)
            {
                imgLifePoints[i].color = colorLife;
            }
            //lifePoints = player.TakeLifePoints();
        }

        public void TakeDamageUI()
        {
            lifePoints = player.TakeLifePoints();

            imgLifePoints[lifePoints].color = colorLifeDamage;


            if (lifePoints <= 0)
            {
                player.Dying();
            }
        }
        public void TakeHealth()
        {
            lifePoints = player.TakeLifePoints();
            for (int i = 0; i < lifePoints; i++)
            {
                imgLifePoints[lifePoints - 1].color = colorLife;
            }
        }
    }
}