using MauiAppMinhasCompras.Models;
using SQLite;

namespace MauiAppMinhasCompras.Helpers
{
    public class SQLiteDatabaseHelper
    {
        //Responsavel por gerenciar a conecção entre o arquivo SQLite, conn é apenas o nome dado a essa conecção, como uma variavel
        readonly SQLiteAsyncConnection _conn;

        // string= caminha. patch= nome do caminho. Caminho para o arquivo
        public SQLiteDatabaseHelper(string patch) 
        {
            //_conn agora esta recebendo um objeto relacionado com onde está os dados
            _conn = new SQLiteAsyncConnection(patch);

            //Cria a tabela produto no arquivo
            _conn.CreateTableAsync<Produto>().Wait();

        }

        //publico, Retorna inteiro, nome do método (método?, parametro)
        //Inserir
        public Task<int> Insert(Produto p) 
        {
            //Retorne, conecção, Interir não espera, 
            return _conn.InsertAsync(p);
        }

        //Atualizar
        public Task<List<Produto>> Update(Produto p) 
        {
            //caminha sql = atualizar produto,Insere Descrição, Quantidade, Preco, Onde Id 
            string sql = "UPDATE Produto SET Descricao=?, Quantidade =?, Preco=? WHERE Id=?";

            return _conn.QueryAsync<Produto>(

                sql, p.Descricao, p.Quantidade, p.Preco, p.Id
                );
        }

        //Deletar
        public Task<int> Delete(int id) 
        {
            return _conn.Table<Produto>().DeleteAsync(i => i.Id == id);
        }

        //Listar produtos
        public Task<List<Produto>> GetAll() 
        {
            return _conn.Table<Produto>().ToListAsync();
        }

        //Busca na tabela
        public Task<List<Produto>> Search(string q) 
        {
            string sql = "SELECT * FROM Produto WHERE descricao LIKE '%" +  q + "%'";

            return _conn.QueryAsync<Produto>(sql);
        }

    }
}
