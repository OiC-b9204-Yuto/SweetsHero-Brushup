using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealthBar : MonoBehaviour
{
    [SerializeField] private EnemyBase enemy;
    [SerializeField] private Canvas healthBar;
    [SerializeField] private Image healthBarImage;

    void Awake()
    {
        //Health_Bar_Image = HealthBar.transform.GetChild(0).GetComponent<Image>();
        //Health_Bar_BG_Image = HealthBar.transform.GetChild(0).GetComponent<Image>();
        healthBar.gameObject.SetActive(false);
        enemy.OnCurrentHealthChanged += () => HealthBarUpdate();
    }

    void Update()
    {
        if (healthBar.enabled)
        {
            healthBar.transform.LookAt(Camera.main.transform);
        }
    }

    void HealthBarUpdate()
    {
        healthBarImage.fillAmount = (float)enemy.CurrentHealth / enemy.MaxHealth;
        if (healthBarImage.fillAmount <= 0.0f)
        {
            healthBar.gameObject.SetActive(false);
        }
        else if (healthBarImage.fillAmount < 1)
        {
            healthBar.gameObject.SetActive(true);
        }
    }
}
