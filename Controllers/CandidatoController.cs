using Microsoft.AspNetCore.Mvc;

public class CandidatoController : Controller
{
    private ICandidatoData data;
    private readonly IWebHostEnvironment _hostingEnvironment;

    public CandidatoController(ICandidatoData data, IWebHostEnvironment hostingEnvironment)
    {
        this.data = data;
        _hostingEnvironment = hostingEnvironment;
    }

    [HttpGet]
    public ActionResult TrabalheConosco(int id)
    {
        Vagas vagas = data.ReadVaga(id);
    
        ViewBag.texto = false;

        return View(vagas);
    }

    [HttpPost]
    public async Task<IActionResult> TrabalheConosco(int id, int admin, Candidato candidato)
    {
       if (candidato.Curriculo != null && candidato.Curriculo.Length > 0)
        {
            candidato.FileName = Path.GetFileName(candidato.Curriculo.FileName);
            candidato.FilePath = Path.Combine(_hostingEnvironment.WebRootPath, "uploads", candidato.FileName);

            using (var stream = new FileStream(candidato.FilePath!, FileMode.Create))
            {
                await candidato.Curriculo.CopyToAsync(stream);
            }

            data.TrabalheConosco(id, admin, candidato);

            Vagas vagas = data.ReadVaga(id);

            ViewBag.texto = true;

            return View(vagas);
        }

        return BadRequest("Nenhum arquivo foi enviado.");
    }

    public ActionResult ListaCandidato(int id)
    {
        return View(data.Read(id));
    }
}
