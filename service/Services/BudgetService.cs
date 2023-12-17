using infrastructure.DataModels;
using infrastructure.Repositories;
using service.Models.Command;

namespace service;

public class BudgetService
{
    private readonly BudgetRepository _budgetRepository;
    
    public BudgetService(BudgetRepository budgetRepository)
    {
        _budgetRepository = budgetRepository;
    }

    public Budget GetAmounts(int userId)
    {
        return _budgetRepository.GetAmounts(userId);
    }
    
    public Budget UpdateCurrentAmount(int userId, float newCurrentAmount)
    {
        return _budgetRepository.UpdateCurrentAmount(userId, newCurrentAmount);
    }
    
    /*public Budget GetStartAmount(int userId)
    {
        return _budgetRepository.GetStartAmount(userId);  
    }
    */

    public Budget UpdateStartAmount(int userId, float updatedStartAmount)
    {
        return _budgetRepository.UpdateStartAmount(userId, updatedStartAmount);
    }

    public IEnumerable<Transaction> getTransactions(int userId)
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
}
