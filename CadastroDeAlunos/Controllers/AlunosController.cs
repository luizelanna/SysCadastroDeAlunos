using System;
using System.Collections;
using System.Data.Entity;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Text.RegularExpressions;
using System.Web.Helpers;
using System.Web.Mvc;
using CadastroDeAlunos.Models;
using Microsoft.AspNet.Identity;
using PagedList;
using static System.Data.Entity.Core.Objects.EntityFunctions;

namespace CadastroDeAlunos.Controllers
{
    [Authorize]
    public class AlunosController : Controller
    {
        private BdAlunoEntities db = new BdAlunoEntities();

        // GET: Alunos
        public async Task<ActionResult> Index(string sortOrder, string currebtFilter, string searchString, int? page, string coluna)
        {
            Session.RemoveAll();
            ViewBag.CurrentSort = sortOrder;
            ViewBag.CodigoParamentro = String.IsNullOrEmpty(sortOrder) ? "Codigo_desc" : "";
            ViewBag.NomeParamentro = String.IsNullOrEmpty(sortOrder) ? "Nome_desc" : "";
            ViewBag.CpfParamentro = String.IsNullOrEmpty(sortOrder) ? "Cpf_desc" : "";
            ViewBag.DataNascimentoParamentro = String.IsNullOrEmpty(sortOrder) ? "DataNasc_desc" : "";
            ViewBag.DataCadastroParamentro = String.IsNullOrEmpty(sortOrder) ? "DataCadas_desc" : "";
            ViewBag.CidadeParamentro = String.IsNullOrEmpty(sortOrder) ? "Cidade_desc" : "";

            if (searchString != null)
            {
                page = 1;
            }
            ViewBag.CurrentSort = searchString;
            ViewBag.coluna = coluna;

            var alunoModel = db.Pessoas.Where(c => c.idTpoPessoa == 1);

            if (!String.IsNullOrEmpty(searchString))
            {
                if (coluna == "id")
                {
                    Regex regexObj = new Regex(@"[^\d]");
                    string numero = regexObj.Replace(searchString, "");
                    if (numero != "")
                    {
                        int cod = Convert.ToInt32(numero);
                        alunoModel = alunoModel.Where(c => c.id == cod);
                    }
                }
                else if (coluna == "Nome")
                {
                    alunoModel = alunoModel.Where(b => b.Nome.ToUpper().Contains(searchString.ToUpper()));
                }
                else if (coluna == "Cpf")
                {
                    alunoModel = alunoModel.Where(b => b.Cpf.ToUpper().Contains(searchString.ToUpper()));
                }
                else if (coluna == "DataNascimento")
                {
                    Regex regexData = new Regex(@"[a-zA-Z]+");
                    var numero = regexData.Replace(searchString, "");
                    DateTime data = Convert.ToDateTime(numero);
                    Match match = Regex.Match(data.ToString(CultureInfo.InvariantCulture), @"^([1-9]|([012][0-9])|(3[01]))\/([0]{0,1}[1-9]|1[012])\/\d\d\d\d [012]{0,1}[0-9]:[0-6][0-9]:[0-6][0-9]$");
                    if (match.Success)
                    {
                        alunoModel = alunoModel.Where(c => TruncateTime(c.DataNascimento) == TruncateTime(data));
                    }
                }
                else if (coluna == "DataCadastro")
                {
                    Regex regexData = new Regex(@"[a-zA-Z]+");
                    var numero = regexData.Replace(searchString, "");
                    DateTime data = Convert.ToDateTime(numero);
                    Match match = Regex.Match(data.ToString(CultureInfo.InvariantCulture), @"^([1-9]|([012][0-9])|(3[01]))\/([0]{0,1}[1-9]|1[012])\/\d\d\d\d [012]{0,1}[0-9]:[0-6][0-9]:[0-6][0-9]$");
                    if (match.Success)
                    {
                        alunoModel = alunoModel.Where(c => TruncateTime(c.DataCadastro) == TruncateTime(data));
                    }
                }
            }
            switch (sortOrder)
            {
                case "Codigo_desc":
                    alunoModel = alunoModel.OrderBy(b => b.id);
                    break;
                case "Nome_desc":
                    alunoModel = alunoModel.OrderBy(b => b.Nome);
                    break;
                case "Cpf_desc":
                    alunoModel = alunoModel.OrderBy(b => b.Cpf);
                    break;
                case "Cep_desc":
                    alunoModel = alunoModel.OrderByDescending(b => b.DataNascimento);
                    break;
                case "DataNasc_desc":
                    alunoModel = alunoModel.OrderByDescending(b => b.DataNascimento);
                    break;
                case "DataCadas_desc":
                    alunoModel = alunoModel.OrderByDescending(b => b.DataCadastro);
                    break;
                default:
                    alunoModel = alunoModel.OrderByDescending(b => b.id);
                    break;
            }

            int pageSize = 10;
            int pageNumber = (page ?? 1);

            return View(alunoModel.ToPagedList(pageNumber, pageSize));
        }


