using System.Collections;
using System.Collections.Generic;
using UnityEngine;

    public class MainGameManager : MonoBehaviour
    {
        [SerializeField] private bool maingame_IsStartAnimation; //ゲーム開始時のアニメーション用
        [SerializeField] private bool maingame_IsStartGame;      //ゲーム開始合図用
        [SerializeField] private bool maingame_GameProgress;     //ゲーム進行中用
        [SerializeField] private bool maingame_IsBossBattle;     //ボスバトル中用
        [SerializeField] private bool maingame_IsPause;          //ゲームポーズ中   
        [SerializeField] private bool maingame_IsGameOver;       //ゲームオーバー用
        [SerializeField] private bool maingame_IsGameClear;      //ゲームクリア用
        [SerializeField] private bool maingame_IsImmortal;       //ゲーム中に無敵状態にする用

        public bool MainGame_IsStartAnimation { get { return maingame_IsStartAnimation; } set { maingame_IsStartAnimation = value; } }
        public bool MainGame_IsStartGame { get { return maingame_IsStartGame; } set { maingame_IsStartGame = value; } }
        public bool MainGame_GameProgress { get { return maingame_GameProgress; } set { maingame_GameProgress = value; } }
        public bool MainGame_IsBossBattle { get { return maingame_IsBossBattle; } set { maingame_IsBossBattle = value; } }
        public bool MainGame_IsPause { get { return maingame_IsPause; } set { maingame_IsPause = value; } }
        public bool MainGame_IsGameOver { get { return maingame_IsGameOver; } set { maingame_IsGameOver = value; } }
        public bool MainGame_IsGameClear {  get { return maingame_IsGameClear; } set { maingame_IsGameClear = value; } }

        private void Awake()
        {
            maingame_IsStartAnimation = true;
        }

        private void Update()
        {
            CheckGameState();
        }

        void CheckGameState()
        {
            if (!maingame_IsStartAnimation && !maingame_GameProgress)
            {
                maingame_IsStartGame = true;
                maingame_GameProgress = true;
            }

            if (maingame_IsPause)
            {
                Time.timeScale = 0.0f;
            }
            else
            {
                Time.timeScale = 1.0f;
            }

        }
    }

