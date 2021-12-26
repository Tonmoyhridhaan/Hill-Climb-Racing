using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class airtime : MonoBehaviour
{
    // Start is called before the first frame update
    float sayac = 0;
    float combo = 0.0f;
    public bool on_air = true;
    public GameObject araba_;
    bool caprti_mi = false;
    public Text air_text;
    public int altin_mikltari_air;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (sayac > 1f)
        {
            on_air = true;
            combo++;
            sayac = 0;
            //air_text.text = "Air: +" + altin_mikltari_air * combo;
            araba_.GetComponent<araba>().toplam_altin += altin_mikltari_air;

        }
        sayac += Time.deltaTime;

    }
    void OnCollisionEnter2D(Collision2D coll)
    {
        if (coll.gameObject.tag == "Player" || coll.gameObject.tag == "teker")
        {
            combo = 0;
            sayac = 0;
            on_air = false;
        }
        if(coll.collider.tag  == "kafa")
        {
            araba_.GetComponent<araba>().Resim_Cek();
            caprti_mi = true;
            araba_.GetComponent<araba>().hayatta_mi = false;
        }

    }

    void OnCollisionStay2D(Collision2D coll)
    {
        if (coll.gameObject.tag == "Player" || coll.gameObject.tag == "teker")
        {
            combo = 0;
            sayac = 0;
            on_air = false;
        }


    }
}
