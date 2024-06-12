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

    string hangi�slem;
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
        hangi�slem = PlayerPrefs.GetString("hangi�slem");
        soruPanel.GetComponent<RectTransform>().localScale = Vector3.zero;
        KareleriOlustur();
        KareDe�erleriniTexteYazd�r(); // Kare de�erlerini olu�turduktan sonra �a��r�n

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
            kare.GetComponent<Button>().onClick.AddListener(() => ButonaBasildi());   //butonlar� aktif etmek i�in
            karelerDizisi[i] = kare;
        }
        Invoke("SoruPanelAc", 1f); // Soru panelini 1 saniye sonra a�
    }

    void ButonaBasildi()
    {
        if (butonaBasilsinmi)
        {

            butonDegeri = int.Parse(UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject.transform.GetChild(0).GetComponent<Text>().text);
            SonucuKontrolEt();
        }
    }

    void SonucuKontrolEt() // canlar� kotrol et
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

    void KareDe�erleriniTexteYazd�r()
    {
        foreach (var kare in karelerDizisi)
        {
            int rastgeleDeger = Random.Range(15, 30);

            kareDegerlerListesi.Add(rastgeleDeger);
            //Debug.Log("de�er eklendi");

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
        if (hangi�slem == "toplama")
        {
            birinciSayi = Random.Range(2, 15);
            kacinciSoru = Random.Range(0, kareDegerlerListesi.Count); //sorunun cevab� ba�t g i
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
        else if (hangi�slem == "cikarma")
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
            Debug.Log("��lem t�r� belirtilmedi!");
        }
    }
}
