using UnityEngine;

public class SelectorState: MonoBehaviour
{
    public Selector selector;
    private bool wasTriggeredLastFrame = false;

    public SelectorState(Selector selector)
    {
        this.selector = selector;
    }

    public bool WasTriggered()
    {
        bool currentState = selector.isTriggered();
        bool result = currentState && !wasTriggeredLastFrame;
        wasTriggeredLastFrame = currentState;
        return result;
    }
}
