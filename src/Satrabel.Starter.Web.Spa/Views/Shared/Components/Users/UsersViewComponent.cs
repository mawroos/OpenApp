﻿using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Satrabel.OpenApp.Web.Views;
using Abp.Application.Navigation;
using Satrabel.OpenApp.Users;
using Satrabel.OpenApp.Users.Dto;

namespace Satrabel.Starter.Web.Views.Shared.Components.Users
{
    public class UsersViewComponent : OpenAppViewComponent
    {
        private readonly IUserAppService _repository;
        public UsersViewComponent(IUserAppService repository)
        {
            _repository = repository;
        }

        public async Task<IViewComponentResult> InvokeAsync(UserMenuItem menuItem)
        {
            var res = await _repository.GetAll(new UsersResultRequestDto()
            {
                MaxResultCount =1,
                SkipCount = 0
            });
            var model = new UsersViewModel
            {
                MenuItem = menuItem,
                Count = res.TotalCount
            };
            return View(model);
        }
    }
}
