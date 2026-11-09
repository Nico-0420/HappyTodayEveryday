using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class item : MonoBehaviour
{
    public int ID;
    public virtual void UseItem()
    {
        Debug.Log("Using item"+ name);
    }
}