        public ActionResult AlunoCidade(string searchString, string coluna)
        {
            IQueryable<Cidades> cidade = db.Cidades;
            if (!string.IsNullOrEmpty(searchString))
            {
                if (coluna == "Codigo")
                {
                    Regex regexObj = new Regex(@"[^\d]");
                    string numero = regexObj.Replace(searchString, "");
                    if (numero != "")
                    {
                        int cod = Convert.ToInt32(numero);
                        cidade = cidade.Where(c => c.id == cod);
                    }
                }
                else if (coluna == "NomeCidade")
                {
                    cidade = cidade.Where(c => c.NomeCidade.Contains(searchString));
                }
                else if (coluna == "Estado")
                {
                    cidade = cidade.Where(c => c.Estado.Contains(searchString));
                }
                else if (coluna == "Cep")
                {
                    cidade = cidade.Where(c => c.Cep.Contains(searchString));
                }
            }
            return View(cidade);
        }

        public ActionResult ListaAlunoCidade(string cidade)
        {
            @ViewBag.NomeCidade = cidade;
            var alunoCidade = db.Pessoas.Where(p => p.Cidade == cidade);

            return View("_ListaAlunosCidade", alunoCidade);
        }

        // GET: Alunos/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Pessoas pessoas = await db.Pessoas.FindAsync(id);
            if (pessoas == null)
            {
                return HttpNotFound();
            }
            return View(pessoas);
        }

        public ActionResult CreatePai()
        {
            return PartialView();
        }

        // POST: Alunos/Create
        // Para se proteger de mais ataques, ative as propriedades específicas a que você quer se conectar. Para 
        // obter mais detalhes, consulte https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> CreatePai([Bind(Include = "id,idPessoa,idTpoPessoa,idUsuarioUpdate,Nome,Rg,Cpf,DataNascimento,Matricula,Idade,Sexo,Profissao,DataCadastro,DataAlteração,Telefone,Celular1,Celular2,Logradouro,Numero,Bairro,Complemento,Cidade,Estado,Cep")] Pessoas pessoas)
        {
            if (ModelState.IsValid)
            {
                pessoas.idTpoPessoa = 2;
                pessoas.DataCadastro = DateTime.Now; ;
                db.Pessoas.Add(pessoas);
                await db.SaveChangesAsync();
                Session["Pai"] = pessoas.id;
                return Json(new { success = true });
            }

            return PartialView(pessoas);
        }

        public ActionResult CreateMae()
        {
            return PartialView();
        }

