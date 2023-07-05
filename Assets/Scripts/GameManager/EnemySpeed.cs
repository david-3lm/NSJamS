using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpeed : Buff
{
    public override string Description()
    {

        if (aux == 0)
        {
            return "Enemies slower!";
        }
        else
        {
            return "Enemies faster!";
        }
    }

    public override void Effect()
    {
        aux = Random.Range(0, 2);
        if(aux == 0)
        {
            PlayerStats.Instance.arriveTimeEnemy += 1;
        }
        else
        {
            PlayerStats.Instance.arriveTimeEnemy -= 1;
        }
        Mathf.Clamp(PlayerStats.Instance.arriveTimeEnemy, 2f, 10f);

    }

}
