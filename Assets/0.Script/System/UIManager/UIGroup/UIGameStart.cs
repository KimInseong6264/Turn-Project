

public class UIGameStart : UIGroup
{
    public void OnGameStart()
    {
        GameManager.Instance.UpdateUI(UIGroupName.UnitSelectUI, true);
        GameManager.Instance.UpdateUI(UIGroupName.GameStart, false);
        GameManager.Instance.OnLoadScene(1);
        
    }
}
    