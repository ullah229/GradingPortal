using System;
using System.Collections.Generic;
using System.Text;

namespace GradingSystem;
    public class Student
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public List<Subject> Subjects { get; set; } = new();
    }
