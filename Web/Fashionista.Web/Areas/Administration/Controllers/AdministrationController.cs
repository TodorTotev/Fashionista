﻿using Microsoft.AspNetCore.Authorization;

namespace Fashionista.Web.Areas.Administration.Controllers
{
    using Fashionista.Web.Controllers;
    using Microsoft.AspNetCore.Mvc;

    [Authorize]
    [Area("Administration")]
    public class AdministrationController : BaseController
    {
    }
}
