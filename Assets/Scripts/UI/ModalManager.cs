using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModalManager : MonoBehaviour
{
    [SerializeField]
    private GameObject modal;

    public void OpenModal()
    {
        modal.SetActive(true);
    }

    public void CloseModal()
    {
        modal.SetActive(false);
    }
}
