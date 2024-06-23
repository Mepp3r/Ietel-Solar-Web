using Microsoft.AspNetCore.Mvc;

public class OrcamentoController : Controller
{
    private IOrcamentoData data;
    private IAgendamentoData agendadata;

    public OrcamentoController (IOrcamentoData data, IAgendamentoData agendadata)
    {
        this.data = data;
        this.agendadata= agendadata;
    }

    [HttpGet]
    public ActionResult Orcamento() 
    {
        ViewBag.HorarioAgendamento=data.Read();
        
        return View();       
    }

    [HttpPost] 
    public ActionResult Orcamento(Orcamento orcamento)
    {
        ViewBag.HorarioAgendamento=data.Read();
        
        data.Orcamento(orcamento);

        ViewBag.HorarioAgendamento=data.Read();
        
        return View();
    }

    [HttpPost]
    public ActionResult ListaOrcamento(IFormCollection form)
    {
        string dataO = form["data"];
        return View(data.ListaOrcamento(dataO));
    }

    [HttpGet]
    public ActionResult ListaOrcamento()
    {
        return View();
    }
     
 }
