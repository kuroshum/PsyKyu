using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterCatchSpaceManager : MonoBehaviour
{
    // ���̃N���X���Ăяo�����N���X(Player or Enemy)
    private Character parent;
    public void SetParent(Character parent) { this.parent = parent; }

    private bool onBallHit;
    public bool GetOnBallHit() { return onBallHit; }
    
    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "Ball")
        {
            onBallHit = true;
            parent.SetThrowToMeBall(col.gameObject);
        }
    }

    void OnTriggerExit(Collider col)
    {
        if (col.gameObject.tag == "Ball")
        {
            onBallHit = false;
            parent.SetThrowToMeBall(null);
        }
    }

}
