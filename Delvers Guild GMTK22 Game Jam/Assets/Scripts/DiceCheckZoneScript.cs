using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiceCheckZoneScript : MonoBehaviour
{
    Vector3 diceVelocity;
    void FixedUpdate()
    {
        diceVelocity = DiceScript.diceVelocity;
    }
    
    void OnTriggerStay(Collider col)
    {
        if (diceVelocity.x == 0f && diceVelocity.y == 0f && diceVelocity.z == 0f)
		{
			switch (col.gameObject.name) {
			case "SideCheck1":
				DiceTextScript.DiceNumber = "6";
				break;
			case "SideCheck2":
				DiceTextScript.DiceNumber = "5";
				break;
			case "SideCheck3":
				DiceTextScript.DiceNumber = "4";
				break;
			case "SideCheck4":
				DiceTextScript.DiceNumber = "3";
				break;
			case "SideCheck5":
				DiceTextScript.DiceNumber = "2";
				break;
			case "SideCheck6":
				DiceTextScript.DiceNumber = "1";
				break;
            }
        }
    }
}
