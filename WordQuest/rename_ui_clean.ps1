Set-StrictMode -Version Latest
$projectRoot = Get-Location
# Mapping of English UI strings to Vietnamese equivalents (UTF‑8)
$replacements = @{
    'Add'        = 'Thêm'
    'Edit'       = 'Sửa'
    'Delete'     = 'Xóa'
    'Save'       = 'Lưu'
    'Cancel'     = 'Hủy'
    'Player'     = 'Người chơi'
    'Players'    = 'Người chơi'
    'Topic'      = 'Chủ đề'
    'Topics'     = 'Chủ đề'
    'Level'      = 'Màn'
    'Levels'     = 'Màn'
    'Score'      = 'Điểm'
    'Scores'     = 'Điểm'
    'Star'       = 'Sao'
    'Stars'      = 'Sao'
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
# Process all *.cs and *.resx files in the GUI folder
Get-ChildItem -Path "$projectRoot\GUI" -Recurse -Include *.cs, *.resx | ForEach-Object {
    $path = $_.FullName
    $content = Get-Content -Path $path -Raw -Encoding UTF8
    $original = $content
    foreach ($key in $replacements.Keys) {
        $escapedKey = [regex]::Escape($key)
        $content = $content -replace $escapedKey, $replacements[$key]
    }
    if ($content -ne $original) {
        Set-Content -Path $path -Value $content -Encoding UTF8
        Write-Host "Renamed UI texts in $path"
    }
}
