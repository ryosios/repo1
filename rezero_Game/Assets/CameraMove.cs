using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour
{
    public Transform Character_locator;
    Vector3 character_pos;
    Vector3 camera_pos;
    Vector3 chara_offset;
    // Start is called before the first frame update
    void Start()
    {
        chara_offset = OffSet( this.transform.position, Character_locator.position);   
    }

    // Update is called once per frame
    void Update()
    {
        CameraSetting();
    }

    void CameraSetting()
    {
        character_pos =  Character_locator.position;
        camera_pos =  this.transform.position;
        camera_pos.y = character_pos.y;
        camera_pos.y += chara_offset.y;
        this.transform.position = camera_pos;

        if(this.transform.position.y> 8f)
        {
            Vector3 camera_pos = this.transform.position;
            camera_pos.y = 8f;
            this.transform.position = camera_pos;
        }


    }

    Vector3 OffSet(Vector3 _A, Vector3 _B)
    {
        Vector3 value = _A - _B;
        return value ;
        
    }
}
