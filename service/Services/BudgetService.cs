using infrastructure.DataModels;
using infrastructure.Repositories;

namespace service;

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

    public Budget GetStartAmount(int userId)
    {
        return _budgetRepository.GetStartAmount(userId);
    }

    public Budget UpdateStartAmount(int userId, float updatedStartAmount)
    {
        return _budgetRepository.UpdateStartAmount(userId, updatedStartAmount);
    }
    
}
