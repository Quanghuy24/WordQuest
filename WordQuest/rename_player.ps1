$replacements = @{
    "\bGetAllPlayers\b" = "LayTatCaNguoiChoi"
    "\bGetPlayerByID\b" = "LayNguoiChoiTheoID"
    "\bGetPlayerByUsername\b" = "LayNguoiChoiTheoTen"
    "\bInsertPlayer\b" = "ThemNguoiChoi"
    "\bUpdatePlayerStats\b" = "CapNhatChiSoNguoiChoi"
    "\bUpdatePlayerStreak\b" = "CapNhatChuoiNgayNguoiChoi"
    "\bUpdateDayStreak\b" = "CapNhatChuoiNgay"
    "\bCalculateNewStreak\b" = "TinhChuoiNgayMoi"
    "\bUpdateStreakInDB\b" = "CapNhatChuoiNgayVaoCSDL"
    "\bDeletePlayer\b" = "XoaNguoiChoi"
    "\bResetPlayerStats\b" = "DatLaiChiSoNguoiChoi"
    "\bGetTotalPlayers\b" = "LayTongSoNguoiChoi"
    "\bGetTopScore\b" = "LayDiemCaoNhat"
    "\bGetTopStreak\b" = "LayChuoiNgayDaiNhat"
    "\bMapToPlayerDTO\b" = "ChuyenSangPlayerDTO"
    
    "\bplayerID\b" = "idNguoiChoi"
    "\busername\b" = "tenDangNhap"
    "\bscoreEarned\b" = "diemKiemDuoc"
    "\bstarsEarned\b" = "saoKiemDuoc"
    "\bnewStreak\b" = "chuoiNgayMoi"
    "\bcurrentStreak\b" = "chuoiNgayHienTai"
    "\blastPlayed\b" = "lanChoiCuoi"
    "\btoday\b" = "homNay"
    "\bdaysDiff\b" = "khoangCachNgay"
    "\bdt\b" = "bangDuLieu"
    "\brow\b" = "dong"
    "\bresult\b" = "ketQua"
    "\bisNewDay\b" = "laNgayMoi"
}

$files = Get-ChildItem -Path '.' -Recurse -Filter *.cs
foreach ($file in $files) {
    $content = Get-Content -Path $file.FullName -Raw
    $original = $content
    foreach ($key in $replacements.Keys) {
        $content = [regex]::Replace($content, $key, $replacements[$key])
    }
    if ($content -cne $original) {
        Set-Content -Path $file.FullName -Value $content -Encoding UTF8
        Write-Host "Updated $($file.Name)"
    }
}
Write-Host "Done PlayerDAL and PlayerBUS replacement!"
