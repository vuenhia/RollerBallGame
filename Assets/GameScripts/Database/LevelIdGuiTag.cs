using UnityEngine;

public class LevelIdGuiTag : MonoBehaviour
{
    // Tag name for idetification on buttons and levels for database interactions
    [SerializeField]
    private string levelName;

    public string getLevelName()
    {
        return this.levelName;
    }
}
