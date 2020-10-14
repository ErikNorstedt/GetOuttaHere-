using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStatManager : MonoBehaviour
{
    public Charactermovement thePlayer;

    
    void Start()
    {
        thePlayer = FindObjectOfType<Charactermovement>();
    }

    public void HurtPlayer(Vector3 direction)
    {
        thePlayer.Knockback(direction);
    }
}
