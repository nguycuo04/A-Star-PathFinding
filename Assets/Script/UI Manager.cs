using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] PlayerController playerController;
    [SerializeField] Slider slider;
    [SerializeField] TextMeshProUGUI currentHealth; 
    // Start is called before the first frame update
    void Start()
    {
        playerController = GameObject.Find("Player").GetComponent<PlayerController>();
        //slider = GetComponent<Slider>();
        //currentHealth = GetComponent<TextMeshProUGUI>(); 
        slider.maxValue = playerController.MaxHealth;
        slider.value = playerController.CurrentHealth; 
    }

    // Update is called once per frame
    void Update()
    {
        SliderUpdate(); 
    }

    public void SliderUpdate()
    {
        slider.value = playerController.CurrentHealth;
        currentHealth.text = playerController.CurrentHealth.ToString(); 
    }    
}
