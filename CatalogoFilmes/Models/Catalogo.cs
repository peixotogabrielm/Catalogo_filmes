using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CatalogoFilmes.Models
{
    public class Catalogo : Base
    {
        public string Nome { get; set; }
        public string Descricao { get; set; }
        public int Visualizacoes { get; set; }
        public int Likes { get; set; }
        public int Dislikes { get; set; }
        public int NumeroFavoritos { get; set; }
        public Tags Tags { get; set; }
        public ICollection<Filme> Filmes { get; set; }
        public string UsuarioId { get; set; }
        public Usuario Usuario { get; set; }
    }
    
    [Flags]
    public enum Tags
    {      
        Acao,
        Aventura,
        Animacao,
        Comedia,
        ComediaRomantica,
        ComediaDramatica,
        Crime,
        Documentario,
        Drama,
        Fantasia,
        Faroeste,
        FiccaoCientifica,
        Guerra,
        Historia,
        Mistério,
        Musical,
        Romance,
        Suspense,
        Terror,
        Biografia,
        Familia,
        Policial,
        Esporte,
        SuperHeroi,
        Thriller,
        Noir,
        Curtametragem,
        Experimental,
        Religioso,
        Infantil,
        Arte,
        Catastrofe,
        Espionagem,
        ArtesMarciais,
        Apocaliptico,
        Cyberpunk,
        Steampunk,
        Zumbi,
        Natal,
        Dança,
        Politico,
        Legal, 
        ViagemNoTempo,
        LGBTQIA,
        Mitologia,
        Natureza,
        Animais,
        CienciaENatureza,
        MusicalInfantil,
        SuspensePsicologico,
        TerrorSobrenatural,
        DramaHistorico,
        DramaRomantico,
        FantasiaUrbana,
        FiccaoDistopica,
        FiccaoEspacial,
        FiccaoMedica,
        AçãoMilitar,
        MistérioPolicial,
        DramaEsportivo,
        DramaBiografico,
        DramaFamiliar,
        RomanceHistorico
    }

}