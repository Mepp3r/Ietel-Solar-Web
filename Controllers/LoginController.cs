using Microsoft.AspNetCore.Mvc;
public class LoginController : Controller
{
 private ILoginData data;

    public LoginController(ILoginData data)
    {
        this.data = data;
    }

    [HttpPost]
    public ActionResult Login(Login login)
    {
        if(data.Verificacao(login))
        {
            return RedirectToAction("Agendamento", "Agendamento");
        }
        return View();
    }


    [HttpGet]
    public ActionResult Login()
    {
        return View();
    }
}
