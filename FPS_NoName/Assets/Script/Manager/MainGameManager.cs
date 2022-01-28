using System.Collections;
using System.Collections.Generic;
using UnityEngine;

    public class MainGameManager : MonoBehaviour
    {
        [SerializeField] private bool maingame_IsStartAnimation; //�Q�[���J�n���̃A�j���[�V�����p
        [SerializeField] private bool maingame_IsStartGame;      //�Q�[���J�n���}�p
        [SerializeField] private bool maingame_GameProgress;     //�Q�[���i�s���p
        [SerializeField] private bool maingame_IsBossBattle;     //�{�X�o�g�����p
        [SerializeField] private bool maingame_IsPause;          //�Q�[���|�[�Y��   
        [SerializeField] private bool maingame_IsGameOver;       //�Q�[���I�[�o�[�p
        [SerializeField] private bool maingame_IsGameClear;      //�Q�[���N���A�p
        [SerializeField] private bool maingame_IsImmortal;       //�Q�[�����ɖ��G��Ԃɂ���p

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

