using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum tutorialFlagType
{
    Heal,
    Ammo,
    Key,
    Enemy
}

public class TutorialSystem : MonoBehaviour
{
    [SerializeField] int keyCounter = 3;
    [SerializeField] OR_SceneManager sceneManager;

    public void EventAction(tutorialFlagType type)
    {
        switch (type)
        {
            case tutorialFlagType.Heal:
                HealItem();
                break;
            case tutorialFlagType.Ammo:
                AmmoRecovItem();
                break;
            case tutorialFlagType.Key:
                Key();
                break;
            case tutorialFlagType.Enemy:
                Enemy();
                break;
            default:
                break;
        }
    }

    private void HealItem()
    {

    }

    private void AmmoRecovItem()
    {

    }

    private void Key()
    {
        keyCounter--;

    }

    private void Enemy()
    {
        //ステージセレクト実装ならばtitleへ　そうでなければstage1へ
        sceneManager.SceneName = "";
        sceneManager.NextSceneLoad();
    }
}
