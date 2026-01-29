using System.Collections;

public interface ICommand
{
    float AfterDelay { get; }
    
    IEnumerator Execute(BattleInfo battleInfo);
}