        // POST: Alunos/Create
        // Para se proteger de mais ataques, ative as propriedades específicas a que você quer se conectar. Para 
        // obter mais detalhes, consulte https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> CreateMae([Bind(Include = "id,idPessoa,idTpoPessoa,idUsuarioUpdate,Nome,Rg,Cpf,DataNascimento,Matricula,Idade,Sexo,Profissao,DataCadastro,DataAlteração,Telefone,Celular1,Celular2,Logradouro,Numero,Bairro,Complemento,Cidade,Estado,Cep")] Pessoas pessoas)
        {
            if (ModelState.IsValid)
            {
                pessoas.idTpoPessoa = 3;
                pessoas.DataCadastro = DateTime.Now;
                db.Pessoas.Add(pessoas);
                await db.SaveChangesAsync();
                Session["Mae"] = pessoas.id;
                return Json(new { success = true });
            }

            return PartialView(pessoas);
        }

        // GET: Alunos/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Alunos/Create
        // Para se proteger de mais ataques, ative as propriedades específicas a que você quer se conectar. Para 
        // obter mais detalhes, consulte https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "id,idPessoa,idTpoPessoa,idUsuarioUpdate,Nome,Rg,Cpf,DataNascimento,Matricula,Idade,Sexo,Profissao,DataCadastro,DataAlteração,Telefone,Celular1,Celular2,Logradouro,Numero,Bairro,Complemento,Cidade,Estado,Cep")] Pessoas pessoas)
        {
            if (ModelState.IsValid)
            {
                VeriicaCidade(pessoas.Cidade, pessoas.Estado, pessoas.Cep);
                pessoas.idTpoPessoa = 1;
                pessoas.idUsuarioUpdate = User.Identity.GetUserId();
                pessoas.DataCadastro = DateTime.Now;
                pessoas.DataAlteração = DateTime.Now;
                db.Pessoas.Add(pessoas);
                await db.SaveChangesAsync();
                if (Session["Pai"] != null)
                {
                    UpdatePais(Session["Pai"].ToString(), pessoas.id);
                    Session.Remove("Pai");
                }
                if (Session["Mae"] != null)
                {
                    UpdatePais(Session["Mae"].ToString(), pessoas.id);
                    Session.Remove("Mae");
                }

                return RedirectToAction("Index");
            }

            return View(pessoas);
        }

        public void VeriicaCidade(string nomeCidade, string estado, string cep)
        {
            var cidade = db.Cidades.Count(c => c.NomeCidade == nomeCidade);
            if (cidade > 0)
            {
                var cidadeModel = new Cidades
                {
                    NomeCidade = nomeCidade,
                    Estado = estado,
                    Cep = cep
                };
                db.Cidades.Add(cidadeModel);
                db.SaveChanges();
            }
        }

        public void UpdatePais(string id, int idAluno)
        {
            var pessoa = db.Pessoas.Find(Convert.ToInt32(id));
            pessoa.idPessoa = idAluno;
            db.Entry(pessoa).State = EntityState.Modified;
            db.SaveChanges();
        }

        // GET: Alunos/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Pessoas pessoas = await db.Pessoas.FindAsync(id);
            if (pessoas == null)
            {
                return HttpNotFound();
            }
            return View(pessoas);
        }

        // POST: Alunos/Edit/5
        // Para se proteger de mais ataques, ative as propriedades específicas a que você quer se conectar. Para 
        // obter mais detalhes, consulte https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "id,idPessoa,idTpoPessoa,idUsuarioUpdate,Nome,Rg,Cpf,DataNascimento,Matricula,Idade,Sexo,Profissao,DataCadastro,DataAlteração,Telefone,Celular1,Celular2,Logradouro,Numero,Bairro,Complemento,Cidade,Estado,Cep")] Pessoas pessoas)
        {
            if (ModelState.IsValid)
            {
                pessoas.idUsuarioUpdate = User.Identity.GetUserId();
                pessoas.DataAlteração = DateTime.Now;
                db.Entry(pessoas).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(pessoas);
        }

        // GET: Alunos/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Pessoas pessoas = await db.Pessoas.FindAsync(id);
            if (pessoas == null)
            {
                return HttpNotFound();
            }
            return View(pessoas);
        }

        // POST: Alunos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Pessoas pessoas = await db.Pessoas.FindAsync(id);
            db.Pessoas.Remove(pessoas);
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
