using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Spine.Unity;

public class CharacterControl : MonoBehaviour
{


    [SerializeField] float character_speed =10f;
    [SerializeField] float character_jump_speed =10000f;
    [SerializeField] bool on_earth;//接地判定
    [SerializeField] Transform character_image_tf;
    [SerializeField] SkeletonAnimation character_Spine ;

    Vector3 character_speed_x;
    Vector3 character_speed_y;

    Rigidbody2D character_rigid2D;

    int character_jump_counter = 0;

    public bool is_special = false;//special判定



    // Start is called before the first frame update
    void Start()
    {
        character_rigid2D = GetComponent<Rigidbody2D>();
        
        character_speed_y = new Vector3(0, character_jump_speed, 0);

    }

    // Update is called once per frame
    void Update()
    {
       
        VerticalMove();
        HorizontalMove();
        StopMove();
        

    }

    void HorizontalMove()
    {

        if (Input.GetKey(KeyCode.RightArrow))
        {
            //==========移動============
            character_speed_x = new Vector3(character_speed, character_rigid2D.velocity.y, 0);
            character_rigid2D.velocity = character_speed_x;
            character_image_tf.localScale = new Vector3(1,1,1);

            //==========モーション============
            if (!character_Spine.AnimationName.Equals("run"))
            {
                character_Spine.state.SetAnimation(0, "run", true);
            }

        }
        else if (Input.GetKey(KeyCode.LeftArrow))
        {
            //==========移動============
            character_speed_x = new Vector3(-character_speed, character_rigid2D.velocity.y, 0);
            character_rigid2D.velocity = character_speed_x;
            character_image_tf.localScale = new Vector3(-1, 1, 1);

            //==========モーション============
            if (!character_Spine.AnimationName.Equals("run"))
            {
                character_Spine.state.SetAnimation(0, "run", true);
            }

        }
    }

    void VerticalMove()
    {

       
        if (character_jump_counter < 3) {

            if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                //==========移動============
                character_rigid2D.velocity = new Vector3(0, 0, 0);
                character_rigid2D.AddForce(character_speed_y);
                character_jump_counter += 1;

                //==========モーション============
                character_Spine.state.SetAnimation(0, "jump", false);
                character_Spine.state.AddAnimation(0, "run", true,0);

            }
        }

        if (!on_earth)
        {
            if (Input.GetKeyDown(KeyCode.DownArrow))
            {
                //==========移動============
                character_rigid2D.velocity = new Vector3(0, 0, 0);
                character_rigid2D.AddForce(-character_speed_y);

                //==========モーション============
                character_Spine.state.SetAnimation(0, "jump", false);
                character_Spine.state.AddAnimation(0, "run", true, 0);
            }
        }



    }

    void StopMove()
    {
        if (on_earth) {

            if (Input.GetKeyUp(KeyCode.RightArrow) || Input.GetKeyUp(KeyCode.LeftArrow))
            {
                //==========移動============
                character_rigid2D.velocity = new Vector3(0, 0, 0);

                //==========モーション============
                character_Spine.state.SetAnimation(0, "idle", true);

            }
        }

    }


  

   

   

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag.Equals("earth"))
        {
            character_jump_counter = 0; //ジャンプ回数初期化
            if (!character_Spine.AnimationName.Equals("idle"))//モーション初期化
            {
                character_Spine.state.SetAnimation(0, "idle", true);
                Debug.Log("koko");
            }
        }
        else
        {
            return;
        }

    }

    void OnCollisionStay2D(Collision2D collision)
    {
      // Debug.Log(collision.gameObject.tag);
        if (collision.gameObject.tag.Equals("earth"))
        {

            on_earth = true;
           
        }

        else
        {
           return;
        }
        

    }

    void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag.Equals("earth"))
        {
            on_earth = false;
        }
        else
        {
            return;
        }
    }


}
