using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SampleScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            PopupManager.instance.ShowOrEnqueue(new PopupManager.PopupInfo()
            {
                _popupType = PopupManager.PopupTypes.Default,
                title = "�T���v���^�C�g��",
                messageBody = "������"
            });
        }

        if (Input.GetKeyDown(KeyCode.X))
        {
            PopupManager.instance.ShowOrEnqueue(new PopupManager.PopupInfo()
            {
                _popupType = PopupManager.PopupTypes.Default,
                title = "�T���v���^�C�g��2",
                messageBody = "������������"
            });
        }

        if (Input.GetKeyDown(KeyCode.W))
        {
            PopupManager.instance.ShowOrEnqueue(new PopupManager.PopupInfo()
            {
                _popupType = PopupManager.PopupTypes.WithButton,
                title = "�T���v���^�C�g��(Button)",
                messageBody = "�ǂ��炩��I�Ԃ�Debug�ɑI�񂾌��ʂ��o��",
                confirmCallback = () => { Debug.Log("OK"); },
                cancelCallback = () => { Debug.Log("Cancel"); }
            }) ;
        }
    }
}
