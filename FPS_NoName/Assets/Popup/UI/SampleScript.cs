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
                title = "サンプルタイトル",
                messageBody = "お試し"
            });
        }

        if (Input.GetKeyDown(KeyCode.X))
        {
            PopupManager.instance.ShowOrEnqueue(new PopupManager.PopupInfo()
            {
                _popupType = PopupManager.PopupTypes.Default,
                title = "サンプルタイトル2",
                messageBody = "お試しお試し"
            });
        }

        if (Input.GetKeyDown(KeyCode.W))
        {
            PopupManager.instance.ShowOrEnqueue(new PopupManager.PopupInfo()
            {
                _popupType = PopupManager.PopupTypes.WithButton,
                title = "サンプルタイトル(Button)",
                messageBody = "どちらかを選ぶとDebugに選んだ結果が出る",
                confirmCallback = () => { Debug.Log("OK"); },
                cancelCallback = () => { Debug.Log("Cancel"); }
            }) ;
        }
    }
}
