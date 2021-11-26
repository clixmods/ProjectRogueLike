using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class LightSpecular : MonoBehaviour
{
    [SerializeField] CircleCollider2D Collider;
    [SerializeField] Light2D Light;
    [SerializeField] Color ColorLight;
    [SerializeField] float IntensityLight;
    // Start is called before the first frame update
    void Start()
    {
        Collider = gameObject.GetComponent<CircleCollider2D>();
        Light = gameObject.GetComponent<Light2D>();
        Collider.radius = Light.pointLightOuterRadius;
        IntensityLight = Light.intensity;
    }
 
    // Update is called once per frame
    void Update()
    {
        Collider.radius = Light.pointLightOuterRadius;
        ColorLight = Light.color;
        //if (IntensityLight < 1)
            IntensityLight = Light.intensity/3f;
        //else
          //  IntensityLight = 1;
    }

    float GetHDREmissive(Material mtl)
    {
        // private const byte k_MaxByteForOverexposedColor = 191; //internal Unity const
         Color _emissionColor = mtl.GetColor("_e_color");
        var intensity = (_emissionColor.r + _emissionColor.g + _emissionColor.b) / 3f;
        //print("intensity : " + intensity);
        var factor = 1f / intensity;
      //  print("factor : " + factor);

        //var maxColorComponent = _emissionColor.maxColorComponent;
        //print(maxColorComponent);

        //  var scaleFactor = 191 / maxColorComponent;
        //float intensity = Mathf.Log(255f / scaleFactor) / Mathf.Log(2f);
        return intensity;

        /*
          private const byte k_MaxByteForOverexposedColor = 191; //internal Unity const
 Color _emissionColor = GetComponent<Renderer>().material.GetColor("_EmissionColor");        
 var maxColorComponent = _emissionColor.maxColorComponent;
 var scaleFactor = k_MaxByteForOverexposedColor / maxColorComponent;
 float intensity = Mathf.Log(255f / scaleFactor) / Mathf.Log(2f); */
    }

    /*
     
     
     I don't think there is a specific HDR color format, just a color field gets labelled to use either hdr editor or regular color. 
    The HDR color is stored as a Color type which is 4 float values that can go above 1.0, such is the case for an hdr color. 
    You can use Color.RGBToHSV() to convert a color into hue/saturation/value values, and then change the V "value"  value to adjust the intensity.
    And then Color.HSVToRGB() to convert it back and assign the color back to your material
     */
    float GetHDREmissive(Material mtl, float new_intensity, Color color)
    {
        Color _emissionColor = mtl.GetColor("_e_color");
        var intensity = _emissionColor.r / 3f + _emissionColor.g / 3f + _emissionColor.b / 3f;
        var factor = 1f / intensity;
        var scaleFactor = 191 / factor;
        float intensitys = (Mathf.Log(255f / scaleFactor) / Mathf.Log(2f)) - 1.21f; // c'est la valeur de l'intensity
        intensitys = (Mathf.Log(255f) / Mathf.Log(2f) - Mathf.Log(scaleFactor) / Mathf.Log(2f)) - 1.21f;
        intensitys = (Mathf.Log(255f) / Mathf.Log(2f) - Mathf.Log(scaleFactor) / Mathf.Log(2f));
     
        intensitys = -(Mathf.Log(scaleFactor) / Mathf.Log(2f)) + (Mathf.Log(255f) / Mathf.Log(2f));


        float tamere = -(-new_intensity - (Mathf.Log(255f) / Mathf.Log(2f))) * Mathf.Log(2f);
        scaleFactor = Mathf.Pow(2.718281828f, tamere);
        factor = 191 / scaleFactor;
        intensity = 1f / factor;
        _emissionColor.r = (intensity * color.r*0.6f) / 3f;
        _emissionColor.g = (intensity * color.g*0.6f) / 3f;
        _emissionColor.b = (intensity * color.b*0.6f) / 3f;
        mtl.SetColor("_e_color", _emissionColor);

        return intensity;

    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.TryGetComponent<SpriteRenderer>(out SpriteRenderer component))
        {
            if (component.material.HasProperty("_specAmount"))
            {
                float multiplier = ((Collider.radius - Vector2.Distance(collision.transform.position, gameObject.transform.position)) / Collider.radius) * component.material.GetFloat("_specAmount") * IntensityLight;
                GetHDREmissive(component.material, multiplier, ColorLight);

            }
            //  print(multiplier);


        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.TryGetComponent<SpriteRenderer>(out SpriteRenderer component))
        {
            GetHDREmissive(component.material, 0,Color.white);
            //component.material.SetColor("_e_color", new Color(0.01f,0.01f,0.01f));
        }
    }
}
