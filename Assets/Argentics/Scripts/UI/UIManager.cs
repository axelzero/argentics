using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets._2D;

namespace GameUI
{
    public class UIManager : MonoBehaviour
    {
        public static UIManager instance = null;
        [SerializeField] private LifePoints lifePoints;
        [SerializeField] private GameOver gameOver;
        [SerializeField] private Restarter restarter;
        

        #region PROPERTYS
        public LifePoints LifePoints => lifePoints;
        public GameOver GameOver => gameOver;
        public Restarter Restarter => restarter;
        #endregion

        void Awake()
        {
            if (instance == null)
            { 
                instance = this; 
            }
            else if (instance == this)
            { 
                Destroy(gameObject);
            }

            DontDestroyOnLoad(gameObject);

            InitializeManager();
        }

        private void InitializeManager()
        {
            lifePoints.InitUI();
            gameOver.Init();
        }
    }
}