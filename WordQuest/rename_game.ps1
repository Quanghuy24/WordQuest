# Script Việt hoá đợt cuối - AnswerManagerBUS + GameBUS còn sót
$replacements = [ordered]@{
    # ── AnswerManagerBUS ──────────────────────────────────────────────────────
    '\bInitialize\b'              = 'KhoiTao'
    '\bGenerateLetters\b'         = 'TaoChuCai'
    '\bGetAvailableLetters\b'     = 'LayChuCaiCoThe'
    '\bGetFilledLetters\b'        = 'LayChuCaiDaDien'
    '\bRemoveFromIndex\b'         = 'XoaTuViTri'
    '\bIsFull\b'                  = 'DayO'
    '\bGetCorrectAnswer\b'        = 'LayDapAnDung'

    # ── GameBUS ───────────────────────────────────────────────────────────────
    '\bNextWord\b'                = 'TuTiepTheo'
    '\bGetCurrentInput\b'         = 'LayNhapHienTai'
    '\bCancelGame\b'              = 'HuyGame'
    '\bGetQuestionText\b'         = 'LayCauHoi'
    '\bGenerateShuffledLetters\b' = 'TaoChuCaiNgauNhien'
    '\bHandleCorrectAnswer\b'     = 'XuLyDapAnDung'
    '\bHandleWrongAnswer\b'       = 'XuLyDapAnSai'
    '\bHandleTimeOut\b'           = 'XuLyHetGio'
    '\bEndGame\b'                 = 'KetThucGame'
    '\bCalculateStars\b'          = 'TinhSoSao'
    '\bComputeStars\b'            = 'TinhSaoTuDiem'
    '\bResetTimer\b'              = 'DatLaiDongHo'
    '\bBuildLivesDisplay\b'       = 'TaoHienThiMang'

    # ── GameRuleBUS ───────────────────────────────────────────────────────────
    '\bSaveGameRule\b'            = 'LuuLuatChoi'

    # ── Events trong GameBUS ──────────────────────────────────────────────────
    '\bOnStateChanged\b'          = 'KhiTrangThaiThayDoi'
    '\bOnTimeOut\b'               = 'KhiHetGio'
    '\bOnGameEnded\b'             = 'KhiGameKetThuc'
    '\bOnNextWordRequested\b'     = 'KhiYeuCauTuTiep'
    '\bOnScoreChanged\b'          = 'KhiDiemThayDoi'
    '\bOnAnswerCompleted\b'       = 'KhiTraLoiXong'
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
