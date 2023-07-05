using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletSpeed : Buff
{
    public override string Description()
    {

        if (aux == 0)
        {
            return "Faster bullets!";
        }
        else
        {
            return "Slower bullets!";
        }
    }

    public override void Effect()
    {
        aux = Random.Range(0, 2);
        if (aux == 0)
        {
            PlayerStats.Instance.bulletSpeed += 0.5f;
        }
        else
        {
            PlayerStats.Instance.bulletSpeed -= 0.5f;

        }
        Mathf.Clamp(PlayerStats.Instance.bulletSpeed, 0.5f, 3);

    }
}
