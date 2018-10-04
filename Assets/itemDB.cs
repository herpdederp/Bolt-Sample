using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class itemDB : MonoBehaviour
{

    public List<itemData> itemDatabase;



}
[System.Serializable]
public class itemData
{
    //stackable, stack size, graphic, type, value, description, stat1, stat2, etc

    public int ID;
    public string name;
    public Sprite icon;
    public bool stackable;
    public int stackSize;
    public string description;
    public int type; //make enum
    public float value;
    public float stat1;
    public float stat2;
    public float stat3;
}
