using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class SelectLighthouseActivation : MonoBehaviour
{

    //pay for lighthouse activation
    public Button redChoiseButton;
    public Button blueChoiseButon;
    public Button yellowChoiseButton;

    public void ClickedChoiseButton(Button clickedButton)
    {
        switch (clickedButton.name)
        {
            case "red":
                redChoiseButton.image.color = Color.red;
                blueChoiseButon.image.color = new Color(118, 227, 225);
                yellowChoiseButton.image.color = new Color(253, 255, 141);
                break;
            case "blue":
                blueChoiseButon.image.color = Color.blue;
                redChoiseButton.image.color = new Color(255, 156, 156);
                break;
            case "yellow":
                yellowChoiseButton.image.color = Color.yellow;
                blueChoiseButon.image.color = new Color(118, 227, 225);
                redChoiseButton.image.color = new Color(255, 156, 156);
                break;
            default:
                break;
        }
    }


}
