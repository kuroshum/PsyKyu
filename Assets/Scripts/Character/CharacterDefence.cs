using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterDefence : MonoBehaviour
{
    // このクラスを呼び出したクラス(Player or Enemy)
    private Character parent;
    public void SetParent(Character parent) { this.parent = parent; }

    private float dodgeroll_time = 1.1f;
    private float invinsible_time = 0.5f;

    public void DodgeRoll(Character cha, Vector3 player_move_vec)
    {
        //animation起動

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
        //playerに無敵を付与する && 操作不可
        //cha.Isinvinsible = true;
        //cha.Canplay = false;
        yield return new WaitForSeconds(invinsible_time);

        //cha.Isinvinsible = false;

        yield return new WaitForSeconds(dodgeroll_time - invinsible_time);

        //cha.Canplay = true;
    }

    // 目の前に魔法陣を出現させる
    private IEnumerator playMagicCircle(GameObject space, ParticleSystem magicCircle, float pauseMagicCircleSeconds)
    {
        // 魔法陣の位置と角度調整
        magicCircle.transform.localPosition = space.transform.localPosition;
        magicCircle.transform.localRotation = Quaternion.Euler(0.0f, 180.0f, 0.0f);
        
        // ボールをキャッチする構えを取る
        parent.SetIsCatchBall(false);
        // 魔法陣のパーティクルを再生
        magicCircle.Simulate(0.0f, true, true);
        magicCircle.Play();
        
        yield return new WaitForSeconds(pauseMagicCircleSeconds);

        // 魔法陣のパーティクルを停止
        magicCircle.Simulate(0.0f, true, true);
        magicCircle.Pause();
        
        // ボールをキャッチする構えをとく
        parent.SetIsCatchBall(true);

        // 飛んできたボールをキャッチしたボールに設定する
        if (parent.GetThrowToMeBall() != null)
        {
            parent.SetCatchedBall(parent.GetThrowToMeBall());
            parent.SetThrowToMeBall(null);
            parent.SetIsIdleBall(true);
        }
    }


    // ボールをキャッチする
    public void PlayCatchMagicCircle(GameObject catchSpace, ParticleSystem magicCircle, float pauseMagicCircleSeconds)
    {
        StartCoroutine(playMagicCircle(catchSpace, magicCircle, pauseMagicCircleSeconds));
    }

    public void CatchBall(GameObject space, GameObject ball)
    {
        ball.transform.position = space.transform.position;
    }

}
