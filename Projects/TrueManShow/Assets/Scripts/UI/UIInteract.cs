using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.UI;

public enum EDescription
{
    Interact, Pickup, Drop, Throw, Inspect
}

[System.Serializable]
public class UIInteractionValue
{
    public enum EInteractKey
    {
        E, LMB
    }

    public EInteractKey Key;
    public EDescription Description;
}

public class UIInteract : SerializedMonoBehaviour
{
    [System.Serializable]
    public class InteractionSet
    {
        public GameObject group;
        public Image InteractImage;
        public Text InteractionDescription;
    }

    public static UIInteract Main { get; private set; }

    public Sprite[] KeyIcons;

    public InteractionSet[] Set;

    private void Awake()
    {
        if (Main) Destroy(Main);
        Main = this;

        HideAll();
    }

    public void Display(UIInteractionValue value, int setIndex = 0)
    {
        Assert.IsTrue(setIndex < Set.Length);

        InteractionSet set = Set[setIndex];
        set.group.SetActive(true);
        set.InteractImage.sprite = KeyIcons[(int)value.Key];
        set.InteractionDescription.text = value.Description.ToString();
    }

    public void Hide(int setIndex = 0)
    {
        Assert.IsTrue(setIndex < Set.Length);
        Set[setIndex].group.SetActive(false);
    }
    public void HideAll()
    {
        for (int i = 0; i < Set.Length; i++)
            Hide(i);
    }

}
