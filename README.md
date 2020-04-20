# EFCorePaginator
Paginator library for .net entity framework


### Usage

```C#
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using EFCorePaginator;    
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

    [ApiController]
    public class UserController : Controller
    {
    
        public async Task<ActionResult<IEnumerable<User>>> GetFilteredAccount([FromQuery] PagingParams pagingParams)     
        {
            IQueryable<User> query = _applicationDbContext.Users.ToQueryable();
            var pagedList = new PagedList<User>(query, pagingParams); // created paged data
            return Ok(pagedList);
        }
    }
}
            
```