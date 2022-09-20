using UnityEngine;
using UnityEngine.UIElements;
using Zenject;

public class ExpirienceViewer : MonoBehaviour
{
    private UIDocument Document;
    private ProgressBar ExpBar;
    
    [Inject]
    private void Construct(Document document)
    {
        Document = document.Bar;
    }

    private void Start()
    {
        var root = Document.rootVisualElement;
        ExpBar = root.Q<ProgressBar>("ExpBar");
    }

    public void ChangeExpBar(int currentExp, int neededExp)
    {
        ChangeBar(currentExp, neededExp);
    }
    
    private void ChangeBar(int currentExp, int neededExp)
    {
        ExpBar.value = ExpBar.highValue * currentExp / neededExp;
        ExpBar.title = $"{System.Math.Round(ExpBar.value, 2)}%";
    }
}