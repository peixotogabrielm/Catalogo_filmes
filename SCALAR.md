# 🚀 Git Scalar - Configuração Otimizada

Este projeto está configurado para usar **Git Scalar** para melhor performance com Git, especialmente no Visual Studio.

## 📋 O que é Git Scalar?

Git Scalar é uma ferramenta que otimiza repositórios Git para melhor performance, especialmente útil para:
- Projetos .NET grandes
- Uso com Visual Studio
- Operações Git mais rápidas
- Cache otimizado de arquivos

## ✅ Status Atual

✅ **Repositório registrado** com Git Scalar  
✅ **Configurações otimizadas** aplicadas  
✅ **Cache de arquivos** habilitado  
✅ **Integração Visual Studio** configurada  

## 🛠️ Configurações Aplicadas

| Configuração | Valor | Descrição |
|--------------|-------|-----------|
| `core.preloadindex` | `true` | Pré-carrega índice para operações mais rápidas |
| `core.fscache` | `true` | Cache do sistema de arquivos |
| `gc.auto` | `256` | Garbage collection otimizado |

## 🎯 Benefícios no Visual Studio

Quando você abrir o projeto no Visual Studio, você terá:

- **✨ Clone mais rápido** de repositórios
- **⚡ Operações Git otimizadas** (commit, pull, push)
- **📁 Navegação mais fluida** em projetos grandes
- **🔄 Sync automático** otimizado
- **💾 Uso eficiente de disco** e memória

## 🚀 Como Usar

### Visual Studio
1. Abra o arquivo `CatalogoFilmes.sln`
2. O Scalar será aplicado **automaticamente**
3. Performance melhorada imediatamente!

### Linha de Comando
```bash
# Verificar status
scalar list

# Ver configurações aplicadas
git config --local --list | findstr "core\|gc"

# Executar manutenção (opcional)
scalar run all
```

## 🔧 Configuração Manual (se necessário)

Se por algum motivo o Scalar não estiver funcionando:

```powershell
# Execute o script de configuração
.\setup-scalar.ps1

# Ou configure manualmente:
scalar register
git config --local core.preloadindex true
git config --local core.fscache true
git config --local gc.auto 256
```

## 📊 Verificar Performance

Para verificar se o Scalar está funcionando:

```bash
# Verificar configurações
git config --local --list

# Ver repositórios registrados
scalar list

# Diagnóstico completo
scalar diagnose
```

## ❌ Desabilitar (se necessário)

Para remover o Scalar do projeto:

```bash
scalar unregister
```

## 📚 Mais Informações

- [Git Scalar Docs](https://github.com/microsoft/scalar)
- [Visual Studio Git Integration](https://docs.microsoft.com/visualstudio/version-control/)
- [Git Performance Tips](https://git-scm.com/docs/git-config)

---

💡 **Dica**: O Scalar é especialmente útil em projetos .NET com muitos arquivos binários e dependências!