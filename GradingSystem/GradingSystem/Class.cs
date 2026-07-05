using System;
using System.Collections.Generic;
using System.Text;

namespace GradingSystem
{
    public class Class
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<Student> Students { get; set; } = new();
    }
}
