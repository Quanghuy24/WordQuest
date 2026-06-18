$files = Get-ChildItem -Path '.' -Recurse -Include *.cs,*.resx
foreach ($file in $files) {
    $content = Get-Content -Path $file.FullName -Raw -Encoding UTF8
    $content = $content -replace 'Topic', 'Chủ đề' -replace 'Topics', 'Các chủ đề' -replace 'Level', 'Mức độ' -replace 'Levels', 'Các mức độ' -replace 'Score', 'Điểm' -replace 'Scores', 'Các điểm' -replace 'Star', 'Sao' -replace 'Stars', 'Các sao' -replace 'Start', 'Bắt đầu' -replace 'Exit', 'Thoát' -replace 'Login', 'Đăng nhập' -replace 'Logout', 'Đăng xuất' -replace 'Admin', 'Quản trị' -replace 'Game', 'Trò chơi' -replace 'Player', 'Người chơi' -replace 'Players', 'Các người chơi'
    Set-Content -Path $file.FullName -Value $content -Encoding UTF8
}
