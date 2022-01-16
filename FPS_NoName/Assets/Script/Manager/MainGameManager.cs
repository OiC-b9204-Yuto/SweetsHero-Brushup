using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MainGameManage
{
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
