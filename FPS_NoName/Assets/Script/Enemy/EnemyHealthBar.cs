using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealthBar : MonoBehaviour
{
    [SerializeField] private BaseEnemy baseEnemy;
    [SerializeField] private Canvas HelthBar;
    private Image image;

    void Awake()
    {
        image = HelthBar.transform.GetChild(0).GetComponent<Image>();
        HelthBar.gameObject.SetActive(false);
    }

    void Update()
    {
        image.fillAmount = (float)baseEnemy.CurrentHealth / baseEnemy.MaxHealth;
        image.rectTransform.LookAt(Camera.main.transform);
        if (image.fillAmount < 1)
        {
            HelthBar.gameObject.SetActive(true);
        }
    }
}
