using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Buffs
{
    Health,
    Stamina,
    Damage
};

public class Mug : MonoBehaviour {

    public Buffs Buff;
	// Use this for initialization
	void Start () {
        Buff = (Buffs)Random.Range(0, 2);
        SpriteRenderer renderer = GetComponent<SpriteRenderer>();
        switch(Buff)
        {
            case Buffs.Health:
                renderer.color = Color.green;
                break;
            case Buffs.Stamina:
                renderer.color = Color.yellow;
                break;
            case Buffs.Damage:
                renderer.color = Color.red;
                break;
        }
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
