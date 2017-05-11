using System.Data.Entity;
using System.Net;
using System.Threading.Tasks;
using System.Web.Mvc;
using CadastroDeAlunos.Models;

namespace CadastroDeAlunos.Controllers
{
    public class TipoPessoasController : Controller
    {
        private BdAlunoEntities db = new BdAlunoEntities();

        // GET: Aluno/TipoPessoas
        public async Task<ActionResult> Index()
        {
            return View(await db.TipoPessoas.ToListAsync());
        }

        // GET: Aluno/TipoPessoas/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Aluno/TipoPessoas/Create
        // Para se proteger de mais ataques, ative as propriedades específicas a que você quer se conectar. Para 
        // obter mais detalhes, consulte https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "id,Descricao")] TipoPessoas tipoPessoas)
        {
            if (ModelState.IsValid)
            {
                db.TipoPessoas.Add(tipoPessoas);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(tipoPessoas);
        }

        // GET: Aluno/TipoPessoas/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TipoPessoas tipoPessoas = await db.TipoPessoas.FindAsync(id);
            if (tipoPessoas == null)
            {
                return HttpNotFound();
            }
            return View(tipoPessoas);
        }

        // POST: Aluno/TipoPessoas/Edit/5
        // Para se proteger de mais ataques, ative as propriedades específicas a que você quer se conectar. Para 
        // obter mais detalhes, consulte https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "id,Descricao")] TipoPessoas tipoPessoas)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tipoPessoas).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(tipoPessoas);
        }

        // GET: Aluno/TipoPessoas/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TipoPessoas tipoPessoas = await db.TipoPessoas.FindAsync(id);
            if (tipoPessoas == null)
            {
                return HttpNotFound();
            }
            return View(tipoPessoas);
        }

        // POST: Aluno/TipoPessoas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            TipoPessoas tipoPessoas = await db.TipoPessoas.FindAsync(id);
            db.TipoPessoas.Remove(tipoPessoas);
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
