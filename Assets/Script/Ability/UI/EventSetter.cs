using System.Collections.Generic;
using UnityEngine.Events;
using UnityEngine.UIElements;

namespace UiSkillUpgrade
{
    public class EventSetter
    {
        private readonly VisualElement UpgradeWindow;
        private readonly List<VisualElement> ParentSkillsWindows = new();

        public EventSetter(Button button, VisualElement window, List<VisualElement> parentSkillsWindows,
            ref UnityAction allWindowsVisibleControl)
        {
            UpgradeWindow = window;
            ParentSkillsWindows = parentSkillsWindows;
            button.clicked += WindowVisible;
            allWindowsVisibleControl += InvisibleAllWindows;
        }

        private void WindowVisible()
        {
            InvisibleAllWindows();
            UpgradeWindow.visible = true;
        }

        private void InvisibleAllWindows()
        {
            foreach (var window in ParentSkillsWindows)
                window.visible = false;
        }
    }
}