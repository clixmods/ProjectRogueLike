using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class debug_mtl_check : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float GetHDREmissive(Material mtl , float new_intensity)
        {
            Color _emissionColor = mtl.GetColor("_e_color");
            var intensity = _emissionColor.r/3f + _emissionColor.g/3f + _emissionColor.b/ 3f;
            print("intensity : " + intensity);
            var factor = 1f / intensity;
            print("factor : " + factor);
            var scaleFactor = 191 / factor;
            print("scaleFactor : " + scaleFactor);

            float intensitys = (Mathf.Log(255f / scaleFactor) / Mathf.Log(2f) )-1.21f; // c'est la valeur de l'intensity
            print("a :  " + intensitys);
            intensitys = (Mathf.Log(255f)/ Mathf.Log(2f) - Mathf.Log(scaleFactor)/ Mathf.Log(2f)) - 1.21f;
            print("b :  " + intensitys);

            intensitys = (Mathf.Log(255f) / Mathf.Log(2f) - Mathf.Log(scaleFactor) / Mathf.Log(2f));
            intensitys = -(Mathf.Log(scaleFactor) / Mathf.Log(2f)) + (Mathf.Log(255f) / Mathf.Log(2f));
            print("C : "+intensitys );

            /*
             intensitys = (Mathf.Log(255f) / Mathf.Log(2f)) - (Mathf.Log(scaleFactor) / Mathf.Log(2f)) ;
                intensitys = - (Mathf.Log(scaleFactor) / Mathf.Log(2f)) + (Mathf.Log(255f) / Mathf.Log(2f)) ;
            intensitys - (Mathf.Log(255f) / Mathf.Log(2f)) = - (Mathf.Log(scaleFactor) / Mathf.Log(2f)) ;


            (intensitys - (Mathf.Log(255f) / Mathf.Log(2f))) * -(Mathf.Log(2f)) = - (Mathf.Log(scaleFactor) ) ;

            (intensitys - (Mathf.Log(255f) / Mathf.Log(2f) ) ) * -(Mathf.Log(2f)) = - (Mathf.Log(scaleFactor) ) ;

            (intensitys - (Mathf.Log(255f) / Mathf.Log(2f))) * (Mathf.Log(2f)) = Mathf.Log(scaleFactor) ;


            */

            print("intensitys : " + (-intensitys) +" with scalefact = "+scaleFactor);


            float tamere = -(-new_intensity - (Mathf.Log(255f) / Mathf.Log(2f)) ) * Mathf.Log(2f);
            scaleFactor = Mathf.Pow(2.718281828f, tamere);
            print("new scalefactor : "+ scaleFactor);
            factor = 191 / scaleFactor ;
            intensity = 1f / factor;
            _emissionColor.r = (intensity * 3f )/3f ;
            _emissionColor.g = (intensity * 3f) / 3f;
            _emissionColor.b = (intensity * 3f) / 3f;
            mtl.SetColor("_e_color",_emissionColor);

            return intensity;

        }

       // GetHDREmissive(gameObject.GetComponent<SpriteRenderer>().material, 10);
    }
}
