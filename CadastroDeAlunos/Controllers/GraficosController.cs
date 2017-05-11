using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CadastroDeAlunos.Models;
using Newtonsoft.Json;
using static System.Data.Entity.Core.Objects.EntityFunctions;


namespace CadastroDeAlunos.Controllers
{
    public class GraficosController : Controller
    {
        private BdAlunoEntities db = new BdAlunoEntities();

        // GET: Graficos
        public ActionResult Index()
        {
            var dadosAlunos = db.Pessoas.Where(p => p.idTpoPessoa == 1);
            var cidades = dadosAlunos.Distinct().OrderBy(c => c.Cidade).Select(c => c.Cidade);

            List<object> chartData = new List<object>();
            chartData.Add(new object[]
                       {
                           "Horas", "Alunos"
                       });
            foreach (var item in cidades)
            {
                int qtdCadastraddos = dadosAlunos.Count(c => c.Cidade == item);
                chartData.Add(new object[]
                       {
                           item, qtdCadastraddos
                       });
            }

            string datastring = JsonConvert.SerializeObject(chartData, Formatting.None);
            ViewBag.Data = new HtmlString(datastring);
            return View();
        }

        public ActionResult GraficoAlunoData()
        {
            var dadosAlunos = db.Pessoas.Where(p => p.idTpoPessoa == 1);
            var horas = dadosAlunos.OrderBy(c => c.DataCadastro).Distinct().ToList();

            List<object> chartData = new List<object>();
            chartData.Add(new object[]
                       {
                           "Data/Hora", "Quantidade"
                       });
            foreach (var item in horas)
            {
                var qtdCadastraddos = horas.Count(p => p.idTpoPessoa == 1 && p.DataCadastro == item.DataCadastro);
                chartData.Add(new object[]
                       {
                           item.DataCadastro, qtdCadastraddos
                       });
            }

            string datastring = JsonConvert.SerializeObject(chartData, Formatting.None);
            ViewBag.Data = new HtmlString(datastring);
            return View();
        }


    }
}

/*
 * public ActionResult GraficoAlunoData()
        {
            List<Pessoas> sd = new List<Pessoas>();
            var dadosAlunos = db.Pessoas.Where(p => p.idTpoPessoa == 1);
            var horas = dadosAlunos.OrderBy(c => c.DataCadastro).Select(c => c.DataCadastro).Distinct().ToList();
            
            var chartData = new object[horas.Count+1];
            chartData[0] = new object[]
                       {
                           "Datahora", "quantidade"
                       };
            int j = 0;
            foreach (var item in horas)
            {
                j++;
                var qtdCadastraddos = dadosAlunos.Count(c => c.DataCadastro == item);
                //string data = item.Value.ToShortDateString();
                chartData[j] = new object[]
                       {
                           item, qtdCadastraddos
                       };
            }

            string datastring = JsonConvert.SerializeObject(chartData, Formatting.None);
ViewBag.Data = new HtmlString(datastring);
            return View();
        }
        */