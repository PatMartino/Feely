using System;
using Enums;
using Extensions;
using UnityEngine.Events;

namespace Signals
{
    public class MathFundamentalSignals : MonoSingleton<MathFundamentalSignals>
    {
        public Action<int,int, string, int,int,int,int> OnSetTexts = delegate {  };
        public UnityAction<int> OnCheckIsCorrect = delegate {  };
        public Func<LevelStatus> OnGetLevelStatus = () => LevelStatus.Complete;
        public UnityAction OnGameUI = delegate {  };
        public UnityAction OnNextLevelUI = delegate {  };
        public UnityAction OnNextLevel= delegate {  };
        public UnityAction OnLevelFailed= delegate {  };
        public Func<int> OnGetLevelID = () => 0;
    }
}