using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;


public class item : MonoBehaviour
{
    public int ID;

    public int quantity=1;
    private TMP_Text quantityText;


    private void Awake()
    {
        quantityText=GetComponentInChildren<TMP_Text>();
    }
    public void UpdateQuantityDisplay()
    {
        if (quantityText!=null)
        {
            quantityText.text=quantity>1? quantity.ToString():"";
        }
    }


    public void AddtoStack(int amount= 1)
    {
        quantity+=amount;
        UpdateQuantityDisplay();
    }


    public int RemoveFromStack(int amount=1)
    {
        int removed= Math.Min(amount,quantity);
        quantity-=removed;
        UpdateQuantityDisplay();
        return removed;
    }


    public GameObject CloneItem(int newQuantity)
    {
        GameObject clone= Instantiate(gameObject);
        item cloneItem=clone.GetComponent<item>();
        cloneItem.quantity=newQuantity;
        cloneItem.UpdateQuantityDisplay();
        return clone;
    }


    public virtual void UseItem()
    {
        Debug.Log("Using item"+ name);
    }
}

