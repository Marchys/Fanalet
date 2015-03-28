public class CameraShakeMessage
{
    public float Duration { get; set; }
    public float Magnitude { get; set; }

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

