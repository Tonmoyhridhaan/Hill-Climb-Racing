using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class kamera_takibimiz : MonoBehaviour
{
    // Start is called before the first frame update
    public Transform nesne;
    void Start(){
        
    }

    // Update is called once per frame
    void Update(){
        Vector3 konum = new Vector3(nesne.position.x, nesne.position.y, transform.position.z);
        transform.position = konum;
    }
}
