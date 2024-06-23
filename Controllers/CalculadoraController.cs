using Microsoft.AspNetCore.Mvc;

public class CalculadoraController : Controller
{
    [HttpGet]
    public ActionResult CalculadoraSolar()
    {
        return View();
    }
    [HttpPost]
    public ActionResult SimuladorSolar(Calculadora form)
    {
        var producaoDoAparalho = 57.50;
        var kwpDoAparelho = 0.46;
        var areaDoAparelho = 2.157;
        var eficienciaDoAparelho = 0.21;
        var diasDoAno = 8760;
        var imprevistos = 0.65;
        var precoDoAparelho = 1200;
        var maoDeObra = 2750;
        var kwhMes = form.ValorGastoMensalmente / (form.Concessionaria / 100);
        form.QuantidadeDePlacasInstaladas = (int)Math.Ceiling(kwhMes / producaoDoAparalho);
        form.ProducaoMensal = form.QuantidadeDePlacasInstaladas * producaoDoAparalho;
        form.PotenciaInstalada = form.QuantidadeDePlacasInstaladas * kwpDoAparelho;
        form.AreaMinima = areaDoAparelho * form.QuantidadeDePlacasInstaladas;
        var producaoAnual = form.PotenciaInstalada * eficienciaDoAparelho * diasDoAno * imprevistos;
        form.EconomiaAnual = producaoAnual * (form.Concessionaria /100);
        var investimentoInicial = (form.QuantidadeDePlacasInstaladas * precoDoAparelho) + maoDeObra;
        form.TempoDePayback = investimentoInicial / form.EconomiaAnual;
        return View(form);
    }
}