using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class puanManager : MonoBehaviour
{

    private int toplamPuan;
    private int puanArtii;

    [SerializeField]
    private Text puanText;
    // Start is called before the first frame update
    void Start()
    {
        puanText.text_toplamPuan.ToString();
    }

    
    public void PuanArtir(string zorlukSeviyesi)
    {
        switch(zorlukSeviyesii)
        {
            case "kolay":
                puanArtisi = 5;
                break;
            case "orta":
                puanArtisi = 10;
                break;
            case "zor":
                puanArtisi = 15;
                break;
        }
        toplamPuan += puanArtisi;
        puanText.text_toplamPuan.ToString();

    }
}
