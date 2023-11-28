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
}
