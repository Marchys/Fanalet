using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class EventMessagingGui : MonoBehaviourEx, IHandle<GameEventsGuiMessage>
{

    public Image MessageBox;
    public Text TextOfMessage;


    public void Handle(GameEventsGuiMessage message)
    {
        bool firstPass = true;
        foreach (var lineMessage in message.EventTextMessage)
        {
            if (firstPass)
            {
                TextOfMessage.text = lineMessage;
                firstPass = false;
            }
            else
            {
                TextOfMessage.text += "\n" + lineMessage;
            }
        }

        StartCoroutine(ShowMessage());
    }

    IEnumerator ShowMessage()
    {
        Color messageBoxColor = MessageBox.color;
        Color textOfMessageColor = TextOfMessage.color;
        while (messageBoxColor.a < 1)
        {
            messageBoxColor.a += 0.05f;
            textOfMessageColor.a += 0.05f;
            TextOfMessage.color = messageBoxColor;
            MessageBox.color = textOfMessageColor;
            yield return new WaitForSeconds(0.01f);
        }
        yield return new WaitForSeconds(5f);
        StartCoroutine(HideMessage());
    }

    IEnumerator HideMessage()
    {
        Color messageBoxColor = MessageBox.color;
        Color textOfMessageColor = TextOfMessage.color;
        while (messageBoxColor.a > 0)
        {
            messageBoxColor.a -= 0.05f;
            textOfMessageColor.a -= 0.05f;
            TextOfMessage.color = messageBoxColor;
            MessageBox.color = textOfMessageColor;
            yield return new WaitForSeconds(0.01f);
        }
    }
}
