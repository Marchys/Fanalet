using System;
using UnityEngine;
using UnityEngine.UI;

public class CanvasShow : MonoBehaviourEx, IHandle<UpdateGuiMessage>
{
    //stats gui
    public Text lifeText;
    public Image fadeImage;
    public Text RedHeartText;
    public Text BlueHeartText;
    public Text YellowHeartText;

    //public void Start()
    //{
    //    fadeImage.color = new Color(0, 0, 0, 0);
    //}

    //message to update the gui
    public void Handle(UpdateGuiMessage message)
    {
        lifeText.text = "" + message.UpdatedProtaStats.OiLife + "/" + message.UpdatedProtaStats.MaxOiLife;
        RedHeartText.text = "" + message.UpdatedProtaStats.RedHearts;
        BlueHeartText.text = "" + message.UpdatedProtaStats.BlueHearts;
        YellowHeartText.text = "" + message.UpdatedProtaStats.YellowHearts;
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
