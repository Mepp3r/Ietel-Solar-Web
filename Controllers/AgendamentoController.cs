using Microsoft.AspNetCore.Mvc;

public class AgendamentoController : Controller
{
    private IAgendamentoData data;

    public AgendamentoController(IAgendamentoData data)
    {
        this.data = data;
    }

    [HttpGet]
    public ActionResult Agendamento()
    {
        return View();
    }

    [HttpPost]
    public ActionResult Agendamento(Agendamento agendamento)
    {
        data.Agendamento(agendamento);
        return View();
    }

    [HttpPost]
    public async Task<string> ListaAgendamento(string date)
    {
        return await data.ReadAgendamento(date);;
    }
}
