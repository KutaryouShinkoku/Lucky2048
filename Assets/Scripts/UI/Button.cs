using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Button : MonoBehaviour
{
    public object OnClick { get; internal set; }

    public void Close()
    {
        gameObject.SetActive(false);
    }

}
