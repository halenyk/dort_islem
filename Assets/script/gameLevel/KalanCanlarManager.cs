using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KalanCanlarManager : MonoBehaviour
{
    [SerializeField]
    private GameObject kalanCan1, kalanCan2, kalanCan3;

    public void KalanCanlariKontrolEt(int kalanCan)
    {
        switch (kalanCan)
        {
            case 3:
                kalanCan1.SetActive(true);
                kalanCan2.SetActive(true);
                kalanCan3.SetActive(true);
                break;
            case 2:
                kalanCan1.SetActive(true);
                kalanCan2.SetActive(true);
                kalanCan3.SetActive(false);
                break;
            case 1:
                kalanCan1.SetActive(true);
                kalanCan2.SetActive(false);
                kalanCan3.SetActive(false);
                break;
            case 0:
                kalanCan1.SetActive(false);
                kalanCan2.SetActive(false);
                kalanCan3.SetActive(false);
                break;
            default:
                Debug.LogError("Invalid number of remaining lives: " + kalanCan);
                break;
        }
    }
}
