using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class araba : MonoBehaviour
{
    // Start is called before the first frame update
    public WheelJoint2D arka_tekerlek;
    public WheelJoint2D on_tekerlek;
    public float hiz;
    private float hareket;
    private float rot_lik;
    private float rot_mevcut;
    private float rot_son;
    private float rot_lik_t;
    private float rot_mevcut_t;
    private float rot_son_t;
    public int toplam_altin;
    public Text altin_texti;
    public Image yakit_resmi;
    public float toplam_yakit = 95;
    public float yakit_azalama_hiz;
    public Sprite spritemiz;
    public Texture2D texture_resim;
    public bool hayatta_mi = true;
    float mevcut_yakit;
    public Text text_takla;
    public int altin_miktari_takla;
    public float sayac = 0.1f;
    public GameObject Panel;
    public Image gosterilen_resim;
    public Text takla_sayisi_text;
    public Text ters_takla_sayisi_text;
    public Text altin_sayisi_text;
    private int takla_sayisi = 0;
    private int ters_takla_sayisi = 0;
    public bool gas;
    public bool fren;
    public float ivme;

    void Start(){
        rot_mevcut = transform.rotation.eulerAngles.z;
        rot_lik = rot_mevcut;
        rot_son = rot_mevcut;
        rot_mevcut_t = transform.rotation.eulerAngles.z;
        rot_lik_t = rot_mevcut_t;
        rot_son_t = rot_mevcut_t;
        ivme = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (!hayatta_mi)
        {
            return;
        }
        //float v = Input.GetAxis("Horizontal");
        //hareket = hiz * v;
        if (gas)
        {
            ivme += 0.07f;
            if(ivme > 1f)
            {
                ivme = 1f;
            }
        }
        if (fren)
        {
            ivme -= 0.009f;
            if(ivme <-1f)
            {
                ivme = -1f;
            }
        }
        
        if(!gas && !fren)
        {
            ivme = 0;
        }
        hareket = hiz * ivme;
        Takla();
        TersTakla();
        RectTransform asd = yakit_resmi.GetComponent<RectTransform>();
        mevcut_yakit = asd.sizeDelta.x - Time.deltaTime * yakit_azalama_hiz;
        asd.sizeDelta = new Vector2(mevcut_yakit, asd.sizeDelta.y);
        if (mevcut_yakit < 0)
        {
            hayatta_mi = false;
        }
        if(text_takla.text != "")
        {
            sayac -= Time.deltaTime;
        }
        if(sayac <= 0)
        {
            text_takla.text="";
            sayac = 0.1f;
        }

    }
    void FixedUpdate()
    {
        if(hareket==0)
        {
            arka_tekerlek.useMotor = false;
            on_tekerlek.useMotor = false;

        }
        else
        {
            arka_tekerlek.useMotor=true;
            on_tekerlek.useMotor=true;
            JointMotor2D motore = new JointMotor2D();
            motore.motorSpeed = hareket;
            motore.maxMotorTorque = 10000;
            arka_tekerlek.motor = motore;
            on_tekerlek.motor = motore;
        }
        
    }

    void Takla()
    {
        rot_mevcut = transform.rotation.eulerAngles.z;
        if (rot_son < rot_mevcut)
        {
            rot_lik = rot_mevcut;
        }
        else if (rot_son > rot_mevcut && rot_son - rot_mevcut > 100)
        {
            rot_lik = rot_mevcut;
        }
        if (rot_lik - rot_mevcut > 300)
        {
            Debug.Log("Takla");
            rot_lik = rot_mevcut;
            text_takla.text = "Takla +" + altin_miktari_takla;
            toplam_altin += altin_miktari_takla;
            takla_sayisi++;

        }
        rot_son = rot_mevcut;
    }
  
    void TersTakla()
    {
        rot_mevcut_t = transform.rotation.eulerAngles.z;
        if (rot_son_t > rot_mevcut_t)
        {
            rot_lik_t = rot_mevcut_t;
        }
        else if (rot_son_t < rot_mevcut_t && rot_mevcut_t - rot_son_t > 100)
        {
            rot_lik_t = rot_mevcut_t;
        }
        if (rot_mevcut_t - rot_lik_t > 300)
        {
            Debug.Log("Ters Takla");
            rot_lik_t = rot_mevcut_t;

            text_takla.text = "Takla +" + altin_miktari_takla;
            ters_takla_sayisi++;
        }
        rot_son_t = rot_mevcut_t;

    }

    void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.gameObject.tag == "altin")
        {
            int miktar = coll.GetComponent<altinn>().miktar;
            toplam_altin += miktar;
            GameObject.Destroy(coll.gameObject);
            altin_texti.text = toplam_altin.ToString();
        }
        if (coll.gameObject.tag == "yakit")
        {
            RectTransform asd = yakit_resmi.GetComponent<RectTransform>();
            asd.sizeDelta = new Vector2(toplam_yakit, asd.sizeDelta.y);
            GameObject.Destroy(coll.gameObject);
        }

    }
    public void Resim_Cek()
    {
        if(!hayatta_mi)
        {
            return;
        }
        Texture2D text = new Texture2D(Screen.width/2, Screen.height/2, TextureFormat.RGB24, false);
        texture_resim = new Texture2D(Screen.width/2, Screen.height/2);
        text.ReadPixels(new Rect(Screen.width/4, Screen.height/4, Screen.width/2, Screen.height/2), 0, 0);
        text.Apply();
        texture_resim = text;
        text.Compress(false);
        spritemiz = Sprite.Create(texture_resim, new Rect(0, 0, texture_resim.width, texture_resim.height), new Vector2(0, 0));
        hayatta_mi = false;
        Panel.SetActive(true);
        takla_sayisi_text.text = "Flip: " + takla_sayisi;
        ters_takla_sayisi_text.text = "Air: " + ters_takla_sayisi;
        altin_sayisi_text.text = "Total Points: " + toplam_altin;
        

    }
    public void Game_Over()
    {
        Application.LoadLevel(Application.loadedLevel);
    }
    public void Gas_press()
    {
        gas = true;
    }
    public void Gas_break()
    {
        gas = false;
    }
    public void Fren_press()
    {
        fren = true;
    }
    public void Fren_break()
    {
        fren = false;
    }

}
