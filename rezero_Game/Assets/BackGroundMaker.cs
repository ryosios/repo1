﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackGroundMaker : MonoBehaviour
{
    [SerializeField] float background_speed = 20f;
    [SerializeField] GameObject[] background_prefab = new GameObject[3];
    [SerializeField] GameObject[] background_Clone = new GameObject[3];
    // Start is called before the first frame update
    void Start()
    {
        SetBackGround();
    }

    // Update is called once per frame
    void Update()
    {
        BackGroundMove();
    }

    void SetBackGround()
    {
        for (int i = 0; i < 3; i++)
        {

            background_Clone[i] = Instantiate(background_prefab[i],  this.transform) as GameObject;
            background_Clone[i].transform.localPosition = new Vector3(-15.5f + i * 15.5f, 0, 0);

        }
    }

    void BackGroundMove()
    {
        foreach (var background in background_Clone)
        {
            Vector3 this_position = background.transform.localPosition;
            this_position.x -= background_speed*Time.deltaTime;
            background.transform.localPosition = this_position;

            if (background.transform.localPosition.x < -31f)
            {
                background.transform.localPosition = new Vector3(15.5f, 0, 0);
            }
          //  Debug.Log("koko2");
        }

    }
}
