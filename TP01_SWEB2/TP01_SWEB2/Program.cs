using System;
using TP01_SWEB2.Negócio;

public class Program
{
    public static void Main()
    {
        Console.WriteLine("=== INICIANDO CLASSE DE TESTES ===\n");

        // 1. Criando uma instância com mais de um autor (Conforme item C)
        Author[] autoresDoLivro = new Author[]
        {
            new Author("Mauricio", "mauricio@email.com", "M"),
            new Author("Ana", "ana@email.com", "F")
        };

        // Instanciando o Livro utilizando o construtor completo
        Book meuLivro = new Book("C# Orientado a Objetos", autoresDoLivro, 89.90, 15);

        // 2. Demonstrando o uso de TODOS os métodos da classe Book

        // Teste: getName(): String
        Console.WriteLine($"[Método getName()]: Nome do livro = {meuLivro.getName("a")}");

        // Teste: getAuthors(): Author[]
        Author[] autoresRecuperados = meuLivro.GetAuthors();
        Console.WriteLine($"[Método getAuthors()]: Quantidade de autores retornados = {autoresRecuperados.Length}");

        // Teste: getPrice(): double
        Console.WriteLine($"[Método getPrice()]: Preço original = R$ {meuLivro.getPrice():F2}");

        // Teste: setPrice(price: double): void
        meuLivro.setPrice(119.50);
        Console.WriteLine($"[Método setPrice()]: Preço atualizado para = R$ {meuLivro.getPrice():F2}");

        // Teste: getQty(): int
        Console.WriteLine($"[Método getQty()]: Quantidade original em estoque = {meuLivro.getQty()}");

        // Teste: setQty(qty: int): void
        meuLivro.setQty(32);
        Console.WriteLine($"[Método setQty()]: Quantidade atualizada para = {meuLivro.getQty()}");

        // Teste: getAuthorNames(): String
        Console.WriteLine($"[Método getAuthorNames()]: Nomes obtidos = {meuLivro.getAuthorNames()}");

        // Teste: toString(): String
        Console.WriteLine("\n[Método toString()]: Resultado da string formatada:");
        Console.WriteLine(meuLivro.ToString());

        Console.WriteLine("\n=== FIM DOS TESTES ===");
    }
}
