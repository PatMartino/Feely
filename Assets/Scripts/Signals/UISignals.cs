using Enums;
using Extensions;
using UnityEngine.Events;

namespace Signals
{
    public class UISignals : MonoSingleton<UISignals>
    {
        public UnityAction<MenuTypes> OnSwitchMenu = delegate {  };
        public UnityAction<SettingButtonsType> OnSettingsPanelController = delegate {  };
        public UnityAction<UIStates> OnMenuUIManagement = delegate {  };
        public UnityAction OnUpdateBallSortLevelIDText = delegate {  };
        public UnityAction OnCompleteBallSortLevel = delegate {  };
        public UnityAction OnStartBallSortLevel = delegate {  };
        public UnityAction OnHome = delegate {  };
        public UnityAction OnGame = delegate {  };
        public UnityAction OnTest = delegate {  };
        public UnityAction<MenuTypes> OnChangeHeaderText = delegate {  };
        public UnityAction<MenuTypes> OnChangeMenuIconColor = delegate {  };
    }
}