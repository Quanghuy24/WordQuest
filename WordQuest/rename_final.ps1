# Script cuối: Việt hoá toàn bộ hàm còn tiếng Anh

$replacements = [ordered]@{
    # ── DAL methods ──────────────────────────────────────────────────────────
    '\bDeleteProgressByPlayer\b'  = 'XoaTienTrinhTheoNguoiChoi'
    '\bDeleteProgressByTopic\b'   = 'XoaTienTrinhTheoChuDe'
    '\bGetLeaderboard\b'          = 'LayBangXepHang'
    '\bDeleteScoreHistoryByPlayer\b' = 'XoaLichSuDiemTheoNguoiChoi'
    '\bGetTotalGames\b'           = 'LayTongSoGame'
    '\bGetParentTopics\b'         = 'LayChuDeCha'
    '\bHasChildren\b'             = 'CoChuDeCon'
    '\bMapToWordDTO\b'            = 'ChuyenSangWordDTO'

    # ── DTO methods ──────────────────────────────────────────────────────────
    '\bClone\b'                   = 'NhanBan'

    # ── frmAdmin private methods ──────────────────────────────────────────────
    '\bSetupControls\b'           = 'KhoiTaoControls'
    '\bExportToCSV\b'             = 'XuatRaCSV'
    '\bRenderPage\b'              = 'VeGiaoTrang'
    '\bColorDifficultyCell\b'     = 'ToMauODoKho'
    '\bEnableEditControls\b'      = 'BatTatControls'
    '\bValidateInput\b'           = 'KiemTraNhapLieu'
    '\bClearInput\b'              = 'XoaNhapLieu'
    '\bSelectRowByWordID\b'       = 'ChonHangTheoMaTu'
    '\bHighlightRow\b'            = 'LamNoiHang'
    '\bSetPreviewImage\b'         = 'DatAnhXemTruoc'

    # ── frmAdminPlayers ──────────────────────────────────────────────────────
    '\bSetupUI\b'                 = 'KhoiTaoUI'
    '\bSetupDataGridView\b'       = 'KhoiTaoBangDuLieu'

    # ── frmAdminRules ─────────────────────────────────────────────────────────
    '\bSetDefaultValues\b'        = 'DatGiaTriMacDinh'
    '\bValidateRules\b'           = 'KiemTraLuatChoi'

    # ── frmAdminTopics ────────────────────────────────────────────────────────
    '\bUpdateWordInfo\b'          = 'CapNhatThongTinTu'
    '\bSelectTopicNodeByID\b'     = 'ChonNutChuDeTheoID'
    '\bFindNodeByID\b'            = 'TimNutTheoID'
    '\bSetModeStyle\b'            = 'DatKieuCheDo'
    '\bSetLevelEditVisible\b'     = 'DatHienThiChinhSuaMuc'
    '\bClearLevelForm\b'          = 'XoaFormMuc'

    # ── frmGame ───────────────────────────────────────────────────────────────
    '\bUpdateUI\b'                = 'CapNhatUI'
    '\bStartNextWordTimer\b'      = 'BatDauHenGioTuTiep'
    '\bSetupForm\b'               = 'KhoiTaoForm'
    '\bBuildAnswerSlots\b'        = 'TaoODienDapAn'
    '\bBuildLetterButtons\b'      = 'TaoNutChuCai'
    '\bHighlightAllSlots\b'       = 'LamNoiTatCaO'
    '\bRenderEmoji\b'             = 'VeBieuTuong'
    '\bInitializeSpeech\b'        = 'KhoiTaoGiongNoi'
    '\bSpeakCurrentWord\b'        = 'DocTuHienTai'
    '\bUpdateTimerColor\b'        = 'CapNhatMauDongHo'
    '\bPlayCorrectSound\b'        = 'PhatAmThanhDung'
    '\bPlayWrongSound\b'          = 'PhatAmThanhSai'
    '\bCleanupResources\b'        = 'DonDepTaiNguyen'

    # ── frmImage ─────────────────────────────────────────────────────────────
    '\bShowTab\b'                 = 'HienThiTab'
    '\bSelectListViewItemByID\b'  = 'ChonHangDanhSachTheoID'

    # ── frmLevels ────────────────────────────────────────────────────────────
    '\bRenderLevelPage\b'         = 'VeGiaoMuc'
    '\bGetLabelIndex\b'           = 'LayViTriNhan'
    '\bUpdateLevelPageButtons\b'  = 'CapNhatNutGiaoMuc'

    # ── frmLogin ─────────────────────────────────────────────────────────────
    '\bPromptPassword\b'          = 'HoiMatKhau'

    # ── frmTopics ─────────────────────────────────────────────────────────────
    '\bRefreshTopics\b'           = 'LamMoiChuDe'

    # ── frmWin ────────────────────────────────────────────────────────────────
    # SetupUI already handled

    # ── frmLeaderboard ────────────────────────────────────────────────────────
    # SetupUI already handled
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
Write-Host "`nHoàn thành Việt hoá hàm còn lại!"
