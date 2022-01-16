using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MainGameManage
{
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

        public bool MainGame_IsPause { get { return maingame_IsPause; } set { value = maingame_IsPause; } }
        private void Awake()
        {
            DontDestroyOnLoad(this.gameObject);
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

            if (maingame_IsGameOver && maingame_IsGameClear && maingame_IsPause)
            {
                Time.timeScale = 0.0f;
            }
            else if (!maingame_IsGameOver && !maingame_IsGameClear && !maingame_IsPause)
            {
                Time.timeScale = 1.0f;
            }

        }
    }
}
