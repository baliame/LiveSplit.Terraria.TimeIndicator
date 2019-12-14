using LiveSplit.Model;
using System;

namespace LiveSplit.UI.Components
{
    public class TerrariaTimeIndicatorFactory : IComponentFactory
    {
        public string ComponentName => "Terraria Time Indicator";

        public string Description => "Displays the current Terraria run day/night cycle.";

        public ComponentCategory Category => ComponentCategory.Information;

        public IComponent Create(LiveSplitState state) => new TerrariaTimeIndicator();

        public string UpdateName => ComponentName;

        public string XMLURL => "";

        public string UpdateURL => "";

        public Version Version => Version.Parse("1.7.2");
    }
}
