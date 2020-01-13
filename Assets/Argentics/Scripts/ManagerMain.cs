using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Argentics._2D;

namespace Game
{
    public class ManagerMain : MonoBehaviour
    {
        public static ManagerMain instance = null;
        [SerializeField] private PlatformerCharacter player;
        [SerializeField] private GameObject fireBulletPrefab;

        #region PROPERTYS
        public PlatformerCharacter Player => player;
        public GameObject FireBulletPrefab => fireBulletPrefab;
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

            //InitializeManager();
        }
        //private void InitializeManager()
        //{
            
        //}
    }
}
