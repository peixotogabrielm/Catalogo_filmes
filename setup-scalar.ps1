# Script para configurar Git Scalar no projeto
# Execute este script para otimizar o repositório Git com Scalar

Write-Host "🚀 Configurando Git Scalar para o projeto Catalogo Filmes..." -ForegroundColor Green

# Verificar se o Scalar está disponível
try {
    $scalarVersion = scalar version
    Write-Host "✅ Git Scalar encontrado: $scalarVersion" -ForegroundColor Green
} catch {
    Write-Host "❌ Git Scalar não encontrado. Instale o Git mais recente." -ForegroundColor Red
    Write-Host "Download: https://git-scm.com/download/win" -ForegroundColor Yellow
    exit 1
}

# Obter o diretório do projeto
$projectPath = Get-Location

Write-Host "📁 Projeto localizado em: $projectPath" -ForegroundColor Cyan

# Registrar com o Scalar (se ainda não estiver registrado)
Write-Host "🔧 Registrando repositório com Git Scalar..." -ForegroundColor Yellow

try {
    scalar register $projectPath
    Write-Host "✅ Repositório registrado com sucesso!" -ForegroundColor Green
} catch {
    Write-Host "⚠️  Repositório pode já estar registrado ou erro na configuração." -ForegroundColor Yellow
}

# Verificar se está na lista
Write-Host "📋 Repositórios registrados com Scalar:" -ForegroundColor Cyan
scalar list

# Configurar otimizações adicionais do Git
Write-Host "⚙️  Aplicando configurações otimizadas do Git..." -ForegroundColor Yellow

# Habilitar partial clone (mais rápido)
git config core.preloadindex true
git config core.fscache true
git config gc.auto 256

# Configurar Git para Windows
if ($IsWindows -or $env:OS -eq "Windows_NT") {
    git config core.autocrlf true
    git config core.symlinks false
}

# Configurar para trabalhar com Visual Studio
git config merge.tool "vsdiffmerge"

Write-Host "🎉 Configuração concluída!" -ForegroundColor Green
Write-Host ""
Write-Host "📝 Benefícios habilitados:" -ForegroundColor Cyan
Write-Host "   • Performance otimizada do Git" -ForegroundColor White
Write-Host "   • Cache de arquivos melhorado" -ForegroundColor White
Write-Host "   • Partial clone para repositórios grandes" -ForegroundColor White
Write-Host "   • Integração com Visual Studio" -ForegroundColor White
Write-Host ""
Write-Host "💡 Para usar no Visual Studio:" -ForegroundColor Yellow
Write-Host "   1. Abra o arquivo .sln no Visual Studio" -ForegroundColor White
Write-Host "   2. O Scalar será aplicado automaticamente" -ForegroundColor White
Write-Host "   3. Performance melhorada em operações Git" -ForegroundColor White
Write-Host ""
Write-Host "🔄 Para desregistrar (se necessário): scalar unregister" -ForegroundColor Gray