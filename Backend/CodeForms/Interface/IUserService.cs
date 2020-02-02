﻿using CodeForms.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CodeForms.Interface
{
    public interface IUserService
    {
        Task<ActionResult<IEnumerable<User>>> GetAll();
        Task<ActionResult<User>> CreateUser(User user);

    }
}
