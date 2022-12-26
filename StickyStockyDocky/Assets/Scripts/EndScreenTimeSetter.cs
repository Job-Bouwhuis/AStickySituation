using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class EndScreenTimeSetter : MonoBehaviour
{
    TMP_Text text;

    // Start is called before the first frame update
    void Start()
    {
        text = GetComponent<TMP_Text>();
        text.text = $"You finished in\n{TimerScript.time}";
    }
}
