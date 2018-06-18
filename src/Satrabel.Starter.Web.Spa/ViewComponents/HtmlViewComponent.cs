﻿using Microsoft.AspNetCore.Mvc;
using Satrabel.OpenApp.Web.Views;
using Satrabel.Starter.Web.Application;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Satrabel.Starter.Web.ViewComponents
{
    [ViewComponent(Name = "Html")]
    public class HtmlViewComponent: OpenAppViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync(ModuleDto module)
        {
            return View(module);
        }
    }
}