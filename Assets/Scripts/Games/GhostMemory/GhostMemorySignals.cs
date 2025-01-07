using System;
using Enums;
using Extensions;
using UnityEngine.Events;

namespace Games.GhostMemory
{
    public class GhostMemorySignals : MonoSingleton<GhostMemorySignals>
    {
        public UnityAction<GhostMemoryTile> OnTileSelected;
        public UnityAction OnGameUI;
        public UnityAction OnNextLevelUI;
        public UnityAction OnNextLevel;
        public UnityAction OnRestartLevel;
        public UnityAction OnFailLevel;
        public Func<LevelStatus> OnGetLevelStatus = () => LevelStatus.Complete;
        public Func<int> OnGetChapterIndex = () => 1;
        public Func<int> OnGetDifficulty = () => 0;
        public Func<float> OnGetHidePicturesDelay = () => 2;
    }
}
