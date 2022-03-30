using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GameProgressState
{
    Game_IsStartAnimation,
    Game_IsStartGame,
    Game_IsGameProgress,
    Game_IsPause,
    Game_IsGameOver,
    Game_IsGameClear
}
    public class MainGameManager : MonoBehaviour
    {
    public GameProgressState gameProgressState;
        private void Awake()
        {
        gameProgressState = GameProgressState.Game_IsStartGame;
        }

        private void Update()
        {
            CheckGameState();
        }

        void CheckGameState()
        {

            if (gameProgressState == GameProgressState.Game_IsPause)
            {
                Time.timeScale = 0.0f;
            }
            else
            {
                Time.timeScale = 1.0f;
            }

        }
    }

