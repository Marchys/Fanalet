using System;
using UnityEngine;
using UnityEngine.UI;

public class CanvasShow : MonoBehaviourEx, IHandle<UpdateGuiMessage>
{
    private Text lifeText;
    private Image fadeImage;

    public void Start()
    {
        lifeText = transform.Find("StatGroup/life").GetComponent<Text>();
        fadeImage = transform.Find("StatGroup/fade").GetComponent<Image>();
        fadeImage.color = new Color(0, 0, 0, 0);
    }

    public void Handle(UpdateGuiMessage message)
    {
        lifeText.text = "" + message.UpdatedProtaStats.OiLife + "/" + message.UpdatedProtaStats.MaxOiLife;
    }
    
    //public void setFade(string fadecolor)
    //{
    //    StopCoroutine("fade");
    //    StartCoroutine("fade", fadecolor);
    //}

    //private IEnumerator fade(string fadecolor)
    //{
    //    Color fadeFrom = (fadecolor == "black") ? Color.black : Color.white;
    //    while (fadeFrom.a > 0)
    //    {
    //        fadeFrom.a -= 2*Time.deltaTime;            
    //        fadeImage.color = fadeFrom;
    //        yield return null;
    //    }
       
    //}
  }
