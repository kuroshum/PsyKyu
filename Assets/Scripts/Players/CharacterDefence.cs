using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterDefence : MonoBehaviour
{
    private float dodgeroll_time = 1.1f;
    private float invinsible_time = 0.5f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void DodgeRoll(Character cha, Vector3 player_move_vec)
    {
        //animationãNìÆ

        player_move_vec = player_move_vec.normalized;

        StartCoroutine(Dodge_Time_Delay(cha));
        StartCoroutine(Dodge_Move(player_move_vec));
    }

    IEnumerator Dodge_Move(Vector3 vec)
    {
        

        yield return null;
    }

    IEnumerator Dodge_Time_Delay(Character cha)
    {
        //playerÇ…ñ≥ìGÇïtó^Ç∑ÇÈ && ëÄçÏïsâ¬
        //cha.Isinvinsible = true;
        //cha.Canplay = false;
        yield return new WaitForSeconds(invinsible_time);

        //cha.Isinvinsible = false;

        yield return new WaitForSeconds(dodgeroll_time - invinsible_time);

        //cha.Canplay = true;
    }


    public void CatchBall(GameObject forwardSpace)
    {
        
    }

}
