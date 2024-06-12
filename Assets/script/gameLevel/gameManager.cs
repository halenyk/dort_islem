using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;


public class gameManager : MonoBehaviour
{
    [SerializeField]
    private GameObject KareNesne;

    [SerializeField]
    private Transform karelerPaneli;

    private GameObject[] karelerDizisi = new GameObject[25];

    string hangiÝslem;
    [SerializeField]
    private Transform soruPanel;

    [SerializeField]
    private Text soruText;

    List<int> kareDegerlerListesi = new List<int>();
    int birinciSayi, ikinciSayi;
    int kacinciSoru;
    int dogruSonuc;

    int butonDegeri;
    bool butonaBasilsinmi;

    int kalanCan;
    KalanCanlarManager kalanCanlarManager;
    PuanManager puanManager;

    string sorununZorlukDerecesi;

    private void Awake()
    {
        kalanCan = 3;
        kalanCanlarManager = Object.FindObjectOfType<KalanCanlarManager>();
        puanManager = Object.FindObjectOfType<PuanManager>();
        kalanCanlarManager.KalanCanlariKontrolEt(kalanCan);
    }

    void Start()
    {
        butonaBasilsinmi = false;
        hangiÝslem = PlayerPrefs.GetString("hangiÝslem");
        soruPanel.GetComponent<RectTransform>().localScale = Vector3.zero;
        KareleriOlustur();
        KareDeðerleriniTexteYazdýr(); // Kare deðerlerini oluþturduktan sonra çaðýrýn

        SoruyuSor();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void KareleriOlustur()
    {
        for (int i = 0; i < 25; i++)
        {
            GameObject kare = Instantiate(KareNesne, karelerPaneli);
            kare.GetComponent<Button>().onClick.AddListener(() => ButonaBasildi());   //butonlarý aktif etmek için
            karelerDizisi[i] = kare;
        }
        Invoke("SoruPanelAc", 1f); // Soru panelini 1 saniye sonra aç
    }

    void ButonaBasildi()
    {
        if (butonaBasilsinmi)
        {

            butonDegeri = int.Parse(UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject.transform.GetChild(0).GetComponent<Text>().text);
            SonucuKontrolEt();
        }
    }

    void SonucuKontrolEt() // canlarý kotrol et
    {
        if (butonDegeri == dogruSonuc)
        {
            puanManager.PuanArtir(sorununZorlukDerecesi);
        }
        else
        {
            kalanCan--;
            kalanCanlarManager.KalanCanlariKontrolEt(kalanCan);
        }

    }

    void KareDeðerleriniTexteYazdýr()
    {
        foreach (var kare in karelerDizisi)
        {
            int rastgeleDeger = Random.Range(15, 30);

            kareDegerlerListesi.Add(rastgeleDeger);
            //Debug.Log("deðer eklendi");

            kare.transform.GetChild(0).GetComponent<Text>().text = rastgeleDeger.ToString();
        }
    }

    void SoruPanelAc()
    {
        butonaBasilsinmi = true;
        soruPanel.GetComponent<RectTransform>().DOScale(1, 0.3f).SetEase(Ease.OutBack);
    }

    void SoruyuSor()
    {
        if (hangiÝslem == "toplama")
        {
            birinciSayi = Random.Range(2, 15);
            kacinciSoru = Random.Range(0, kareDegerlerListesi.Count); //sorunun cevabý baþt g i
            dogruSonuc = kareDegerlerListesi[kacinciSoru];
            Debug.Log(kareDegerlerListesi.Count);
            ikinciSayi = kareDegerlerListesi[kacinciSoru] - birinciSayi;

            if(ikinciSayi <= 10)
            {
                sorununZorlukDerecesi = "kolay";
            }
            else if(ikinciSayi > 10 && ikinciSayi <= 20)
            {
                sorununZorlukDerecesi = "orta";
            }
            else
            {
                sorununZorlukDerecesi = "zor";
            }
            soruText.text = ikinciSayi.ToString() + " + " + birinciSayi.ToString();
        }
        else if (hangiÝslem == "cikarma")
        {
            birinciSayi = Random.Range(2, 15);
            kacinciSoru = Random.Range(0, kareDegerlerListesi.Count);
            dogruSonuc = kareDegerlerListesi[kacinciSoru];
            ikinciSayi = kareDegerlerListesi[kacinciSoru] + birinciSayi;


            if (ikinciSayi <= 20)
            {
                sorununZorlukDerecesi = "kolay";
            }
            else if (ikinciSayi > 20 && ikinciSayi <= 35)
            {
                sorununZorlukDerecesi = "orta";
            }
            else
            {
                sorununZorlukDerecesi = "zor";
            }
            soruText.text = ikinciSayi.ToString() + " - " + birinciSayi.ToString();
        }
        else
        {
            Debug.Log("Ýþlem türü belirtilmedi!");
        }
    }
}
