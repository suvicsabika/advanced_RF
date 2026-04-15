# A script mappájából dolgozunk.
$workDir = $PSScriptRoot

# A results mappa elérési útja.
$resultsDir = Join-Path $workDir "results"

# Ha nincs results mappa, létrehozzuk.
if (-not (Test-Path $resultsDir)) {
    New-Item -ItemType Directory -Path $resultsDir | Out-Null
}

# Megkeressük a már létezõ run mappákat, pl. run1, run2, run15.
$existingRuns = Get-ChildItem -Path $resultsDir -Directory |
    Where-Object { $_.Name -match '^run(\d+)$' } |
    ForEach-Object { [int]$matches[1] }

# Kiszámoljuk a következõ run azonosítót.
if ($existingRuns.Count -eq 0) {
    $nextRunNumber = 1
} else {
    $nextRunNumber = (($existingRuns | Measure-Object -Maximum).Maximum) + 1
}

$runId = "run$nextRunNumber"

Write-Host "Starting training with run id: $runId"
Write-Host "Working directory: $workDir"

# Az ML-Agents parancs.
$trainCmd = "title ML-Agents Training - $runId && mlagents-learn config.yaml --run-id=$runId"

# A TensorBoard parancs.
$tensorboardCmd = "title TensorBoard - $runId && tensorboard --logdir results --port 6006"

# Külön CMD ablakban elindítjuk a tanítást.
Start-Process -FilePath "cmd.exe" `
    -WorkingDirectory $workDir `
    -ArgumentList "/k", $trainCmd

# Rövid várakozás, hogy a trainer el tudjon indulni.
Start-Sleep -Seconds 2

# Külön CMD ablakban elindítjuk a TensorBoardot.
Start-Process -FilePath "cmd.exe" `
    -WorkingDirectory $workDir `
    -ArgumentList "/k", $tensorboardCmd