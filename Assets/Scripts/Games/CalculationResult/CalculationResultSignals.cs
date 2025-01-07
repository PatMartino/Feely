using System;
using Enums;
using Extensions;
using UnityEngine.Events;

namespace Games.CalculationResult
{
    public class CalculationResultSignals : MonoSingleton<CalculationResultSignals>
    {
        public Action<int,int, string,int> OnSetTexts = delegate {  };
        public UnityAction<bool> OnCheckIsCorrect = delegate {  };
        public Func<LevelStatus> OnGetLevelStatus = () => LevelStatus.Complete;
        public UnityAction OnGameUI = delegate {  };
        public UnityAction OnNextLevelUI = delegate {  };
        public UnityAction OnNextLevel= delegate {  };
        public UnityAction OnLevelFailed= delegate {  };
        public Func<int> OnGetLevelID = () => 0;
    }
}