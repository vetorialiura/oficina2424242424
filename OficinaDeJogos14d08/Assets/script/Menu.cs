using UnityEngine;

public class MenuButtons : MonoBehaviour
{
    public void PlayGame()
    {
        ICommand playCommand = new PlayCommand("Level_2");
        playCommand.Execute();
    }
}