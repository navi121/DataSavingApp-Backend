using DataSavingApplication.Models;
using FluentValidation;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DataSavingApplication.Service
{
    public class FluentValidation : AbstractValidator<IFormFile>
    {
        public FluentValidation()
        {
            RuleFor(x => x.ContentType).NotNull().Must(x => x.Equals("application/vnd.openxmlformats-officedocument.spreadsheetml.sheet")).WithMessage("File type not allowed");
        }
    }
}
