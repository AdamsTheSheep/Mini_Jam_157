using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerUI : MonoBehaviour
{
    public static PlayerUI instance;

    public Image interactableHoldProgressImage;
    public Image pointerImage;

    private void Awake()
    {
        if (instance) Destroy(instance.gameObject);
        instance = this;
    }
}
