# Script Việt hoá đợt cuối - BUS/DAL còn sót
$replacements = [ordered]@{
    # ── ImageBUS / ImageDAL ────────────────────────────────────────────────────
    '\bAddImageFromBytes\b'       = 'ThemHinhTuByte'
    '\bGetImageData\b'            = 'LayDuLieuHinh'
    '\bGetImage\b'                = 'LayHinh'
    '\bUpdateImageData\b'         = 'CapNhatDuLieuHinh'
    '\bIsImageInUse\b'            = 'HinhDangDuocDung'
    '\bIsImageNameExists\b'       = 'TenHinhDaTonTai'

    # ── LevelBUS / LevelDAL ───────────────────────────────────────────────────
    '\bGetLevelsByTopic\b'        = 'LayMucTheoChuDe'
    '\bAddLevel\b'                = 'ThemMuc'
    '\bAddWordToLevel\b'          = 'ThemTuVaoMuc'
    '\bRemoveWordFromLevel\b'     = 'XoaTuKhoiMuc'
    '\bClearLevelWords\b'         = 'XoaTatCaTuTrongMuc'
    '\bGetFixedWordsForLevel\b'   = 'LayTuCoDinhChoMuc'

    # ── PlayerBUS / PlayerProgressDAL ─────────────────────────────────────────
    '\bGetOrCreatePlayer\b'       = 'LayHoTaoNguoiChoi'
    '\bGetPlayerProgress\b'       = 'LayTienTrinhNguoiChoi'
    '\bGetAllPlayerProgress\b'    = 'LayToanBoTienTrinh'
    '\bSaveProgress\b'            = 'LuuTienTrinh'
    '\bSaveScoreHistory\b'        = 'LuuLichSuDiem'
    '\bSaveOrUpdateProgress\b'    = 'LuuHoCapNhatTienTrinh'

    # ── TopicBUS / TopicDAL ────────────────────────────────────────────────────
    '\bAddTopic\b'                = 'ThemChuDe'

    # ── GameRuleDAL ───────────────────────────────────────────────────────────
    '\bUpdateGameRule\b'          = 'CapNhatLuatChoi'
    '\bInsertGameRule\b'          = 'ThemLuatChoi'
    '\bHasAnyRule\b'              = 'CoBatKyLuat'
    '\bGetDefaultRule\b'          = 'LayLuatMacDinh'

    # ── DatabaseHelper ────────────────────────────────────────────────────────
    '\bExecuteNonQuery\b'         = 'ThucThiKhongTraVe'
    '\bExecuteScalar\b'           = 'ThucThiTraGiaTri'
    '\bExecuteQuery\b'            = 'ThucThiTraBang'

    # ── Scan leftovers in scanner output that are actually Vietnamese OK ──────
    # (skip - those are already Vietnamese)
}

$files = Get-ChildItem -Path '.' -Recurse -Filter *.cs
foreach ($file in $files) {
    $content = Get-Content -Path $file.FullName -Raw -Encoding UTF8
    $original = $content
    foreach ($key in $replacements.Keys) {
        $content = [regex]::Replace($content, $key, $replacements[$key])
    }
    if ($content -cne $original) {
        Set-Content -Path $file.FullName -Value $content -Encoding UTF8
        Write-Host "Updated: $($file.Name)"
    }
}
Write-Host "`nHoàn thành!"
