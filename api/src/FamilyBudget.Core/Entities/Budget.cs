﻿namespace FamilyBudget.Core.Entities;

public class Budget
{
    public string Month { get; set; }
    public string Category { get; set; }
    public decimal Income { get; set; }
    public decimal Expenses { get; set; }
    public bool Shared { get; set; }

    public Budget(string month, string category, decimal income, decimal expenses, bool shared)
    {
        Month = month;
        Category = category;
        Income = income;
        Expenses = expenses;
        Shared = shared;
    }
}