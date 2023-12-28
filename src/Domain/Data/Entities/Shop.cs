using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Data.Entities;

public class Shop
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Address { get; set; }
    public bool IsDeleted { get; set; }
}
