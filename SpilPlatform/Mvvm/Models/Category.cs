using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class Category
{
    public Guid Id { get; set; }
    public string Name { get; set; }

    public Category(string name)
    {
        Name = name;
    }
}