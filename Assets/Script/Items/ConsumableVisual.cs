using UnityEngine.UIElements;

public class ConsumableVisual : IConsumable
{
    private VisualElement Root;

    public ConsumableVisual(Document document)
    {
        Root = document.ItemPanel.rootVisualElement;
    }
    
    public void Change(ConsumableItem item, ref int amount, int id)
    {
        var amountVisual = Root.Q<Label>("AmountText" + id);
        amountVisual.text = amount.ToString();
    }
}