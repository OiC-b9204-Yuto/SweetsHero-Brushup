using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CallDestroy : MonoBehaviour
{
    [SerializeField] private TutorialSystem tutorialSystem;
    [SerializeField] private tutorialFlagType type;
    // Start is called before the first frame update

    private void OnDestroy()
    {
        tutorialSystem.EventAction(type);
    }
}
