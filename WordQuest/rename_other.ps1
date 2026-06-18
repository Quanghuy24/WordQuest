$replacements = @{
    "\bGetAllWords\b" = "LayTatCaTu"
    "\bGetWordByID\b" = "LayTuTheoID"
    "\bInsertWord\b" = "ThemTu"
    "\bUpdateWord\b" = "CapNhatTu"
    "\bDeleteWord\b" = "XoaTu"
    "\bGetWordsByTopicID\b" = "LayTuTheoChuDeID"
    "\bwordID\b" = "idTu"
    "\btopicID\b" = "idChuDe"
    "\blevelID\b" = "idMuc"
    "\bimageID\b" = "idHinh"
    "\bruleID\b" = "idLuat"
    "\bscoreID\b" = "idDiem"
    "\bplayerProgressID\b" = "idTienTrinhNguoiChoi"
    # Topic
    "\bGetAllTopics\b" = "LayTatCaChuDe"
    "\bGetTopicByID\b" = "LayChuDeTheoID"
    "\bInsertTopic\b" = "ThemChuDe"
    "\bUpdateTopic\b" = "CapNhatChuDe"
    "\bDeleteTopic\b" = "XoaChuDe"
    # Level
    "\bGetAllLevels\b" = "LayTatCaMuc"
    "\bGetLevelByID\b" = "LayMucTheoID"
    "\bInsertLevel\b" = "ThemMuc"
    "\bUpdateLevel\b" = "CapNhatMuc"
    "\bDeleteLevel\b" = "XoaMuc"
    # Image
    "\bGetAllImages\b" = "LayTatCaHinh"
    "\bGetImageByID\b" = "LayHinhTheoID"
    "\bInsertImage\b" = "ThemHinh"
    "\bUpdateImage\b" = "CapNhatHinh"
    "\bDeleteImage\b" = "XoaHinh"
    # GameRule
    "\bGetAllRules\b" = "LayTatCaLuat"
    "\bGetRuleByID\b" = "LayLuatTheoID"
    "\bInsertRule\b" = "ThemLuat"
    "\bUpdateRule\b" = "CapNhatLuat"
    "\bDeleteRule\b" = "XoaLuat"
    # ScoreHistory
    "\bGetScoreHistoryByPlayer\b" = "LayLichSuDiemTheoNguoiChoi"
    "\bInsertScoreHistory\b" = "ThemLichSuDiem"
    # PlayerProgress
    "\bGetProgressByPlayer\b" = "LayTienTrinhTheoNguoiChoi"
    "\bUpdateProgress\b" = "CapNhatTienTrinh"
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
Write-Host "Done other DAL/BUS replacement!"
