using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealthBar : MonoBehaviour
{
    [SerializeField] private BaseEnemy baseEnemy;
    [SerializeField] private EnemyBase enemy;
    [SerializeField] private Canvas HealthBar;
    [SerializeField] private Image Health_Bar_Image;
    [SerializeField] private Image Health_Bar_BG_Image;

    void Awake()
    {
        //Health_Bar_Image = HealthBar.transform.GetChild(0).GetComponent<Image>();
        //Health_Bar_BG_Image = HealthBar.transform.GetChild(0).GetComponent<Image>();
        HealthBar.gameObject.SetActive(false);
        enemy.OnCurrentHealthChanged += () => HealthBarUpdate();
    }

    void HealthBarUpdate()
    {
        Health_Bar_Image.fillAmount = (float)baseEnemy.CurrentHealth / baseEnemy.MaxHealth;
        Health_Bar_Image.rectTransform.LookAt(Camera.main.transform);
        Health_Bar_BG_Image.rectTransform.LookAt(Camera.main.transform);
        if (Health_Bar_Image.fillAmount <= 0.0f)
        {
            HealthBar.gameObject.SetActive(false);
        }
        else if (Health_Bar_Image.fillAmount < 1)
        {
            HealthBar.gameObject.SetActive(true);
        }
    }
}
