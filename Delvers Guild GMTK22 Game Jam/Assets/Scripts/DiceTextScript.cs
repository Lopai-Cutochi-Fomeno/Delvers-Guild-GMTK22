using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DiceTextScript : MonoBehaviour
{
    public TMP_Text Text;
    public static string DiceNumber;

    void Start()
    {
        Text = GetComponent<TMP_Text>();
    }

    void Update()
    {
        Text.text = DiceNumber;
    }
}
