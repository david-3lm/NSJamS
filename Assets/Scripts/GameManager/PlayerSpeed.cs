using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpeed : Buff
{
    public override string Description()
    {

        if (aux == 0)
        {
            return "Move speed up!";
        }
        else
        {
            return "Move speed down!";
        }
    }

    public override void Effect()
    {
        aux = Random.Range(0, 2);

        if(aux == 0)
        {
            PlayerStats.Instance.moveSpeed += 1;

        }
        else
        {
            PlayerStats.Instance.moveSpeed -= 1;
        }
        Mathf.Clamp(PlayerStats.Instance.moveSpeed, 5f, 30);

    }
}
