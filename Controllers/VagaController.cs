using Microsoft.AspNetCore.Mvc;

public class VagaController : Controller
{
    private IVagasData data;

    public VagaController (IVagasData data)
    {
        this.data = data;
    }

    [HttpGet]
    public ActionResult AdicionaVaga()
    {
        return View();
    }

    [HttpPost]
    public ActionResult AdicionaVaga(Vagas vagas)
    {
        data.AdicionaVaga(vagas);
        return RedirectToAction("ListaVaga");
    }
    [HttpGet]
    public ActionResult Remover(int id)
    {
        data.Remover(id);
        return RedirectToAction("ListaVaga");
    }
   [HttpGet]
    public ActionResult EditarVaga(int id)
    {
        Vagas vagas = data.ReadUpdate(id);

        if(vagas == null)
            return RedirectToAction("vaga", "listarVaga");

        return View(vagas);
    }
    [HttpPost]
    public ActionResult EditarVaga(Vagas form)
    {
        data.Alterar(form);
        return RedirectToAction("ListaVaga");
    }
    public ActionResult ListaVaga()
    {
        return View(data.Read());
    }

    public ActionResult ListaVagaCandidato()
    {
        return View(data.Read());
    }
}
