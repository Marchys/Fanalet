using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class MinotaurMinimapHandler : MonoBehaviourEx, IHandle<MinotaurEnterLighthouseAreaMessage>, IHandle<MinotaurExitLighthouseAreaMessage>, IHandle<LighthouseActivatedMessage>
{

    private Image _minotaurImage;
    private bool[] _lighthousesState = new bool[] { false, false, false, false };
    private bool[] _minotaurInArea = new bool[] { false, false, false, false };

    void Start()
    {
        _minotaurImage = GetComponent<Image>();
    }

    public void Handle(MinotaurEnterLighthouseAreaMessage message)
    {
        _minotaurInArea[message.Area] = true;
        CheckIfMinotaurShow();
    }

    public void Handle(MinotaurExitLighthouseAreaMessage message)
    {
        _minotaurInArea[message.Area] = false;
        CheckIfMinotaurShow();
    }

    public void Handle(LighthouseActivatedMessage message)
    {
        _lighthousesState[message.Lighthouse] = true;
        CheckIfMinotaurShow();
    }

    private void CheckIfMinotaurShow()
    {
        _minotaurImage.enabled = false;
        for (var i = 0; i < 4; i++)
        {
            if (_minotaurInArea[i] && _lighthousesState[i])
            {
                _minotaurImage.enabled = true;
            }
        }
    }
}
