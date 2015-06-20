public class StartTransitionGuiMessage
{

    public int Transition { get; set; }
    public int TransitionPhase { get; set; }
    public int MessageId { get; set; }


    public StartTransitionGuiMessage(int transition, int transitionPhase, int messageId)
    {
        Transition = transition;
        TransitionPhase = transitionPhase;
        MessageId = messageId;
    }

}
