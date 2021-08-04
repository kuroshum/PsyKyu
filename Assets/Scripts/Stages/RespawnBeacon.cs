using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RespawnBeacon : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        CharacterManager.resList.Add(this.transform);
    }
}
