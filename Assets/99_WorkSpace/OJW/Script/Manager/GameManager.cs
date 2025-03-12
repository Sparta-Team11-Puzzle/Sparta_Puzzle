public class GameManager : Singleton<GameManager>
{
    private void Start()
    {
        UIManager.ToggleCursor(true);
    }
}
