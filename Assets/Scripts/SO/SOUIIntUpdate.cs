using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SOUIIntUpdate : MonoBehaviour
{

    public SOInt soInt;
    public TextMeshProUGUI uiTextValue;

    // Start is called before the first frame update
    void Start()
    {
        uiTextValue.text = $"x {soInt.value}";    
    }

    // Update is called once per frame
    void Update()
    {
        uiTextValue.text = $"x {soInt.value}";
    }
}
