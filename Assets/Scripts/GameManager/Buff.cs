using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Buff : MonoBehaviour
{
    protected int aux;
    public abstract void Effect();
    public abstract string Description();
}
