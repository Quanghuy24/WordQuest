$replacements = @{
    "\bConnectionString\b" = "ChuoiKetNoi"
    "\bGetConnection\b" = "LayKetNoi"
    "\bTestConnection\b" = "KiemTraKetNoi"
    "\bExecuteStoredProcedure\b" = "ThucThiThuTuc"
    "\bExecuteStoredProcedureNonQuery\b" = "ThucThiThuTucKhongTraVe"
    "\bExecuteStoredProcedureScalar\b" = "ThucThiThuTucTraVeDon"
    "\bExecuteStoredProcedureScalarAsync\b" = "ThucThiThuTucTraVeDonBatDongBo"
    "\bExecuteInTransaction\b" = "ThucThiTrongGiaoDich"
    "\bAddParameters\b" = "ThemThamSo"
    "\berrorMessage\b" = "thongBaoLoi"
    "\bspName\b" = "tenThuTuc"
    "\bparameters\b" = "cacThamSo"
    "\btransaction\b" = "giaoDich"
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
Write-Host "Done DatabaseHelper replacement!"
