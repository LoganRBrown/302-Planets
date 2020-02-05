using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public delegate void ClickDelegate();


[RequireComponent(typeof(Button))]
public class ButtonController : MonoBehaviour, IPointerEnterHandler
{

    ClickDelegate callBack;

    public Text textField;

    public GameObject myPlanet;

    public bool isSun = false;

    public void Init(string name, GameObject planet, ClickDelegate callBack = null)
    {
        textField.text = name;
        myPlanet = planet;
        this.callBack = callBack;
    }

    public void Clicked()
    {
        if (callBack != null) callBack();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        GetComponent<Button>().Select();
    }
}
