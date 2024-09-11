using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
namespace SLC
{
    public interface IService
    {
        Task<ActionResult<Customer>> CreateAsync([FromBody] Customer toCreate);
        Task<ActionResult> DeleteAsync(int id);
        Task<ActionResult<List<Customer>>> GetAll();
        Task<ActionResult<Customer>> RetreiveAsync(int id);
        Task<ActionResult> UpdateAsync(int id, [FromBody] Customer toUpdate);
    }
}
