using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class TimerScript : MonoBehaviour
{
    [SerializeField] GameMaster GameMasterSC = default;
    [SerializeField] TextMeshProUGUI timer_textmeshpro = default;

    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        TimerSet();
    }

    void TimerSet()
    {
        timer_textmeshpro.text = GameMasterSC.timer_time.ToString("F2");
    }
}
