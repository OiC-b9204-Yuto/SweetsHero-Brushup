using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class OR_SceneManager : MonoBehaviour
{
    //ASync
    private AsyncOperation async;           //AsyncOperation

    //String
    public string SceneName;      //�ǂݍ��ރV�[����

    //bool
    private bool isLoading;

    //GameObject
    [SerializeField] private GameObject LoadingUI;  //���[�f�B���OUI

    //Image
    [SerializeField] private Image LoadingBar;      //NowLoading�̃v���O���X�o�[�̎w��

    //Text
    [SerializeField] private Text ProgressText;     //�v���O���X�e�L�X�g

    private void Awake()
    {
        isLoading = false;
    }
    //���̃V�[���ֈړ�����V�X�e��
    //���̃V�[���ֈړ��������Ƃ��� NextSceneLoad()�֐������g�p���������B
    //�� �g���� ��
    //1 -  ���̃V�[���ɋ�̃Q�[���I�u�W�F�N�g��z�u���A���̃X�N���v�g��ݒ肵�܂��B
    //2 -  SceneName�ɓǂݍ��܂������V�[���̖��O����͂��܂��B
    //3 -  ���̃I�u�W�F�N�g���炱�̃X�N���v�g��ǂݍ���� �C�ӂ̃^�C�~���O�� NextSceneLoad�����s���܂�
    public void NextSceneLoad()
    {
        if (!isLoading)
        {
            LoadingUI.SetActive(true);       
            StartCoroutine("LoadAsyncSceneSystem");
            isLoading = true;
        }
    }


    //���[�f�B���O�V�X�e�� 
    IEnumerator LoadAsyncSceneSystem()
    {
        async = SceneManager.LoadSceneAsync(SceneName);
        async.allowSceneActivation = false;
        //�V�[�����ǂݏI���܂ő҂�
        while (async.progress < 0.9f)
        {
            ProgressText.text = (async.progress * 100).ToString("0") + "%";
            LoadingBar.fillAmount = async.progress;
            yield return null;
        }
        ProgressText.text = "100%";
        LoadingBar.fillAmount = 1.0f;
        yield return new WaitForSeconds(1.25f); //�҂�����
        async.allowSceneActivation = true;
    }
    void stopMusic()
    {
    }
}
