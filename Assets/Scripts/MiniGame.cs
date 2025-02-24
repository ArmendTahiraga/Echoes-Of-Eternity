public interface MiniGame {
    void StartGame();
    bool GetHasMiniGameStarted();
    bool GetIsCursorNeeded();
    string GetMiniGameResult();
    NPCInteractable GetSuccessInteractable();
    NPCInteractable GetFailInteractable();
}