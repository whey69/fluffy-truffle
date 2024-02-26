using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bgcolor : MonoBehaviour
{
    [SerializeField] int colorAmount;
    [SerializeField] float time;

    [SerializeField] Color[] colors;
    private int curColor;

    void Start()
    {
        time = Time.time;
    }

    void Update()
    {
        float t = (Time.time - time) / time;

        if(t>=1.0f){
            curColor = (curColor + 1) % colors.Length;
            time = Time.time;
        }
        Camera.main.backgroundColor = Color.Lerp(colors[curColor], colors[(curColor + 1) % colors.Length], t);
    }
}
