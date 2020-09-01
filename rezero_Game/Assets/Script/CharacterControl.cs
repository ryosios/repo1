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
    public bool is_attack = false;

    public AttackSlider AttackSliderScript; 


    // Start is called before the first frame update
    void Start()
    {
        character_rigid2D = GetComponent<Rigidbody2D>();
        
        character_speed_y = new Vector3(0, character_jump_speed, 0);

        character_Spine.state.SetAnimation(0, "run", true);

    }

    // Update is called once per frame
    void Update()
    {
       
        VerticalMove();

        HorizontalMove();

        StopMove();

        CharacterMoveLimit();

        
        AttackMove();
       
        
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
            if (!character_Spine.AnimationName.Equals("run2")&& !character_Spine.AnimationName.Equals("jump") && !character_Spine.AnimationName.Equals("attack"))
            {
                character_Spine.state.SetAnimation(0, "run2", true);
            }

        }
        else if (Input.GetKey(KeyCode.LeftArrow))
        {
            //==========移動============
            character_speed_x = new Vector3(-character_speed, character_rigid2D.velocity.y, 0);
            character_rigid2D.velocity = character_speed_x;

            // character_image_tf.localScale = new Vector3(-1, 1, 1);
            character_image_tf.localScale = new Vector3(1, 1, 1);

            //==========モーション============
            if (!character_Spine.AnimationName.Equals("run") && !character_Spine.AnimationName.Equals("jump") && !character_Spine.AnimationName.Equals("attack"))
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
                character_Spine.state.SetAnimation(0, "down", false);
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
                // character_Spine.state.SetAnimation(0, "idle", true);
                character_Spine.state.SetAnimation(0, "run", true);

               

            }
        }

    }

    void CharacterMoveLimit()
    {
        if(this.transform.position.x<-4.5f) {

            Vector3 chara_pos = transform.position;
            chara_pos.x = -4.5f;
            transform.position = chara_pos;
        }
        if(this.transform.position.x > 3f)
        {
            Vector3 chara_pos = transform.position;
            chara_pos.x = 3f;
            transform.position = chara_pos;
        }

    }

    void AttackMove()
    {
        if (AttackSliderScript.is_attack_slider_full == true)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                //==========モーション============
                character_Spine.state.SetAnimation(0, "attack", false);
                character_Spine.state.AddAnimation(0, "run", true, 0);

                //==========bool============
                is_attack = true;
            }
        }
        else
        {
            is_attack = false;
        }

    }
   

   

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag.Equals("earth"))
        {
            character_jump_counter = 0; //ジャンプ回数初期化
            if (!character_Spine.AnimationName.Equals("idle"))//モーション初期化
            {
                // character_Spine.state.SetAnimation(0, "idle", true);
                character_Spine.state.SetAnimation(0, "run", true);
              
            }
        }
        else
        {
            return;
        }

    }

    void OnCollisionStay2D(Collision2D collision)
    {
      
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
