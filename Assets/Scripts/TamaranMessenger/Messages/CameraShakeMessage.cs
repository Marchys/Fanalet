public class CameraShakeMessage
{
    public float Duration { get; set; }
    public float Magnitude { get; set; }

    /// <summary>
    /// CameraShake Message
    /// </summary>
    /// <param name="duration">Duration of the shake.</param>
    /// <param name="magnitude">Magnitude of the shake.</param>
    public CameraShakeMessage(float duration, float magnitude)
    {
        Duration = duration;
        Magnitude = magnitude;
    }

    public CameraShakeMessage()
    {
        Magnitude = 0.05f;
        Duration = 1f;
    }
}

