﻿namespace Dapper_VS_EFcore.Models
{
    public class Category
    {
        public int CategoryID { get; set; }
        public string Name { get; set; } = string.Empty;

        public List<GameCategory> GameCategories { get; set; } = new();
    }
}