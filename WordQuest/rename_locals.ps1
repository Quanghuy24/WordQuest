# Script bổ sung: Việt hoá biến cục bộ và tham số còn sót lại
$replacements = [ordered]@{
    # ── Local variables & parameters trong GUI / DAL ──────────────────────────
    '\btopicName\b'         = 'tenChuDe'
    '\btopicIcon\b'         = 'bieuTuongChuDe'
    '\benglishWord\b'       = 'tuTiengAnh'
    '\bisDuplicate\b'       = 'laTrung'
    '\blevelName\b'         = 'tenMuc'
    '\bquestionCount\b'     = 'soCauHoi'
    '\bparentID\b'          = 'maChuDeCha'
    '\bparentItem\b'        = 'mucCha'
    '\bgameMode\b'          = 'cheDoCHoi'
    '\bisCorrect\b'         = 'laDung'
    '\bcorrectAnswer\b'     = 'dapAnDung'
    '\bcurrentInput\b'      = 'nhapHienTai'
    '\blives\b'             = 'soMang'
    '\bmaxLives\b'          = 'soMangToiDa'
    '\bkeyword\b'           = 'tuKhoa'
    '\bimageFilter\b'       = 'boDieuHoa'
    '\bdifficultyLevel\b'   = 'doKho'
    '\bexcludeWordID\b'     = 'truMaTu'
    '\btotalQuestions\b'    = 'tongCauHoi'
    '\bcorrectCount\b'      = 'soCauDung'
    '\bisAdmin\b'           = 'laAdmin'
    '\bstarsNeeded\b'       = 'saoCan'
    '\bisCorrectStr\b'      = 'laДungStr'
    # ── DAL parameter names ───────────────────────────────────────────────────
    '\bGetChildTopics\b'    = 'LayChuDeConTheo'
    # ── GUI method helpers ────────────────────────────────────────────────────
    '\bShowCorrectAnswer\b' = 'HienThiDapAnDung'
    '\bGetLivesDisplay\b'   = 'LayHienThiMang'
    '\bUpdateAnswerSlotsText\b' = 'CapNhatODienChuCai'
    '\bUpdateLevelPanelUI\b'    = 'CapNhatGiaoMucUI'
    '\bOnAnswerCompleted\b'  = 'KhiTraLoiXong'
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
Write-Host "`nHoàn thành Việt hoá biến cục bộ và tham số!"
