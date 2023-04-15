public class LevelRestarter : InteractiveObject
{
    public override void Action(Player initiator)
    {
        SceneLoader.RestartScene();
    }
}