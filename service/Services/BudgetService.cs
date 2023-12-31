﻿using infrastructure.DataModels;
using infrastructure.Repositories;
using service.Models.Command;

namespace service.Services;

public class BudgetService
{
    private readonly BudgetRepository _budgetRepository;
    
    public BudgetService(BudgetRepository budgetRepository)
    {
        _budgetRepository = budgetRepository;
    }

    public Budget GetCurrentAmount(int userId)
    {
        return _budgetRepository.GetCurrentAmount(userId);
    }
    
    public Budget UpdateCurrentAmount(int userId, float newCurrentAmount)
    {
        return _budgetRepository.UpdateCurrentAmount(userId, newCurrentAmount);
    }
    
    public Budget UpdateStartAmount(int userId, float updatedStartAmount)
    {
        return _budgetRepository.UpdateStartAmount(userId, updatedStartAmount);
    }

    public IEnumerable<Transaction> GetTransactions(int userId)
    {
        return _budgetRepository.GetTransactions(userId);
    }
    public Transaction PostTransactions(int userId, PostTransaction command)
    {
        return _budgetRepository.PostTransactions(
            userId,
            command.ItemAmount,
            command.ItemName,
            command.TotalCost);
    }
    public Transaction UpdateTransactions(int userId, UpdateTransactions command)
    {
        return _budgetRepository.UpdateTransactions(
            userId,
            command.id,
            command.ItemAmount,
            command.ItemName,
            command.TotalCost);
    }

    public void DeleteTransaction(int userId, int id)
    { 
        _budgetRepository.DeleteTransaction(userId, id);
    }
}
