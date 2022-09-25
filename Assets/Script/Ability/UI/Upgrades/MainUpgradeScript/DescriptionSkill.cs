using UnityEngine.UIElements;

public class DescriptionSkill
{
    private readonly Label DescriptionLabel;

    public DescriptionSkill(VisualElement root)
    {
        DescriptionLabel = root.Q<Label>();
    }

    public void Set(string description)
    {
        DescriptionLabel.text = description;
    }
}