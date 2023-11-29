//Nome do Grupo: Marllon Filipe, Juan Diego, Lucas Ramos, Mauro Victor 

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

struct Produto
{
    public string Nome { get; set; }
    public double Preco { get; set; }
    public DateTime Validade { get; set; }
    public int Quantidade { get; set; }
}

struct Venda
{
    public string NomeProduto;
    public int Quantidade;
    public double TotalVenda;
    public int OpcaoPagamento;

    public Venda(string nomeProduto, int quantidade, double totalVenda, int opcaoPagamento)
    {
        NomeProduto = nomeProduto;
        Quantidade = quantidade;
        TotalVenda = totalVenda;
        OpcaoPagamento = opcaoPagamento;
    }
}

class Program
{
    static List<Produto> produtos = new List<Produto>();
    static List<Venda> vendas = new List<Venda>();
    static double totalVendas = 0;
    static string nomeEmpresa = "";
    static string nomeComerciante = "";
    static string chavePix = "";

    static void Main()
    {
        Console.WriteLine("Digite o nome da empresa:");
        nomeEmpresa = Console.ReadLine();

        Console.WriteLine("Digite o nome do comerciante:");
        nomeComerciante = Console.ReadLine();

        Console.WriteLine("Digite a chave Pix:");
        chavePix = Console.ReadLine();

        int opcao = 0;

        while (opcao != 6)
        {
            Console.WriteLine($"Seja Bem Vindo {nomeEmpresa}");
            Console.WriteLine("1. Cadastrar Produto");
            Console.WriteLine("2. Realizar Venda");
            Console.WriteLine("3. Controle de Estoque");
            Console.WriteLine("4. Relatório de Vendas");
            Console.WriteLine("5. Atualizar Cadastro");
            Console.WriteLine("6. Sair");

            opcao = Convert.ToInt32(Console.ReadLine());

            switch (opcao)
            {
                case 1:
                    CadastrarProduto();
                    break;
                case 2:
                    RealizarVenda();
                    break;
                case 3:
                    ControleEstoque();
                    break;
                case 4:
                    RelatorioVendas();
                    break;
                case 5:
                    AtualizarCadastro();
                    break;
                case 6:
                    Console.WriteLine("Saindo do programa.");
                    break;
                default:
                    Console.WriteLine("Opção inválida. Tente novamente.");
                    break;
            }
        }

        // Salva os dados em arquivos
        SalvarFluxoDeCaixaEmTXT();
        SalvarInventarioEmTXT();
        SalvarDadosEmBIN();
    }

    private static void SalvarFluxoDeCaixaEmTXT()
    {
        throw new NotImplementedException();
    }

    private static void SalvarDadosEmBIN()
    {
        throw new NotImplementedException();
    }

    private static void SalvarInventarioEmTXT()
    {
        throw new NotImplementedException();
    }

    static void CadastrarProduto()
    {
        Produto produto = new Produto();

        Console.WriteLine("Digite o nome do produto:");
        produto.Nome = Console.ReadLine();

        Console.WriteLine("Digite o preço do produto:");
        produto.Preco = Convert.ToDouble(Console.ReadLine());

        Console.WriteLine("O produto tem validade? (S/N)");
        char temValidade = Console.ReadKey().KeyChar;
        Console.WriteLine();

        produto.Validade = DateTime.MinValue;

        if (char.ToUpper(temValidade) == 'S')
        {
            Console.WriteLine("Digite a data de validade do produto (dd/MM/yyyy):");
            produto.Validade = Convert.ToDateTime(Console.ReadLine());
        }

        Console.WriteLine("Digite a quantidade em estoque do produto:");
        produto.Quantidade = Convert.ToInt32(Console.ReadLine());

        produtos.Add(produto);

        Console.WriteLine("Produto cadastrado com sucesso!");
    }

