using System;

namespace WorkSharp.DAL.DbModels
{
    public class DbSolution
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Path { get; set; }
    }
}
