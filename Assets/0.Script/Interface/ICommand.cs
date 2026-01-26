using System.Collections;

public interface ICommand
{
    float Duration { get; }
    
    IEnumerator Execute(IHitable target);
}