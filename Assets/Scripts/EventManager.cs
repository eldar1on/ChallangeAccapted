using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventManager : MonoBehaviour
{
    public delegate void ClickAction();
    public static event ClickAction OnClicked;

    public GameObject _hasteUI;

    public void Haste()
    {
        OnClicked();
        _hasteUI.SetActive(false);
    }

}
