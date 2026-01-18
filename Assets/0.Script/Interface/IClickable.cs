using System;

public interface IClickable
{
    event Action OnClick;
    
    void OnStartCklick();
}