    static void RealizarVenda()
    {
        Console.WriteLine("Digite o número de produtos diferentes a serem vendidos:");
        int numProdutos = Convert.ToInt32(Console.ReadLine());

        double totalVenda = 0;

        for (int i = 0; i < numProdutos; i++)
        {
            Console.WriteLine($"Produto {i + 1}:");

            Console.WriteLine("Digite o nome do produto vendido:");
            string nomeProduto = Console.ReadLine();

            int ProdutoVendidoIndex = produtos.FindIndex(p => p.Nome == nomeProduto);

            if (ProdutoVendidoIndex != -1)
            {
                Produto produtoVendido = produtos[ProdutoVendidoIndex];

                Console.WriteLine($"Digite a quantidade vendida do produto {nomeProduto}:");
                int quantidadeVendida = Convert.ToInt32(Console.ReadLine());

                if (produtoVendido.Quantidade >= quantidadeVendida)
                {
                    Console.WriteLine("Selecione a opção de pagamento:");
                    Console.WriteLine("1. Dinheiro");
                    Console.WriteLine("2. Cartão de Crédito");
                    Console.WriteLine("3. Cartão de Débito");
                    Console.WriteLine("4. Pix");

                    int opcaoPagamento = Convert.ToInt32(Console.ReadLine());

                    double subtotalVenda = quantidadeVendida * produtoVendido.Preco;

                    totalVenda += subtotalVenda;

                    Venda venda = new Venda(nomeProduto, quantidadeVendida, subtotalVenda, opcaoPagamento);
                    vendas.Add(venda);

                    produtoVendido.Quantidade -= quantidadeVendida;
                    produtos[ProdutoVendidoIndex] = produtoVendido;

                    Console.WriteLine($"Venda do produto {nomeProduto} realizada com sucesso! Subtotal: {subtotalVenda}");
                }
                else
                {
                    Console.WriteLine($"Quantidade insuficiente em estoque para o produto {nomeProduto}.");
                    i--; // Volta uma iteração para que o usuário insira novamente a quantidade para o mesmo produto
                }
            }
            else
            {
                Console.WriteLine($"Produto {nomeProduto} não encontrado.");
                i--; // Volta uma iteração para que o usuário insira novamente o nome do produto
            }
        }

        Console.WriteLine($"Venda total realizada com sucesso! Valor do pedido: {totalVenda}");
    }

    static void ControleEstoque()
    {
        Console.WriteLine("Controle de Estoque:");

        foreach (Produto produto in produtos)
        {
            Console.WriteLine($"{produto.Nome} - Quantidade: {produto.Quantidade} - {(produto.Validade != DateTime.MinValue ? $"Validade: {produto.Validade.ToString("dd/MM/yyyy")}" : "Sem validade")}");
        }
    }

    static void RelatorioVendas()
    {
        Console.WriteLine("Relatório de Vendas:");

        foreach (Venda venda in vendas)
        {
            Console.WriteLine($"Produto: {venda.NomeProduto}, Quantidade: {venda.Quantidade}, Valor: {venda.TotalVenda}, Opção de Pagamento: {ObterDescricaoOpcaoPagamento(venda.OpcaoPagamento)}");
        }

        totalVendas = vendas.Sum(venda => venda.TotalVenda);  // Calcular o total de vendas

        Console.WriteLine($"Total de Vendas Registradas: {totalVendas}");

        // Baixar relatório em arquivo .txt na área de trabalho
        SalvarRelatorioVendasEmTXT();
    }

    static void SalvarRelatorioVendasEmTXT()
    {
        string path = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);

        string relatorioPath = Path.Combine(path, "RelatorioVendas.txt");

        using (StreamWriter sw = new StreamWriter(relatorioPath))
        {
            sw.WriteLine("Relatório de Vendas:");

            foreach (Venda venda in vendas)
            {
                sw.WriteLine($"Produto: {venda.NomeProduto}, Quantidade: {venda.Quantidade}, Valor: {venda.TotalVenda}, Opção de Pagamento: {ObterDescricaoOpcaoPagamento(venda.OpcaoPagamento)}");
            }

            sw.WriteLine($"Total de Vendas Registradas: {totalVendas}");
        }

        Console.WriteLine($"Relatório de Vendas salvo em {relatorioPath}");
    }

    private static string ObterDescricaoOpcaoPagamento(int opcaoPagamento)
    {
        switch (opcaoPagamento)
        {
            case 1:
                return "Dinheiro";
            case 2:
                return "Cartão de Crédito";
            case 3:
                return "Cartão de Débito";
            case 4:
                return "Pix";
            default:
                return "Opção inválida";
        }
    }

    static void AtualizarCadastro()
    {
        Console.WriteLine("Atualizar Cadastro:");
        Console.WriteLine("1. Atualizar Nome da Empresa");
        Console.WriteLine("2. Atualizar Nome do Comerciante");
        Console.WriteLine("3. Atualizar Chave Pix");

        int opcao = Convert.ToInt32(Console.ReadLine());

        switch (opcao)
        {
            case 1:
                Console.WriteLine("Digite o novo nome da empresa:");
                nomeEmpresa = Console.ReadLine();
                break;
            case 2:
                Console.WriteLine("Digite o novo nome do comerciante:");
                nomeComerciante = Console.ReadLine();
                break;
            case 3:
                Console.WriteLine("Digite a nova chave Pix:");
                chavePix = Console.ReadLine();
                break;
            default:
                Console.WriteLine("Opção inválida. Tente novamente.");
                break;
        }
    }
}