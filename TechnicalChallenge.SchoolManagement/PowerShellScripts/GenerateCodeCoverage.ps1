# Rutas de salida
$coverageOutputFolder = Join-Path (Get-Location) "Coverage"
$coverageReportFolder = Join-Path (Get-Location) "CoverageReport"

# Limpia las carpetas de cobertura y reportes anteriores
if (Test-Path $coverageOutputFolder) {
    Remove-Item -Recurse -Force $coverageOutputFolder
}
if (Test-Path $coverageReportFolder) {
    Remove-Item -Recurse -Force $coverageReportFolder
}

# Crear las carpetas de cobertura y reporte si no existen
New-Item -ItemType Directory -Force -Path $coverageOutputFolder | Out-Null
New-Item -ItemType Directory -Force -Path $coverageReportFolder | Out-Null

# Obtener la lista de directorios, excluyendo Coverage y CoverageReport
$testProjectDirectories = Get-ChildItem -Recurse -Directory | Where-Object {
    $_.Name -like "*.Tests" -and
    $_.FullName -notmatch "\\Coverage\\" -and
    $_.FullName -notmatch "\\CoverageReport\\"
}

# Ejecutar pruebas con cobertura en todos los proyectos dentro de carpetas *.Tests
foreach ($directory in $testProjectDirectories) {
    # Buscar archivos *.csproj dentro de cada carpeta *.Tests
    $projectFile = Get-ChildItem -Path $directory.FullName -Filter *.csproj | Select-Object -First 1

    if ($null -eq $projectFile) {
        Write-Warning "No se encontró un archivo .csproj en la carpeta $($directory.FullName)."
        continue
    }

    Write-Host "Ejecutando pruebas para $($projectFile.FullName)..."

    # Ruta específica para la carpeta de cobertura del proyecto (ruta absoluta)
    $projectCoverageFolder = Join-Path $coverageOutputFolder $directory.Name
    $projectCoverageFolderFullPath = [System.IO.Path]::GetFullPath($projectCoverageFolder)

    # Crear la carpeta de cobertura del proyecto si no existe
    New-Item -ItemType Directory -Force -Path $projectCoverageFolderFullPath | Out-Null

    try {
        # Ejecuta las pruebas con cobertura usando la ruta absoluta
        dotnet test $projectFile.FullName --no-build `
            /p:CollectCoverage=true `
            "/p:CoverletOutput=$projectCoverageFolderFullPath\" `
            /p:CoverletOutputFormat=opencover
    } catch {
        Write-Warning "Error al ejecutar pruebas para $($projectFile.FullName). Continuando con el siguiente proyecto..."
        continue
    }

    # Verificar si el archivo de cobertura existe
    $coverageFile = Get-ChildItem -Path $projectCoverageFolderFullPath -Filter *.xml -ErrorAction SilentlyContinue
    if (-not $coverageFile) {
        Write-Warning "No se encontró archivo de cobertura para $($projectFile.FullName). Continuando con el siguiente proyecto..."
    } else {
        Write-Host "Archivo de cobertura generado para $($projectFile.FullName): $($coverageFile.FullName)"
    }
}

# Combinar todos los archivos de cobertura en un solo reporte
Write-Host "Generando reporte HTML combinado..."

# Recopilar rutas de archivos de cobertura existentes
$coverageFiles = Get-ChildItem -Path $coverageOutputFolder -Recurse -Filter *.xml -ErrorAction SilentlyContinue

if ($coverageFiles.Count -eq 0) {
    Write-Warning "No se encontraron archivos de cobertura para combinar. Verifica que las pruebas generen cobertura."
} else {
    # Construir la lista de archivos para ReportGenerator
    $reportArgs = ($coverageFiles | ForEach-Object { $_.FullName }) -join ";"
    # O puede usar directamente:
    # $reportArgs = $coverageFiles.FullName -join ";"

    try {
        # Ejecutar ReportGenerator con las rutas recopiladas
        reportgenerator -reports:$reportArgs `
                        -targetdir:$coverageReportFolder `
                        -reporttypes:Html
        Write-Host "Reporte combinado generado en $coverageReportFolder\index.html"
    } catch {
        Write-Warning "No se pudo generar el reporte combinado. Asegúrate de que los archivos de cobertura sean válidos."
    }
}
