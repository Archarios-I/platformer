using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class inventoryScript : MonoBehaviour
{
    private Button bKeyButton;

    // Start is called before the first frame update
    void Start()
    {
        bKeyButton = GameObject.FindGameObjectWithTag("keyButton").GetComponent<Button>();
        bKeyButton.interactable = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void GetKey()
    {
        bKeyButton.interactable = true;
    }
}
