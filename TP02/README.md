# TP02 - Sistema de Gestão Portuária

Projeto ASP.NET Core MVC para cadastro de BLs e Containers, com relacionamento obrigatório entre as entidades e persistência em SQLite.

## Executar

No terminal, dentro da pasta `TP02`:

```bash
dotnet restore
dotnet run
```

Abra no navegador o endereço exibido no terminal (normalmente `http://localhost:5247`).

O arquivo `sistema.db` é criado automaticamente na primeira execução. Dois BLs de exemplo também são adicionados inicialmente.

## Funcionalidades

- CRUD completo de BLs e Containers;
- associação obrigatória do Container a um BL;
- validação no navegador, no servidor e no banco;
- bloqueio da exclusão de BL com Containers vinculados;
- menu responsivo e layout Bootstrap;
- relatório imprimível relacionando BLs e Containers.
