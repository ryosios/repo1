using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AttackSlider : MonoBehaviour
{
    [SerializeField] Image attack_slider_fill_Im;
    Color attack_slider_fill_defaultcolor;
    public Slider attack_slider;
    public GameMaster GameMaster;
    public CharacterControl CharacterControl;
    float attack_slider_speed = 0.5f;


    public bool is_attack_slider_full = false;

   

   

    // Start is called before the first frame update
    void Start()
    {

        attack_slider_fill_defaultcolor = attack_slider_fill_Im.color;

        attack_slider.value = 0;
       
    }

    // Update is called once per frame
    void Update()
    {
        if (GameMaster.is_game_playing == true)
        {
            AttackCount();
        }

        SliderColorFull();


    }

    void AttackCount()
    {
        attack_slider.value += attack_slider_speed * Time.deltaTime;
        if (attack_slider.value >= 1f)
        {
            attack_slider.value = 1;
            
            is_attack_slider_full = true;

        }

        if (CharacterControl.is_attack == true)
        {
            attack_slider.value = 0;
            is_attack_slider_full = false;
           
        }
       
    }

    void SliderColorFull()
    {
        if (is_attack_slider_full)
        {
            attack_slider_fill_Im.color = new Color(1f, 1f, 1f);
        }
        else
        {
            attack_slider_fill_Im.color = attack_slider_fill_defaultcolor;
        }

    }
    
}
