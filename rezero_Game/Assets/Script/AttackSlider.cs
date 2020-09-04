using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AttackSlider : MonoBehaviour
{
    [SerializeField] Image attack_slider_fill_Im = default;
    [SerializeField] GameMaster GameMasterSC = default;
    [SerializeField] Slider attack_slider = default;
    [SerializeField] CharacterControl CharacterControl = default;

    Color attack_slider_fill_defaultcolor;


    public bool is_attack_slider_full { get; set; } = false; //アタックスライダーフル時のフラグ
    float attack_slider_speed = 0.5f;//アタックスライダーのたまる速さ


    

   

   

    // Start is called before the first frame update
    void Start()
    {

        attack_slider_fill_defaultcolor = attack_slider_fill_Im.color;

        attack_slider.value = 0;
       
    }

    // Update is called once per frame
    void Update()
    {
        if (GameMasterSC.is_game_playing == true)
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

        if (CharacterControl. on_attack == true)
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
