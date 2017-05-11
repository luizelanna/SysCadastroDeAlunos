using System.Linq;
using System.Web.Mvc;
using CadastroDeAlunos.Models;
using Microsoft.AspNet.Identity;

namespace CadastroDeAlunos.Controllers
{
    public class PrincipalController : Controller
    {

        private BdAlunoEntities db = new BdAlunoEntities();
        
        // GET: Aluno/Principal
        public ActionResult Index()
        {
            return View();
        }
       // string página = HttpContext.Current.Request.Url;
       

        // GET: Aluno/Principal/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Aluno/Principal/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Aluno/Principal/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Aluno/Principal/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Aluno/Principal/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Aluno/Principal/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Aluno/Principal/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
