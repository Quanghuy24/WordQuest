$replaceMap = @{
    'PlayerID' = 'MaNguoiChoi'
    'Username' = 'TenNguoiChoi'
    'TotalScore' = 'TongDiem'
    'TotalStars' = 'TongSao'
    'DayStreak' = 'NgayStreak'
    'CreatedAt' = 'NgayTao'
    'LastPlayed' = 'LanChoiCuoi'
    'ProgressID' = 'MaTienTien'
    'TopicID' = 'MaChuDe'
    'LevelNum' = 'SoMuc'
    'Stars' = 'Sao'
    'BestScore' = 'DiemCaoNhat'
    'IsCompleted' = 'DaHoanThanh'
    'CompletedAt' = 'ThoiDiemHoanThanh'
    'HistoryID' = 'MaLichSu'
    'Score' = 'Diem'
    'TimeTaken' = 'ThoiGianChoi'
    'PlayedAt' = 'ThoiDiemChoi'
    # Add more if needed
}

$files = Get-ChildItem -Path '.' -Recurse -Filter *.cs
foreach ($file in $files) {
    $content = Get-Content -Path $file.FullName -Raw
    $original = $content
    foreach ($key in $replaceMap.Keys) {
        $value = $replaceMap[$key]
        $content = $content -replace "\b$key\b", $value
    }
    if ($content -ne $original) {
        Set-Content -Path $file.FullName -Value $content -Encoding UTF8
        Write-Host "Renamed DTOs in $($file.Name)"
    }
}
