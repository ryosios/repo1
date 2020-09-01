using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class TimerScript : MonoBehaviour
{
    [SerializeField] GameMaster GameMasterSC;
    TextMeshProUGUI textmeshpro;

    // Start is called before the first frame update
    void Start()
    {
        textmeshpro = this.GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        TimerSet();
    }

    void TimerSet()
    {
        textmeshpro.text = GameMasterSC.timer_time.ToString("F2");
    }
}
