using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using System;
using System.Timers;

public class CubeContoller : MonoBehaviour 
{
    public float hue = 0f;
    public float sat = 1f;
    public float val = 1f;
    public float speed = 0.05f;
    private bool clicked = false;
    Vector3 scaleChange;
    Vector3 defaultSize;
    float minSize = 0.01f;
    public event Action<Vector3> OnShrink;
    private System.Timers.Timer shrinkTimer;
    private Renderer rend;

    // Start is called before the first frame update
    void Start()
    {
        defaultSize = new Vector3(transform.localScale.x,transform.localScale.y, transform.localScale.z);
        scaleChange = new Vector3(transform.localScale.x * -0.01f,transform.localScale.y * -0.01f,transform.localScale.z * -0.01f);
        MatrixContoller.OnCubeShrink += onFriendShrinking;
        rend = GetComponent<Renderer>();
        rend.material.color = Color.HSVToRGB (hue, sat, val);
    }

    // Update is called once per frame
    void Update()
    {
        if (clicked) {
            if (hue < 1) {
                hue = hue + speed;
            } else {
                hue = 0;
            }
            rend.material.color = Color.HSVToRGB (hue, sat, val);
            if (transform.localScale.y < minSize)
            {   
                scaleChange = -scaleChange;
            }
            transform.localScale += scaleChange;
            if (transform.localScale.y == defaultSize.y) {
                scaleChange = -scaleChange;
                clicked = false;
            }
        }
    }

    void OnMouseDown()
    {
        if (!clicked) {
            clicked = true;
            if (OnShrink != null) {
                OnShrink(transform.position);
            }            
        }         
    }

    void onFriendShrinking(Vector3 friendLoc) {
        //float shrinkMult = Math.Abs(transform.position.magnitude - friendLoc.magnitude);
        float shrinkMult = Vector3.Distance(friendLoc, transform.position);
        shrinkTimer = new System.Timers.Timer((shrinkMult * 100) + 1);
        shrinkTimer.Elapsed += OnTimedEvent;
        shrinkTimer.AutoReset = false;
        shrinkTimer.Enabled = true;
    }

    private void OnTimedEvent(System.Object source, ElapsedEventArgs e)
    {
        if(!clicked) clicked = true;
        shrinkTimer.Enabled = false;
    }

}
