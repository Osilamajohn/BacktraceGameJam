using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class HealthBar : MonoBehaviour
{

    Slider slider;
    
    // Start is called before the first frame update
    void Start()
    {
        slider = GetComponent<Slider>();
        slider.maxValue = GameManager.instance.maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        slider.value = GameManager.instance.health;

        if(GameManager.instance.health <= 0)
        {
            GameManager.instance.lastScene = SceneManager.GetActiveScene().name;
            SceneManager.LoadScene(3);
        }


    }
}
