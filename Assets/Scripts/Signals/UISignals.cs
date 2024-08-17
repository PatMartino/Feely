using Enums;
using Extensions;
using UnityEngine.Events;

namespace Signals
{
    public class UISignals : MonoSingleton<UISignals>
    {
        public UnityAction<MenuTypes> OnSwitchMenu = delegate {  };
    }
}