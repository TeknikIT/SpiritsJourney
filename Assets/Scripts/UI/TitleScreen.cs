using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TitleScreen : MonoBehaviour {

    Text text;
    GameObject image;
    bool buttonPressed = false;
    float timer = 0;
	// Use this for initialization
	void Start () {
        text = gameObject.transform.Find("Panel").GetComponentInChildren<Text>();
        image = gameObject.transform.Find("Panel").transform.Find("Logo").gameObject;
        StartCoroutine(FlucuateTextAlpha(Mathf.PI / 2));
    }
	
	// Update is called once per frame
	void Update () {
        if (Input.anyKey)
        {
            buttonPressed = true;
        }
        if (buttonPressed)
        {
            FadeCanvas();
        }
        fluctuateImage();
    }
    IEnumerator FlucuateTextAlpha(float FadeTime) {
        text.CrossFadeAlpha(0.0f, FadeTime, false);
        yield return new WaitForSeconds(FadeTime);
        text.CrossFadeAlpha(1.0f, FadeTime, false);
        yield return new WaitForSeconds(FadeTime);
        StartCoroutine(FlucuateTextAlpha(FadeTime));

    }
    void FadeCanvas()
    {
        if(GetComponent<CanvasGroup>().alpha > 0)
        {
            GetComponent<CanvasGroup>().alpha -= 0.025f;
        }
        else
        {
            buttonPressed = false;
            gameObject.SetActive(false);
        }
    }
    void fluctuateImage()
    {
        float size = 0.01f * Mathf.Sin(2 * timer) + 0.5f;
        image.GetComponent<RectTransform>().localScale = new Vector3(size, size, 1);
        timer += Time.deltaTime;
        if(timer > Mathf.PI * 2)
        {
            timer = 0;
        }
    }
}
