using System;

namespace CRUD_Workout
{
    public class Workout
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Duration { get; set; }
        public string? Description { get; set; }
        public byte[]? Image { get; set; }
    }
}
