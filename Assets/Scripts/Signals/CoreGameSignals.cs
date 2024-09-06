using System;
using Enums;
using Extensions;
using UnityEngine.Events;

namespace Signals
{
    public class CoreGameSignals : MonoSingleton<CoreGameSignals>
    {
        public UnityAction<GamesAndTestsNames> OnGameManagement = delegate {  };
        public Func<float> OnGetRemainingTime = () => 0;
        public UnityAction<float> OnStartTimer = delegate {  };
        public UnityAction OnStopTimer = delegate {  };
    }
}