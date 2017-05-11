using System.Linq;
using System.Web.Mvc;
using CadastroDeAlunos.Models;

namespace CadastroDeAlunos.Controllers
{
    public class RelatoriosController : Controller
    {
        private BdAlunoEntities db = new BdAlunoEntities();
        // GET: Relatorios
        public ActionResult Alunos()
        {
            ViewBag.NomeRelatorio = "Relatorio de Alunos";
            var alunos = db.Pessoas.Where(a => a.idTpoPessoa == 1).OrderBy(a => a.Cidade);
            return View(alunos.ToList());
        }

        public ActionResult Cidades()
        {
            ViewBag.NomeRelatorio = "Relatorio de Cidades";
            var cidades = db.Cidades.OrderBy(c => c.NomeCidade);
            return View(cidades.ToList());
        }

        public ActionResult AlunosCidades()
        {
            ViewBag.NomeRelatorio = "Relatorio de Alunos por Cidade";
            ViewBag.Cidade = db.Cidades.Distinct().OrderBy(c => c.NomeCidade).Select(c => c.NomeCidade);
            
            return View();
        }

        public ActionResult ListaAlunosCidades(string nomeCidade)
        {
            var alunos = db.Pessoas.Where(p => p.idTpoPessoa == 1 && p.Cidade == nomeCidade).OrderBy(p => p.Nome);
            ViewBag.NomeCidade = nomeCidade;
            return View(alunos.ToList());
        }
    }
}

///*http://www.devmedia.com.br/como-gerar-relatorios-no-asp-net-mvc/33921*/