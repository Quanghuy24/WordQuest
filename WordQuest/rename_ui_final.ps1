$replacements = @{
    'Add'        = 'Thêm'
    'Edit'       = 'Sửa'
    'Delete'     = 'Xóa'
    'Save'       = 'Lưu'
    'Cancel'     = 'Hủy'
    'Player'     = 'Người chơi'
    'Players'    = 'Các người chơi'
    'Topic'      = 'Chủ đề'
    'Topics'     = 'Các chủ đề'
    'Level'      = 'Mức độ'
    'Levels'     = 'Các mức độ'
    'Score'      = 'Điểm'
    'Scores'     = 'Các điểm'
    'Star'       = 'Sao'
    'Stars'      = 'Các sao'
    'Game Over'  = 'Kết thúc game'
    'Win'        = 'Chiến thắng'
    'Loss'       = 'Thua'
    'Start'      = 'Bắt đầu'
    'Exit'       = 'Thoát'
    'Login'      = 'Đăng nhập'
    'Logout'     = 'Đăng xuất'
    'Admin'      = 'Quản trị'
    'Admin Menu' = 'Menu Quản trị'
    'Leaderboard'= 'Bảng xếp hạng'
    'Settings'   = 'Cài đặt'
    'Language'   = 'Ngôn ngữ'
    'Help'       = 'Trợ giúp'
    'About'      = 'Giới thiệu'
    'Confirm'    = 'Xác nhận'
    'Are you sure?' = 'Bạn có chắc không?'
    'Error'      = 'Lỗi'
    'Warning'    = 'Cảnh báo'
    'Information'= 'Thông tin'
}

Get-ChildItem -Path '.' -Recurse -Include *.cs,*.resx | ForEach-Object {
    $path = $_.FullName
    $content = Get-Content -Path $path -Raw -Encoding UTF8
    $original = $content
    foreach ($key in $replacements.Keys) {
        $escapedKey = [regex]::Escape($key)
        $content = $content -replace $escapedKey, $replacements[$key]
    }
    if ($content -ne $original) {
        Set-Content -Path $path -Value $content -Encoding UTF8
        Write-Host "Updated $path"
    }
}
Cách fix nhanh (Khuyến nghị)
Bước 1: Thêm vào class GameStateDTO (file GameDTOs.cs):
C#public int MaxHintsPerWord { get; set; } = 3;
public int HintsUsedThisWord { get; set; } = 0;
Bước 2: Sửa hàm SuDungGoiY() trong GameBUS.cs:
C#public HintResultDTO SuDungGoiY()
{
    lock (_lockObject)
    {
        if (_state.IsAnswered || _state.IsGameOver || _state.IsWaitingForNext)
            return null;

        // === THÊM KIỂM TRA GIỚI HẠN ===
        if (_state.HintsUsedThisWord >= _state.MaxHintsPerWord)
            return null;

        int nextIndex = _state.CurrentInput.Count;
        if (nextIndex >= _state.CorrectAnswer.Length)
            return null;

        char hintChar = _state.CorrectAnswer[nextIndex];

        _state.CurrentInput.Add(hintChar);
        RemoveLetterFromAvailable(hintChar);
        _state.Score = Math.Max(0, _state.Score - _rule.HintPenalty);
        
        _state.HintsUsedThisWord++;     // ← Quan trọng

        // ... phần còn lại giữ nguyên
    }
}
Bước 3: Trong frmGame.cs, thêm hàm cập nhật nút Hint:
C#private void UpdateHintButtonState()
{
    var state = _gameBUS.LayTrangThaiHienTai();
    bool canUse = state.HintsUsedThisWord < state.MaxHintsPerWord 
                  && !state.IsAnswered;

    btnHint.Enabled = canUse;
    btnHint.Text = canUse ? $"Gợi ý ({state.MaxHintsPerWord - state.HintsUsedThisWord})" : "Hết gợi ý";
}