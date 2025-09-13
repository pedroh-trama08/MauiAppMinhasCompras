using SQLite;

namespace MauiAppMinhasCompras.Models
{
    public class Produto
    {
        string _descricao;

        double _quant;

        double _preco;

        string _categoria;

        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public string Descricao { 
            get => _descricao;
            set
            {
                if (value == null)
                {
                    throw new Exception("Por favor, preencha a descrição");
                }

                _descricao = value;
            }
        }
        public double Quantidade {
            get => _quant; 
            set
            {
                if (value <= 0)
                {
                    throw new Exception("Insira valores acima de zero");
                }

                _quant = value;
            }
        }
        public double Preco {
            get => _preco;
            set
            {
                if (value <= 0)
                {
                    throw new Exception("Insira valores acima de zero");

                }

                _preco = value;
            }
        }
        public double Total { get => Quantidade * Preco;}

        public string Categoria {
            get => _categoria;
            set
            {
                if(value == null)
                {
                    throw new Exception("Por favor, insira a categoria do produto");
                }

                _categoria = value;
            }        
        }
    }
}
